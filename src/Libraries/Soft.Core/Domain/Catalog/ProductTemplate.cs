namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa una plantilla de producto
    /// </summary>
    public partial class ProductTemplate : BaseEntity
    {
        /// <summary>
        /// Nombre de la plantilla
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Vista del path
        /// </summary>
        public string ViewPath { get; set; }

        /// <summary>
        /// Orden de aparicion
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}