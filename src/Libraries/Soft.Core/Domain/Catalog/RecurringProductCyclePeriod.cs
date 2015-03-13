namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa ciclos para productos recurrentes
    /// </summary>
    public enum RecurringProductCyclePeriod
    {
        /// <summary>
        /// Days
        /// </summary>
        Days = 0,

        /// <summary>
        /// Semanas
        /// </summary>
        Weeks = 10,

        /// <summary>
        /// Meses
        /// </summary>
        Months = 20,

        /// <summary>
        /// Años
        /// </summary>
        Years = 30,
    }
}