namespace Soft.Core.Domain.Customers
{
    /// <summary>
    /// Representa a los mejores clientes
    /// </summary>
    public partial class BestCustomerReportLine
    {
        /// <summary>
        /// Identificador del cliente
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Total de ordenes
        /// </summary>
        public decimal OrderTotal { get; set; }

        /// <summary>
        /// Cantidad de ordenes
        /// </summary>
        public int OrderCount { get; set; }
    }
}