using Soft.Core.Domain.Localization;

namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa un atributo de un producto
    /// </summary>
    public partial class ProductAttribute : BaseEntity, ILocalizedEntity
    {
        /// <summary>
        /// Nombre
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descripcion
        /// </summary>
        public string Description { get; set; }
    }
}