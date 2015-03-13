using System;
using Soft.Core.Domain.Localization;
using Soft.Core.Domain.Security;
using Soft.Core.Domain.Seo;
using Soft.Core.Domain.Stores;

namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa un proveedor
    /// </summary>
    public partial class Manufacturer : BaseEntity, ILocalizedEntity, ISlugSupported, IAclSupported,
        IStoreMappingSupported
    {
        /// <summary>
        /// Nombre del proveedor
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descripcion del proveedor
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Plantilla del proveedor
        /// </summary>
        public int ManufacturerTemplateId { get; set; }

        /// <summary>
        /// Meta Keywords
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Meta descripcion
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Meta Titulo
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// Identificador de imagen
        /// </summary>
        public int PictureId { get; set; }

        /// <summary>
        /// Tamaño de la pagina
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Si los clientes pueden cambiar el tamaño de la pagina
        /// </summary>
        public bool AllowCustomersToSelectPageSize { get; set; }

        /// <summary>
        /// Opciones del tamaño de la pagina
        /// </summary>
        public string PageSizeOptions { get; set; }

        /// <summary>
        /// Rango de los precios 
        /// </summary>
        public string PriceRanges { get; set; }

        /// <summary>
        /// Sujeto a ACL
        /// </summary>
        public bool SubjectToAcl { get; set; }

        /// <summary>
        /// Si el entity esta limitado a una tienda
        /// </summary>
        public bool LimitedToStores { get; set; }

        /// <summary>
        /// Si esta publicado
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Eliminacion logica
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Orden para mostrar
        /// </summary>
        public int DisplayOrder { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime UpdatedOnUtc { get; set; }
    }
}