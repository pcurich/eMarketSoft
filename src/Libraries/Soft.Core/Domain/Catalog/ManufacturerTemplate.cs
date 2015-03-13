namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Plantilla para los proveedores
    /// </summary>
    public partial class ManufacturerTemplate : BaseEntity
    {
        /// <summary>
        /// Nombre del template
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Muestra el una vista path
        /// </summary>
        public string ViewPath { get; set; }

        /// <summary>
        /// Muestra el orden de aparicion 
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}