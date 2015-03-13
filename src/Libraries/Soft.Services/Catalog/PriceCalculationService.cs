using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Soft.Core;
using Soft.Core.Caching;
using Soft.Core.Domain.Catalog;
using Soft.Core.Domain.Customers;
using Soft.Core.Domain.Discounts;
using Soft.Core.Domain.Orders;
using Soft.Services.Catalog.Cache;
using Soft.Services.Discounts;

namespace Soft.Services.Catalog
{
    /// <summary>
    ///     Servicio para calcular los precios
    /// </summary>
    public class PriceCalculationService : IPriceCalculationService
    {
        #region Campos

        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IDiscountService _discountService;
        private readonly ICategoryService _categoryService;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly IProductService _productService;
        private readonly ICacheManager _cacheManager;
        private readonly ShoppingCartSettings _shoppingCartSettings;
        private readonly CatalogSettings _catalogSettings;

        #endregion

        #region Ctor

        public PriceCalculationService(IWorkContext workContext,
            IStoreContext storeContext,
            IDiscountService discountService,
            ICategoryService categoryService,
            IProductAttributeParser productAttributeParser,
            IProductService productService,
            ICacheManager cacheManager,
            ShoppingCartSettings shoppingCartSettings,
            CatalogSettings catalogSettings)
        {
            _workContext = workContext;
            _storeContext = storeContext;
            _discountService = discountService;
            _categoryService = categoryService;
            _productAttributeParser = productAttributeParser;
            _productService = productService;
            _cacheManager = cacheManager;
            _shoppingCartSettings = shoppingCartSettings;
            _catalogSettings = catalogSettings;
        }

        #endregion

        #region Nested classes

        [Serializable]
        protected class ProductPriceForCaching
        {
            public decimal Price { get; set; }
            public decimal AppliedDiscountAmount { get; set; }
            public int AppliedDiscountId { get; set; }
        }

        #endregion

        #region Util

        /// <summary>
        ///     Obtiene descuentos
        /// </summary>
        /// <param name="product">Producto</param>
        /// <param name="customer">Clientes</param>
        /// <returns>Descuentos</returns>
        protected virtual IList<Discount> GetAllowedDiscounts(Product product,Customer customer)
        {
            var allowedDiscounts = new List<Discount>();
            if (_catalogSettings.IgnoreDiscounts)
                return allowedDiscounts;

            if (product.HasDiscountsApplied)
            {
                //Se usa esta propiedad ("HasDiscountsApplied") para optimizar en lugar de llamarse de la base de datos
                foreach (var discount in product.AppliedDiscounts)
                {
                    if (_discountService.IsDiscountValid(discount, customer) &&
                        discount.DiscountType == DiscountType.AssignedToSkus &&
                        !allowedDiscounts.ContainsDiscount(discount))
                        allowedDiscounts.Add(discount);
                }
            }

            //performance en la optimizacion
            //Carga todas los descuentos de categorias solo para asegurar que si tenemos almenos uno
            if (_discountService.GetAllDiscounts(DiscountType.AssignedToCategories).Any())
            {
                var productCategories = _categoryService.GetProductCategoriesByProductId(product.Id);
                if (productCategories != null)
                {
                    foreach (var productCategory in productCategories)
                    {
                        var category = productCategory.Category;

                        if (category.HasDiscountsApplied)
                        {
                            //we use this property ("HasDiscountsApplied") for performance optimziation to avoid unnecessary database calls
                            var categoryDiscounts = category.AppliedDiscounts;
                            foreach (var discount in categoryDiscounts)
                            {
                                if (_discountService.IsDiscountValid(discount, customer) &&
                                    discount.DiscountType == DiscountType.AssignedToCategories &&
                                    !allowedDiscounts.ContainsDiscount(discount))
                                    allowedDiscounts.Add(discount);
                            }
                        }
                    }
                }
            }
            return allowedDiscounts;
        }

        /// <summary>
        ///     Precio de nivel
        /// </summary>
        /// <param name="product">Producto</param>
        /// <param name="customer">Cliente</param>
        /// <param name="quantity">Cantidad</param>
        /// <returns>Precio</returns>
        protected virtual decimal? GetMinimumTierPrice(Product product, Customer customer, int quantity)
        {
            if (!product.HasTierPrices)
                return decimal.Zero;

            var tierPrices = product.TierPrices
                .OrderBy(tp => tp.Quantity)
                .ToList()
                .FilterByStore(_storeContext.CurrentStore.Id)
                .FilterForCustomer(customer)
                .RemoveDuplicatedQuantities();

            var previousQty = 1;
            decimal? previousPrice = null;
            foreach (var tierPrice in tierPrices)
            {
                //Revisa las cantidades
                if (quantity < tierPrice.Quantity)
                    continue;
                if (tierPrice.Quantity < previousQty)
                    continue;

                //Guarda el nuevo precio
                previousPrice = tierPrice.Price;
                previousQty = tierPrice.Quantity;
            }

            return previousPrice;
        }

        /// <summary>
        ///     Get number of rental periods (price ratio)
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Number of rental periods</returns>
        protected virtual int GetRentalPeriods(Product product, DateTime startDate, DateTime endDate)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            if (!product.IsRental)
                return 1;

            if (startDate.CompareTo(endDate) > 0)
                return 1;

            var totalDaysToRent = (endDate - startDate).TotalDays;
            if (totalDaysToRent <= 0)
                totalDaysToRent = 1;

            int configuredPeriodDays;
            switch (product.RentalPricePeriod)
            {
                case RentalPricePeriod.Days:
                    configuredPeriodDays = 1*product.RentalPriceLength;
                    break;
                case RentalPricePeriod.Weeks:
                    configuredPeriodDays = 7*product.RentalPriceLength;
                    break;
                case RentalPricePeriod.Months:
                    configuredPeriodDays = 30*product.RentalPriceLength;
                    break;
                case RentalPricePeriod.Years:
                    configuredPeriodDays = 365*product.RentalPriceLength;
                    break;
                default:
                    throw new SoftException("Not supported rental period");
            }

            var totalPeriods = Convert.ToInt32(Math.Ceiling(totalDaysToRent/configuredPeriodDays));
            return totalPeriods;
        }

        /// <summary>
        ///     Gets discount amount
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="customer">The customer</param>
        /// <param name="productPriceWithoutDiscount">Already calculated product price without discount</param>
        /// <param name="appliedDiscount">Applied discount</param>
        /// <returns>Discount amount</returns>
        protected virtual decimal GetDiscountAmount(Product product,
            Customer customer,
            decimal productPriceWithoutDiscount,
            out Discount appliedDiscount)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            appliedDiscount = null;
            var appliedDiscountAmount = decimal.Zero;

            //No se aplica descuento al ptoducto que el precio haya sido ingresado por el cliente
            if (product.CustomerEntersPrice)
                return appliedDiscountAmount;

            //Descuentos son desabilitados
            if (_catalogSettings.IgnoreDiscounts)
                return appliedDiscountAmount;

            var allowedDiscounts = GetAllowedDiscounts(product, customer);

            //no discounts
            if (allowedDiscounts.Count == 0)
                return appliedDiscountAmount;

            appliedDiscount = allowedDiscounts.GetPreferredDiscount(productPriceWithoutDiscount);

            if (appliedDiscount != null)
                appliedDiscountAmount = appliedDiscount.GetDiscountAmount(productPriceWithoutDiscount);

            return appliedDiscountAmount;
        }

        #endregion

        #region Metodos

        /// <summary>
        ///     Me da el precio final
        /// </summary>
        /// <param name="product">Producto</param>
        /// <param name="customer">El cliente</param>
        /// <param name="additionalCharge">Cargos adicionales</param>
        /// <param name="includeDiscounts">Si incluye descuentoo no</param>
        /// <param name="quantity">Cantidad del carrtiro de compras</param>
        /// <returns>
        ///     Precio final
        /// </returns>
        public virtual decimal GetFinalPrice(Product product,
            Customer customer,
            decimal additionalCharge = decimal.Zero,
            bool includeDiscounts = true,
            int quantity = 1)
        {
            decimal discountAmount;
            Discount appliedDiscount;
            return GetFinalPrice(product, customer, additionalCharge, includeDiscounts,
                quantity, out discountAmount, out appliedDiscount);
        }

        /// <summary>
        ///     Me da el precio final
        /// </summary>
        /// <param name="product">Producto</param>
        /// <param name="customer">El cliente</param>
        /// <param name="additionalCharge">Cargos adicionales</param>
        /// <param name="includeDiscounts">Si incluye descuentoo no</param>
        /// <param name="quantity">Cantidad del carrtiro de compras</param>
        /// <param name="discountAmount">Aplica cantidad de descuento</param>
        /// <param name="appliedDiscount">Aplica descuento</param>
        /// <returns>
        ///     Precion Final
        /// </returns>
        public virtual decimal GetFinalPrice(Product product,
            Customer customer,
            decimal additionalCharge,
            bool includeDiscounts,
            int quantity,
            out decimal discountAmount,
            out Discount appliedDiscount)
        {
            return GetFinalPrice(product, customer,
                additionalCharge, includeDiscounts, quantity,
                null, null,
                out discountAmount, out appliedDiscount);
        }

        /// <summary>
        ///     Me da el precio final
        /// </summary>
        /// <param name="product">Producto</param>
        /// <param name="customer">El cliente</param>
        /// <param name="additionalCharge">Cargos adicionales</param>
        /// <param name="includeDiscounts">Si incluye descuentoo no</param>
        /// <param name="quantity">Cantidad del carrtiro de compras</param>
        /// <param name="rentalStartDate">Inicio de periodo de renta (Para priductos de alquiler)</param>
        /// <param name="rentalEndDate">Fin de periodo de renta (Para priductos de alquiler)</param>
        /// <param name="discountAmount">Aplica cantidad de descuento</param>
        /// <param name="appliedDiscount">Aplica descuento</param>
        /// <returns>
        ///     Precion Final
        /// </returns>
        /// <exception cref="System.ArgumentNullException">product</exception>
        public virtual decimal GetFinalPrice(Product product,
            Customer customer,
            decimal additionalCharge,
            bool includeDiscounts,
            int quantity,
            DateTime? rentalStartDate,
            DateTime? rentalEndDate,
            out decimal discountAmount,
            out Discount appliedDiscount)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            discountAmount = decimal.Zero;
            appliedDiscount = null;

            var cacheKey = string.Format(PriceCacheEventConsumer.ProductPriceModelKey,
                product.Id,
                additionalCharge.ToString(CultureInfo.InvariantCulture),
                includeDiscounts,
                quantity,
                string.Join(",", customer.CustomerRoles.Where(cr => cr.Active).Select(cr => cr.Id).ToList()),
                _storeContext.CurrentStore.Id);

            var cacheTime = _catalogSettings.CacheProductPrices ? 60 : 0;

            //No se tiene en la cache los precios de los productos que se rentan
            //Por otro lado, eso puede causar una perdida de memoria (para almacenar todas las combinaciones posibles de los periodos)
            if (product.IsRental)
                cacheTime = 0;
            var cachedPrice = _cacheManager.Get(cacheKey, cacheTime, () =>
            {
                var result = new ProductPriceForCaching();

                //Precio Inicial
                var price = product.Price;

                //Precio especial
                var specialPrice = product.GetSpecialPrice();
                if (specialPrice.HasValue)
                    price = specialPrice.Value;

                //Precio de nivel
                if (product.HasTierPrices)
                {
                    var tierPrice = GetMinimumTierPrice(product, customer, quantity);
                    if (tierPrice.HasValue)
                        price = Math.Min(price, tierPrice.Value);
                }

                //Cargo adicional
                price = price + additionalCharge;

                //Renta del producto
                if (product.IsRental)
                    if (rentalStartDate.HasValue && rentalEndDate.HasValue)
                        price = price*GetRentalPeriods(product, rentalStartDate.Value, rentalEndDate.Value);

                if (includeDiscounts)
                {
                    //Descuento
                    Discount tmpAppliedDiscount;
                    var tmpDiscountAmount = GetDiscountAmount(product, customer, price, out tmpAppliedDiscount);
                    price = price - tmpDiscountAmount;

                    if (tmpAppliedDiscount != null)
                    {
                        result.AppliedDiscountId = tmpAppliedDiscount.Id;
                        result.AppliedDiscountAmount = tmpDiscountAmount;
                    }
                }

                if (price < decimal.Zero)
                    price = decimal.Zero;

                result.Price = price;
                return result;
            });

            if (includeDiscounts)
            {
                //Discount instance cannnot be cached between requests (when "catalogSettings.CacheProductPrices" is "true)
                //This is limitation of Entity Framework
                //That's why we load it here after working with cache
                appliedDiscount = _discountService.GetDiscountById(cachedPrice.AppliedDiscountId);
                if (appliedDiscount != null)
                {
                    discountAmount = cachedPrice.AppliedDiscountAmount;
                }
            }

            return cachedPrice.Price;
        }


        /// <summary>
        ///     Obtiene el precio unitario de un item del carrito
        /// </summary>
        /// <param name="shoppingCartItem">Item del carrito</param>
        /// <param name="includeDiscounts">Si se incluye descuentos o no</param>
        /// <returns>Precio unitario del item</returns>
        public virtual decimal GetUnitPrice(ShoppingCartItem shoppingCartItem,
            bool includeDiscounts = true)
        {
            decimal discountAmount;
            Discount appliedDiscount;
            return GetUnitPrice(shoppingCartItem, includeDiscounts,
                out discountAmount, out appliedDiscount);
        }

        /// <summary>
        ///     Gets the shopping cart unit price (one item)
        /// </summary>
        /// <param name="shoppingCartItem">The shopping cart item</param>
        /// <param name="includeDiscounts">A value indicating whether include discounts or not for price computation</param>
        /// <param name="discountAmount">Applied discount amount</param>
        /// <param name="appliedDiscount">Applied discount</param>
        /// <returns>Shopping cart unit price (one item)</returns>
        public virtual decimal GetUnitPrice(ShoppingCartItem shoppingCartItem,
            bool includeDiscounts,
            out decimal discountAmount,
            out Discount appliedDiscount)
        {
            if (shoppingCartItem == null)
                throw new ArgumentNullException("shoppingCartItem");

            return GetUnitPrice(shoppingCartItem.Product,
                shoppingCartItem.Customer,
                shoppingCartItem.ShoppingCartType,
                shoppingCartItem.Quantity,
                shoppingCartItem.AttributesXml,
                shoppingCartItem.CustomerEnteredPrice,
                shoppingCartItem.RentalStartDateUtc,
                shoppingCartItem.RentalEndDateUtc,
                includeDiscounts,
                out discountAmount,
                out appliedDiscount);
        }

        /// <summary>
        ///     Gets the shopping cart unit price (one item)
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="customer">Customer</param>
        /// <param name="shoppingCartType">Shopping cart type</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="attributesXml">Product atrributes (XML format)</param>
        /// <param name="customerEnteredPrice">Customer entered price (if specified)</param>
        /// <param name="rentalStartDate">Rental start date (null for not rental products)</param>
        /// <param name="rentalEndDate">Rental end date (null for not rental products)</param>
        /// <param name="includeDiscounts">A value indicating whether include discounts or not for price computation</param>
        /// <param name="discountAmount">Applied discount amount</param>
        /// <param name="appliedDiscount">Applied discount</param>
        /// <returns>Shopping cart unit price (one item)</returns>
        public virtual decimal GetUnitPrice(Product product,
            Customer customer,
            ShoppingCartType shoppingCartType,
            int quantity,
            string attributesXml,
            decimal customerEnteredPrice,
            DateTime? rentalStartDate, DateTime? rentalEndDate,
            bool includeDiscounts,
            out decimal discountAmount,
            out Discount appliedDiscount)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            if (customer == null)
                throw new ArgumentNullException("customer");

            discountAmount = decimal.Zero;
            appliedDiscount = null;

            decimal finalPrice;

            var combination = _productAttributeParser.FindProductAttributeCombination(product, attributesXml);
            if (combination != null && combination.OverriddenPrice.HasValue)
            {
                finalPrice = combination.OverriddenPrice.Value;
            }
            else
            {
                //summarize price of all attributes
                var attributesTotalPrice = decimal.Zero;
                var attributeValues = _productAttributeParser.ParseProductAttributeValues(attributesXml);
                if (attributeValues != null)
                {
                    foreach (var attributeValue in attributeValues)
                    {
                        attributesTotalPrice += GetProductAttributeValuePriceAdjustment(attributeValue);
                    }
                }

                //get price of a product (with previously calculated price of all attributes)
                if (product.CustomerEntersPrice)
                {
                    finalPrice = customerEnteredPrice;
                }
                else
                {
                    int qty;
                    if (_shoppingCartSettings.GroupTierPricesForDistinctShoppingCartItems)
                    {
                        //the same products with distinct product attributes could be stored as distinct "ShoppingCartItem" records
                        //so let's find how many of the current products are in the cart
                        qty = customer.ShoppingCartItems
                            .Where(x => x.ProductId == product.Id)
                            .Where(x => x.ShoppingCartType == shoppingCartType)
                            .Sum(x => x.Quantity);
                        if (qty == 0)
                        {
                            qty = quantity;
                        }
                    }
                    else
                    {
                        qty = quantity;
                    }
                    finalPrice = GetFinalPrice(product,
                        customer,
                        attributesTotalPrice,
                        includeDiscounts,
                        qty,
                        product.IsRental ? rentalStartDate : null,
                        product.IsRental ? rentalEndDate : null,
                        out discountAmount, out appliedDiscount);
                }
            }

            //rounding
            if (_shoppingCartSettings.RoundPricesDuringCalculation)
                finalPrice = RoundingHelper.RoundPrice(finalPrice);

            return finalPrice;
        }

        /// <summary>
        ///     Gets the shopping cart item sub total
        /// </summary>
        /// <param name="shoppingCartItem">The shopping cart item</param>
        /// <param name="includeDiscounts">A value indicating whether include discounts or not for price computation</param>
        /// <returns>Shopping cart item sub total</returns>
        public virtual decimal GetSubTotal(ShoppingCartItem shoppingCartItem,
            bool includeDiscounts = true)
        {
            decimal discountAmount;
            Discount appliedDiscount;
            return GetSubTotal(shoppingCartItem, includeDiscounts, out discountAmount, out appliedDiscount);
        }

        /// <summary>
        ///     Gets the shopping cart item sub total
        /// </summary>
        /// <param name="shoppingCartItem">The shopping cart item</param>
        /// <param name="includeDiscounts">A value indicating whether include discounts or not for price computation</param>
        /// <param name="discountAmount">Applied discount amount</param>
        /// <param name="appliedDiscount">Applied discount</param>
        /// <returns>Shopping cart item sub total</returns>
        public virtual decimal GetSubTotal(ShoppingCartItem shoppingCartItem,
            bool includeDiscounts,
            out decimal discountAmount,
            out Discount appliedDiscount)
        {
            if (shoppingCartItem == null)
                throw new ArgumentNullException("shoppingCartItem");

            decimal subTotal;

            //unit price
            var unitPrice = GetUnitPrice(shoppingCartItem, includeDiscounts,
                out discountAmount, out appliedDiscount);

            //discount
            if (appliedDiscount != null)
            {
                if (appliedDiscount.MaximumDiscountedQuantity.HasValue &&
                    shoppingCartItem.Quantity > appliedDiscount.MaximumDiscountedQuantity.Value)
                {
                    //we cannot apply discount for all shopping cart items
                    var discountedQuantity = appliedDiscount.MaximumDiscountedQuantity.Value;
                    var discountedSubTotal = unitPrice*discountedQuantity;
                    discountAmount = discountAmount*discountedQuantity;

                    var notDiscountedQuantity = shoppingCartItem.Quantity -
                                                appliedDiscount.MaximumDiscountedQuantity.Value;
                    var notDiscountedUnitPrice = GetUnitPrice(shoppingCartItem, false);
                    var notDiscountedSubTotal = notDiscountedUnitPrice*notDiscountedQuantity;

                    subTotal = discountedSubTotal + notDiscountedSubTotal;
                }
                else
                {
                    //discount is applied to all items (quantity)
                    //calculate discount amount for all items
                    discountAmount = discountAmount*shoppingCartItem.Quantity;

                    subTotal = unitPrice*shoppingCartItem.Quantity;
                }
            }
            else
            {
                subTotal = unitPrice*shoppingCartItem.Quantity;
            }
            return subTotal;
        }


        /// <summary>
        ///     Gets the product cost (one item)
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="attributesXml">Shopping cart item attributes in XML</param>
        /// <returns>Product cost (one item)</returns>
        public virtual decimal GetProductCost(Product product, string attributesXml)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            var cost = product.ProductCost;
            var attributeValues = _productAttributeParser.ParseProductAttributeValues(attributesXml);
            foreach (var attributeValue in attributeValues)
            {
                switch (attributeValue.AttributeValueType)
                {
                    case AttributeValueType.Simple:
                    {
                        //simple attribute
                        cost += attributeValue.Cost;
                    }
                        break;
                    case AttributeValueType.AssociatedToProduct:
                    {
                        //bundled product
                        var associatedProduct = _productService.GetProductById(attributeValue.AssociatedProductId);
                        if (associatedProduct != null)
                            cost += associatedProduct.ProductCost*attributeValue.Quantity;
                    }
                        break;
                    default:
                        break;
                }
            }

            return cost;
        }


        /// <summary>
        ///     Get a price adjustment of a product attribute value
        /// </summary>
        /// <param name="value">Product attribute value</param>
        /// <returns>Price adjustment</returns>
        public virtual decimal GetProductAttributeValuePriceAdjustment(ProductAttributeValue value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            var adjustment = decimal.Zero;
            switch (value.AttributeValueType)
            {
                case AttributeValueType.Simple:
                {
                    //simple attribute
                    adjustment = value.PriceAdjustment;
                }
                    break;
                case AttributeValueType.AssociatedToProduct:
                {
                    //bundled product
                    var associatedProduct = _productService.GetProductById(value.AssociatedProductId);
                    if (associatedProduct != null)
                    {
                        adjustment =
                            GetFinalPrice(associatedProduct, _workContext.CurrentCustomer, includeDiscounts: true)*
                            value.Quantity;
                    }
                }
                    break;
                default:
                    break;
            }

            return adjustment;
        }

        #endregion
    }
}