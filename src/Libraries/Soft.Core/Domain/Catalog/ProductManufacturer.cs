namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa un producto asociado a un proveedor
    /// </summary>
    public partial class ProductManufacturer : BaseEntity
    {
        /// <summary>
        /// Identificador del producto 
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Identificador del proveedor
        /// </summary>
        public int ManufacturerId { get; set; }

        /// <summary>
        /// Indica si el producto tiene caracteristicas
        /// </summary>
        public bool IsFeaturedProduct { get; set; }

        /// <summary>
        /// Orden de aparicion 
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Proveedor
        /// </summary>
        public virtual Manufacturer Manufacturer { get; set; }

        /// <summary>
        /// Producto
        /// </summary>
        public virtual Product Product { get; set; }
    }
}