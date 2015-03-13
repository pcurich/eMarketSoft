namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa un producto asociado a una categoria
    /// </summary>
    public partial class ProductCategory : BaseEntity
    {
        /// <summary>
        /// Identificador del producto 
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Identificador de la categoria
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Indica si el producto tiene caracteristicas
        /// </summary>
        public bool IsFeaturedProduct { get; set; }

        /// <summary>
        /// Orden de aparicion 
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        ///Categoria
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        /// Producto
        /// </summary>
        public virtual Product Product { get; set; }
    }
}