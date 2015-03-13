namespace Soft.Core.Domain.Discounts
{
    /// <summary>
    /// Representa un tipo de limitacion para el descuento 
    /// </summary>
    public enum DiscountLimitationType
    {
        /// <summary>
        /// Ninguna
        /// </summary>
        Unlimited = 0,

        /// <summary>
        /// N Times Only
        /// </summary>
        NTimesOnly = 15,

        /// <summary>
        /// N Times Por cliente
        /// </summary>
        NTimesPerCustomer = 25,
    }
}