using System.Collections.Generic;
using Soft.Core.Domain.Catalog;

namespace Soft.Core.Domain.Media
{
    /// <summary>
    /// Representa a una imagen
    /// </summary>
    public partial class Picture : BaseEntity
    {
        private ICollection<ProductPicture> _productPictures;

        /// <summary>
        /// imagen en binario
        /// </summary>
        public byte[] PictureBinary { get; set; }

        /// <summary>
        /// Tipo de mime de la imagen
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        ///SEO friednly para el nombre del archivo de la imagen
        /// </summary>
        public string SeoFilename { get; set; }

        /// <summary>
        /// Indica si la imagen es nueva
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// Productos con sus imagenes 
        /// </summary>
        public virtual ICollection<ProductPicture> ProductPictures
        {
            get { return _productPictures ?? (_productPictures = new List<ProductPicture>()); }
            protected set { _productPictures = value; }
        }
    }
}