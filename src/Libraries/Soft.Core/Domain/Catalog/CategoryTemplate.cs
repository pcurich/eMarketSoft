namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa un template de categoria
    /// </summary>
    public partial class CategoryTemplate : BaseEntity
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