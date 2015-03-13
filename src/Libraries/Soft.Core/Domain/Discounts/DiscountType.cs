namespace Soft.Core.Domain.Discounts
{
    /// <summary>
    /// Representa un tipo de descuento
    /// </summary>
    public enum DiscountType
    {
        /// <summary>
        /// Se asigna a total de las ordenes
        /// </summary>
        AssignedToOrderTotal = 1,

        /// <summary>
        /// Asignado al producto (SKUs)
        /// </summary>
        AssignedToSkus = 2,

        /// <summary>
        /// Asignado a las categorias (todos los productos en la categoria)
        /// </summary>
        AssignedToCategories = 5,

        /// <summary>
        /// Asignadio a la compra
        /// </summary>
        AssignedToShipping = 10,

        /// <summary>
        /// Asignado al subtotal de la orden
        /// </summary>
        AssignedToOrderSubTotal = 20,
    }
}