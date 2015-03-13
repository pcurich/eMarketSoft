namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa un rango de precios
    /// 
    /// </summary>
    public partial class PriceRange
    {
        /// <summary>
        ///     Desde
        /// </summary>
        public decimal? From { get; set; }

        /// <summary>
        /// Hast
        /// </summary>
        public decimal? To { get; set; }
    }
}