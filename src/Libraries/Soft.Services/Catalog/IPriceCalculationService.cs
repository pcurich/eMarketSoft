using System;
using Soft.Core.Domain.Catalog;
using Soft.Core.Domain.Customers;
using Soft.Core.Domain.Discounts;
using Soft.Core.Domain.Orders;

namespace Soft.Services.Catalog
{
    /// <summary>
    ///     Price calculation service
    /// </summary>
    public interface IPriceCalculationService
    {
        /// <summary>
        ///     Me da el precio final
        /// </summary>
        /// <param name="product">Producto</param>
        /// <param name="customer">El cliente</param>
        /// <param name="additionalCharge">Cargos adicionales</param>
        /// <param name="includeDiscounts">Si incluye descuentoo no </param>
        /// <param name="quantity">Cantidad del carrtiro de compras</param>
        /// <returns>Precio final</returns>
        decimal GetFinalPrice(Product product,
            Customer customer,
            decimal additionalCharge = decimal.Zero,
            bool includeDiscounts = true,
            int quantity = 1);

        /// <summary>
        ///     Me da el precio final
        /// </summary>
        /// <param name="product">Producto</param>
        /// <param name="customer">El cliente</param>
        /// <param name="additionalCharge">Cargos adicionales</param>
        /// <param name="includeDiscounts">Si incluye descuentoo no </param>
        /// <param name="quantity">Cantidad del carrtiro de compras</param>
        /// <param name="discountAmount">Aplica cantidad de descuento</param>
        /// <param name="appliedDiscount">Aplica descuento</param>
        /// <returns>Precion Final</returns>
        decimal GetFinalPrice(Product product,
            Customer customer,
            decimal additionalCharge,
            bool includeDiscounts,
            int quantity,
            out decimal discountAmount,
            out Discount appliedDiscount);

        /// <summary>
        ///     Me da el precio final
        /// </summary>
        /// <param name="product">Producto</param>
        /// <param name="customer">El cliente</param>
        /// <param name="additionalCharge">Cargos adicionales</param>
        /// <param name="includeDiscounts">Si incluye descuentoo no </param>
        /// <param name="quantity">Cantidad del carrtiro de compras</param>
        /// <param name="rentalStartDate">Inicio de periodo de renta (Para priductos de alquiler)</param>
        /// <param name="rentalEndDate">Fin de periodo de renta (Para priductos de alquiler)</param>
        /// <param name="discountAmount">Aplica cantidad de descuento</param>
        /// <param name="appliedDiscount">Aplica descuento</param>
        /// <returns>Precion Final</returns>
        decimal GetFinalPrice(Product product,
            Customer customer,
            decimal additionalCharge,
            bool includeDiscounts,
            int quantity,
            DateTime? rentalStartDate,
            DateTime? rentalEndDate,
            out decimal discountAmount,
            out Discount appliedDiscount);

        /// <summary>
        ///     Obtiene el precio unitario del carrito (un item)
        /// </summary>
        /// <param name="shoppingCartItem">Item del carrito</param>
        /// <param name="includeDiscounts">Si incluye desceutno o no</param>
        /// <returns>Precio unitario del carrito (un item)</returns>
        decimal GetUnitPrice(ShoppingCartItem shoppingCartItem, bool includeDiscounts = true);

        /// <summary>
        ///     Obtiene el precio unitario del carrito (un item)
        /// </summary>
        /// <param name="shoppingCartItem">Item del carrito</param>
        /// <param name="includeDiscounts">Si incluye desceutno o no</param>
        /// <param name="discountAmount">Monto del descuento aplicado</param>
        /// <param name="appliedDiscount">Descuento a aplicar</param>
        /// <returns>Precio unitario del carrito (un item)</returns>
        decimal GetUnitPrice(ShoppingCartItem shoppingCartItem,
            bool includeDiscounts,
            out decimal discountAmount,
            out Discount appliedDiscount);

        /// <summary>
        ///     Obtiene el precio unitario del carrito (un item)
        /// </summary>
        /// <param name="product">Producto</param>
        /// <param name="customer">Cliente</param>
        /// <param name="shoppingCartType">Tipo de carrito</param>
        /// <param name="quantity">Cantidad</param>
        /// <param name="attributesXml">tributos del producto (formato XML)</param>
        /// <param name="customerEnteredPrice">Precio ingresado por el cliente (Si se especifica)</param>
        /// ///
        /// <param name="rentalStartDate">Inicio de periodo de renta (Para priductos de alquiler; nulo si o se renta)</param>
        /// <param name="rentalEndDate">Fin de periodo de renta (Para priductos de alquiler; nulo si o se renta)</param>
        /// <param name="includeDiscounts">Si incluye descuento</param>
        /// <param name="discountAmount">Monto de descuento aplicado</param>
        /// <param name="appliedDiscount">Descuento aplicado</param>
        /// <returns>Precio unitario del carrito (un item)</returns>
        decimal GetUnitPrice(Product product,
            Customer customer,
            ShoppingCartType shoppingCartType,
            int quantity,
            string attributesXml,
            decimal customerEnteredPrice,
            DateTime? rentalStartDate, DateTime? rentalEndDate,
            bool includeDiscounts,
            out decimal discountAmount,
            out Discount appliedDiscount);

        /// <summary>
        ///     Obtiene del carrito el subtotal
        /// </summary>
        /// <param name="shoppingCartItem">Iten del carrito de compras</param>
        /// <param name="includeDiscounts">Si incluye descuento</param>
        /// <returns>Sub total del carrito de compras</returns>
        decimal GetSubTotal(ShoppingCartItem shoppingCartItem, bool includeDiscounts = true);

        /// <summary>
        ///     Obtiene del carrito el subtotal
        /// </summary>
        /// <param name="shoppingCartItem">Iten del carrito de compras</param>
        /// <param name="includeDiscounts">Si incluye descuento</param>
        /// <param name="discountAmount">Monto de descuento aplicado</param>
        /// <param name="appliedDiscount">Descuento aplicado</param>
        /// <returns>Sub total del carrito de compras</returns>
        decimal GetSubTotal(ShoppingCartItem shoppingCartItem,
            bool includeDiscounts,
            out decimal discountAmount,
            out Discount appliedDiscount);

        /// <summary>
        ///     Obtiene el costo de un producto
        /// </summary>
        /// <param name="product">Producto</param>
        /// <param name="attributesXml">Atributos del carrito en XML</param>
        /// <returns>Costo de un producto</returns>
        decimal GetProductCost(Product product, string attributesXml);

        /// <summary>
        ///     Ajuste del precio para el valor de un atributo
        /// </summary>
        /// <param name="value">Valor del atributo de un producto</param>
        /// <returns>Precio ajustado</returns>
        decimal GetProductAttributeValuePriceAdjustment(ProductAttributeValue value);
    }
}