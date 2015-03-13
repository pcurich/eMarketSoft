using System.Collections.Generic;
using Soft.Core.Domain.Localization;

namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa a una opcion de la  especificacion de un atributo
    /// </summary>
    public partial class SpecificationAttributeOption : BaseEntity, ILocalizedEntity
    {
        private ICollection<ProductSpecificationAttribute> _productSpecificationAttributes;

        /// <summary>
        /// Identificador de una especificacion de un atributo
        /// </summary>
        public int SpecificationAttributeId { get; set; }

        /// <summary>
        /// Nombre de la especificacion 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Orden de aparicion
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Especificacion de un atributo
        /// </summary>
        public virtual SpecificationAttribute SpecificationAttribute { get; set; }

        /// <summary>
        /// Gets or sets the product specification attribute
        /// </summary>
        public virtual ICollection<ProductSpecificationAttribute> ProductSpecificationAttributes
        {
            get
            {
                return _productSpecificationAttributes ??
                       (_productSpecificationAttributes = new List<ProductSpecificationAttribute>());
            }
            protected set { _productSpecificationAttributes = value; }
        }
    }
}