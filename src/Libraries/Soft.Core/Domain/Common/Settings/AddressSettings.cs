using Soft.Core.Configuration;

namespace Soft.Core.Domain.Common.Settings
{
    public class AddressSettings : ISettings
    {
        /// <summary>
        /// Indica si la compañia esta activa
        /// </summary>
        public bool CompanyEnabled { get; set; }

        /// <summary>
        /// Si la compañia es requerida
        /// </summary>
        public bool CompanyRequired { get; set; }
        
        /// <summary>
        /// Indica si la direccion esta activa
        /// </summary>
        public bool StreetAddressEnabled { get; set; }

        /// <summary>
        /// Si la direccion es requerida
        /// </summary>
        public bool StreetAddressRequired { get; set; }

        /// <summary>
        /// Indica si la direccion2 esta activa
        /// </summary>
        public bool StreetAddress2Enabled { get; set; }

        /// <summary>
        /// Si la direccion2 es requerida
        /// </summary>
        public bool StreetAddress2Required { get; set; }

        /// <summary>
        /// Indica si el codigo postal esta activo
        /// </summary>
        public bool ZipPostalCodeEnabled { get; set; }

        /// <summary>
        /// Si el codigo es requerido
        /// </summary>
        public bool ZipPostalCodeRequired { get; set; }

        /// <summary>
        /// Indica si la ciudad esta activa
        /// </summary>
        public bool CityEnabled { get; set; }

        /// <summary>
        /// Si la ciudad es requerida
        /// </summary>
        public bool CityRequired { get; set; }

        /// <summary>
        /// Indica si el pais esta activo
        /// </summary>
        public bool CountryEnabled { get; set; }

        /// <summary>
        /// Si el pais esta activo
        /// </summary>
        public bool StateProvinceEnabled { get; set; }

        /// <summary>
        /// Indica si telefono esta activo
        /// </summary>
        public bool PhoneEnabled { get; set; }

        /// <summary>
        /// Si el telefono es requerido
        /// </summary>
        public bool PhoneRequired { get; set; }

        /// <summary>
        /// Indica si el fax esta activo
        /// </summary>
        public bool FaxEnabled { get; set; }

        /// <summary>
        /// Si el fax es requerido
        /// </summary>
        public bool FaxRequired { get; set; }
    }
}