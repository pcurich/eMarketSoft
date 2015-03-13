namespace Soft.Core.Domain.Payments
{
    /// <summary>
    /// Representa un estado de pago 
    /// </summary>
    public enum PaymentStatus
    {
        /// <summary>
        /// Pendiente
        /// </summary>
        Pending = 10,

        /// <summary>
        /// Autorizado
        /// </summary>
        Authorized = 20,

        /// <summary>
        /// Pagado
        /// </summary>
        Paid = 30,

        /// <summary>
        /// Devolucion Parcial
        /// </summary>
        PartiallyRefunded = 35,

        /// <summary>
        /// Devuelto
        /// </summary>
        Refunded = 40,

        /// <summary>
        /// Anulado
        /// </summary>
        Voided = 50,
    }
}