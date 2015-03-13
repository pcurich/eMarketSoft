namespace Soft.Core.Domain.Tax
{
    /// <summary>
    /// Representa un impuesto por categoria
    /// </summary>
    public partial class TaxCategory : BaseEntity
    {
        /// <summary>
        /// Nombre del impuesto
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Orden de aparicion
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}