namespace Soft.Core.Domain.Shipping
{
    /// <summary>
    /// Representa el estatus de envio
    /// </summary>
    public enum ShippingStatus
    {
        /// <summary>
        /// Envio no requerido
        /// </summary>
        ShippingNotRequired = 10,

        /// <summary>
        /// No enviado aun 
        /// </summary>
        NotYetShipped = 20,

        /// <summary>
        /// Pacialmente enviado
        /// </summary>
        PartiallyShipped = 25,

        /// <summary>
        /// Enviado
        /// </summary>
        Shipped = 30,

        /// <summary>
        /// Entregado
        /// </summary>
        Delivered = 40,
    }
}