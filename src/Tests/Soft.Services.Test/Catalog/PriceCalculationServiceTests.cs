using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Soft.Core;
using Soft.Core.Caching;
using Soft.Core.Domain.Catalog;
using Soft.Core.Domain.Customers;
using Soft.Core.Domain.Discounts;
using Soft.Core.Domain.Orders;
using Soft.Core.Domain.Stores;
using Soft.Services.Catalog;
using Soft.Services.Discounts;
using Soft.Test;

namespace Soft.Services.Test.Catalog
{
    [TestFixture]
    public class PriceCalculationServiceTests : ServiceTest
    {
        private ICacheManager _cacheManager;
        private CatalogSettings _catalogSettings;
        private ICategoryService _categoryService;
        private IDiscountService _discountService;
        private IPriceCalculationService _priceCalcService;
        private IProductAttributeParser _productAttributeParser;
        private IProductService _productService;
        private ShoppingCartSettings _shoppingCartSettings;
        private Store _store;
        private IStoreContext _storeContext;
        private IWorkContext _workContext;

        [SetUp]
        public new void SetUp()
        {
            _workContext = null;

            _store = new Store {Id = 1};

            _storeContext = MockRepository.GenerateMock<IStoreContext>();
            _storeContext.Expect(x => x.CurrentStore).Return(_store);

            _discountService = MockRepository.GenerateMock<IDiscountService>();
            _categoryService = MockRepository.GenerateMock<ICategoryService>();
            _productService = MockRepository.GenerateMock<IProductService>();

            _productAttributeParser = MockRepository.GenerateMock<IProductAttributeParser>();

            _shoppingCartSettings = new ShoppingCartSettings();
            _catalogSettings = new CatalogSettings();

            _cacheManager = new SoftNullCache();

            _priceCalcService = new PriceCalculationService(_workContext,
                _storeContext,
                _discountService,
                _categoryService,
                _productAttributeParser,
                _productService,
                _cacheManager,
                _shoppingCartSettings,
                _catalogSettings);
        }

        [Test]
        public void Can_get_final_product_price()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Product name 1",
                Price = 12.34M,
                CustomerEntersPrice = false,
                Published = true
            };

            //customer
            var customer = new Customer();

            _priceCalcService.GetFinalPrice(product, customer, 0, false, 1).ShouldEqual(12.34M);
            _priceCalcService.GetFinalPrice(product, customer, 0, false, 2).ShouldEqual(12.34M);
        }

        [Test]
        public void Can_get_final_product_price_with_tier_prices()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Product name 1",
                Price = 12.34M,
                CustomerEntersPrice = false,
                Published = true
            };

            //Agregar un precio de nivel
            product.TierPrices.Add(new TierPrice
            {
                Price = 10M, //pague dies 
                Quantity = 2, // por llevar 2
                Product = product
            });

            product.TierPrices.Add(new TierPrice
            {
                Price = 8M, //Pague ocho 
                Quantity = 5, //por llevar 5
                Product = product
            });

            //Activo la propiedad
            product.HasTierPrices = true;

            //Cliente
            var customer = new Customer();

            _priceCalcService.GetFinalPrice(product, customer, 0, false, 1).ShouldEqual(12.34M);
            _priceCalcService.GetFinalPrice(product, customer, 0, false, 2).ShouldEqual(10M);
            _priceCalcService.GetFinalPrice(product, customer, 0, false, 3).ShouldEqual(10M);
            _priceCalcService.GetFinalPrice(product, customer, 0, false, 5).ShouldEqual(8M);
        }

        [Test]
        public void Can_get_final_product_price_with_tier_prices_by_customerRole()
        {
            //Producto
            var product = new Product
            {
                Id = 1,
                Name = "Product name 1",
                Price = 12.34M,
                CustomerEntersPrice = false,
                Published = true
            };

            //Roles para un cliente
            var customerRole1 = new CustomerRole
            {
                Id = 1,
                Name = "Rol1",
                Active = true
            };
            var customerRole2 = new CustomerRole
            {
                Id = 2,
                Name = "Rol2",
                Active = true
            };

            //Agrego precio de nivel
            product.TierPrices.Add(new TierPrice
            {
                Price = 10M,
                Quantity = 2,
                Product = product,
                CustomerRole = customerRole1
            });
            product.TierPrices.Add(new TierPrice
            {
                Price = 9M,
                Quantity = 2,
                Product = product,
                CustomerRole = customerRole2
            });
            product.TierPrices.Add(new TierPrice
            {
                Price = 8M,
                Quantity = 5,
                Product = product,
                CustomerRole = customerRole1
            });
            product.TierPrices.Add(new TierPrice
            {
                Price = 5M,
                Quantity = 10,
                Product = product,
                CustomerRole = customerRole2
            });

            //Se avisa que tiene precios de nivel
            product.HasTierPrices = true;

            var customer = new Customer();
            customer.CustomerRoles.Add(customerRole1);

            _priceCalcService.GetFinalPrice(product, customer, 0, false, 1).ShouldEqual(12.34M);
            _priceCalcService.GetFinalPrice(product, customer, 0, false, 2).ShouldEqual(10);
            _priceCalcService.GetFinalPrice(product, customer, 0, false, 3).ShouldEqual(10);
            _priceCalcService.GetFinalPrice(product, customer, 0, false, 5).ShouldEqual(8);
            _priceCalcService.GetFinalPrice(product, customer, 0, false, 10).ShouldEqual(8);
        }

        [Test]
        public void Can_get_final_product_price_with_additionalFee()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Product name 1",
                Price = 12.34M,
                CustomerEntersPrice = false,
                Published = true
            };

            //customer
            var customer = new Customer();

            _priceCalcService.GetFinalPrice(product, customer, 5, false, 1).ShouldEqual(17.34M);
        }

        [Test]
        public void Can_get_final_product_price_with_discount()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Product name 1",
                Price = 12.34M,
                CustomerEntersPrice = false,
                Published = true
            };

            //Cliente
            var customer = new Customer();

            //Descuentos
            var discount1 = new Discount
            {
                Id = 1,
                Name = "Discount 1",
                DiscountType = DiscountType.AssignedToSkus,
                DiscountAmount = 3,
                DiscountLimitation = DiscountLimitationType.Unlimited
            };

            discount1.AppliedToProducts.Add(product);
            product.AppliedDiscounts.Add(discount1);

            //Activa la propiedad de tiene descuento el producto
            product.HasDiscountsApplied = true;

            _discountService.Expect(ds => ds.IsDiscountValid(discount1, customer)).Return(true);
            _discountService.Expect(ds => ds.GetAllDiscounts(DiscountType.AssignedToCategories))
                .Return(new List<Discount>());
            _discountService.Expect(ds => ds.GetDiscountById(discount1.Id)).Return(discount1);

            _priceCalcService.GetFinalPrice(product, customer, 0, true, 1).ShouldEqual(9.34M);
        }

        [Test]
        public void Can_get_final_product_price_with_special_price()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Product name 1",
                Price = 12.34M,
                SpecialPrice = 10.01M,
                SpecialPriceStartDateTimeUtc = DateTime.UtcNow.AddDays(-1),
                SpecialPriceEndDateTimeUtc = DateTime.UtcNow.AddDays(1),
                CustomerEntersPrice = false,
                Published = true
            };

            _discountService.Expect(ds => ds.GetAllDiscounts(DiscountType.AssignedToCategories))
                .Return(new List<Discount>());

            //Cliente
            var customer = new Customer();

            //Fecha valida
            _priceCalcService.GetFinalPrice(product, customer, 0, true, 1).ShouldEqual(10.01M);

            //Fecha Invalida
            product.SpecialPriceStartDateTimeUtc = DateTime.UtcNow.AddDays(1);
            _priceCalcService.GetFinalPrice(product, customer, 0, true, 1).ShouldEqual(12.34M);

            //Sin fechas
            product.SpecialPriceStartDateTimeUtc = null;
            product.SpecialPriceEndDateTimeUtc = null;
            _priceCalcService.GetFinalPrice(product, customer, 0, true, 1).ShouldEqual(10.01M);
        }

        [Test]
        public void Can_get_shopping_cart_item_unitPrice()
        {
            //customer
            var customer = new Customer();

            //shopping cart
            var product1 = new Product
            {
                Id = 1,
                Name = "Product name 1",
                Price = 12.34M,
                CustomerEntersPrice = false,
                Published = true
            };
            var sci1 = new ShoppingCartItem
            {
                Customer = customer,
                CustomerId = customer.Id,
                Product = product1,
                ProductId = product1.Id,
                Quantity = 2
            };

            _discountService.Expect(ds => ds.GetAllDiscounts(DiscountType.AssignedToCategories))
                .Return(new List<Discount>());

            _priceCalcService.GetUnitPrice(sci1).ShouldEqual(12.34);
        }

        [Test]
        public void Can_get_shopping_cart_item_subTotal()
        {
            //Cliente
            var customer = new Customer();

            //Carrito de compras
            var product1 = new Product
            {
                Id = 1,
                Name = "Product name 1",
                Price = 12.34M,
                CustomerEntersPrice = false,
                Published = true
            };

            var sci1 = new ShoppingCartItem
            {
                Customer = customer,
                CustomerId = customer.Id,
                Product = product1,
                ProductId = product1.Id,
                Quantity = 2
            };

            _discountService.Expect(ds => ds.GetAllDiscounts(DiscountType.AssignedToCategories))
                .Return(new List<Discount>());

            _priceCalcService.GetSubTotal(sci1).ShouldEqual(24.68);
        }
    }
}