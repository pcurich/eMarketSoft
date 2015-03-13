using System;
using System.Collections.Generic;
using Soft.Core.Domain.Discounts;
using Soft.Core.Domain.Localization;
using Soft.Core.Domain.Security;
using Soft.Core.Domain.Seo;
using Soft.Core.Domain.Stores;

namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa una categoria
    /// </summary>
    public partial class Category : BaseEntity, ILocalizedEntity, ISlugSupported, IAclSupported, IStoreMappingSupported
    {
        private ICollection<Discount> _appliedDiscounts;

        /// <summary>
        /// Nombre de la categoria
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descripcion de la categoria
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Identificador de la plantilla de las categorias
        /// </summary>
        public int CategoryTemplateId { get; set; }

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
        /// Gets or sets the parent category identifier
        /// </summary>
        /// ///Padre de la categoria 
        public int ParentCategoryId { get; set; }

        /// <summary>
        /// Identificador de la imagen
        /// </summary>
        public int PictureId { get; set; }

        /// <summary>
        /// Tamaño de la pagina
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Indica si el cliente puede modificar el tamaño de la pagina
        /// </summary>
        public bool AllowCustomersToSelectPageSize { get; set; }

        /// <summary>
        /// Opcines disponibles para que el cliente pueda modificar el tamaño de la pagina
        /// </summary>
        public string PageSizeOptions { get; set; }

        /// <summary>
        /// Rangos de precios
        /// </summary>
        public string PriceRanges { get; set; }

        /// <summary>
        /// Si la categoria se muestra en la pagina principal
        /// </summary>
        public bool ShowOnHomePage { get; set; }

        /// <summary>
        /// Si se incluye en el menu superior
        /// </summary>
        public bool IncludeInTopMenu { get; set; }

        /// <summary>
        /// Si a esta categoria se le puede aplicar descuentos
        /// </summary>
        /// <remarks>
        /// Lo mismo a category.AppliedDiscounts.Count > 0
        /// Solo para perfomance
        /// Si es falso no necesita cargarse los descuentos
        /// </remarks>
        public bool HasDiscountsApplied { get; set; }

        /// <summary>
        /// Si esta sujeto a ACL
        /// </summary>
        public bool SubjectToAcl { get; set; }

        /// <summary>
        /// Limitado a tiendas
        /// </summary>
        public bool LimitedToStores { get; set; }

        /// <summary>
        /// Indica si sera publicado
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Eliminacion Logica
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Muestra el orden de aparicion 
        /// </summary>
        public int DisplayOrder { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Coleccion de descuentos aplicados
        /// </summary>
        public virtual ICollection<Discount> AppliedDiscounts
        {
            get { return _appliedDiscounts ?? (_appliedDiscounts = new List<Discount>()); }
            protected set { _appliedDiscounts = value; }
        }
    }
}