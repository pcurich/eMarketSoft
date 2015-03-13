using Soft.Core.Domain.Media;

namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa un producto asoxiado a una imagen
    /// </summary>
    public partial class ProductPicture : BaseEntity
    {
        /// <summary>
        /// Identificador del producto 
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        ///Identificador de una imagen
        /// </summary>
        public int PictureId { get; set; }

        /// <summary>
        /// Orden de aparicion
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Imagen
        /// </summary>
        public virtual Picture Picture { get; set; }

        /// <summary>
        /// Producto
        /// </summary>
        public virtual Product Product { get; set; }
    }
}