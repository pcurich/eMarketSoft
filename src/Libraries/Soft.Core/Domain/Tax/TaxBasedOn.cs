namespace Soft.Core.Domain.Tax
{
    /// <summary>
    /// Representa la direccion de entregas 
    ///  </summary>
    public enum TaxBasedOn
    {
        /// <summary>
        /// Direccion de facturacion
        /// </summary>
        BillingAddress = 1,

        /// <summary>
        /// Direccion de entrega
        /// </summary>
        ShippingAddress = 2,

        /// <summary>
        /// Direccion por defecto
        /// </summary>
        DefaultAddress = 3,
    }
}