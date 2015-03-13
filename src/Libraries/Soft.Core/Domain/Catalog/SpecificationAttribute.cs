using System.Collections.Generic;
using Soft.Core.Domain.Localization;

namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa una especificacion de un atributo
    /// </summary>
    public partial class SpecificationAttribute : BaseEntity, ILocalizedEntity
    {
        private ICollection<SpecificationAttributeOption> _specificationAttributeOptions;

        /// <summary>
        /// Nomre de la especificacion de un atributo
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Orden de aparicion
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Opciones de la especificacion de los atributos 
        /// </summary>
        public virtual ICollection<SpecificationAttributeOption> SpecificationAttributeOptions
        {
            get
            {
                return _specificationAttributeOptions ??
                       (_specificationAttributeOptions = new List<SpecificationAttributeOption>());
            }
            protected set { _specificationAttributeOptions = value; }
        }
    }
}