using System.Collections.Generic;
using Soft.Core.Domain.Directory;
using Soft.Core.Domain.Localization;

namespace Soft.Core.Domain.Shipping
{
    /// <summary>
    /// Representa un metodo de envio utilizado para los métodos de cálculo de tarifas de envío fuera de línea
    /// </summary>
    public class ShippingMethod : BaseEntity, ILocalizedEntity
    {
        private ICollection<Country> _restrictedCountries;
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }

        public virtual ICollection<Country> RestrictedCountries
        {
            get { return _restrictedCountries ?? (_restrictedCountries = new List<Country>()); }
            protected set { _restrictedCountries = value; }
        }
    }
}