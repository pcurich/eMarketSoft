using Soft.Core.Configuration;

namespace Soft.Core.Domain.Customers
{
    public class CustomerSettings : ISettings
    {
        /// <summary>
        /// Si el username es usado en vez del email
        /// </summary>
        public bool UsernamesEnabled { get; set; }

        /// <summary>
        /// Si los usuarios pueden hacer ckeck para habilitar el username 
        /// (Cuando se registran o cambian en mi account)
        /// </summary>
        public bool CheckUsernameAvailabilityEnabled { get; set; }

        /// <summary>
        /// Si los usuarios pueden cambiar sus userName
        /// Gets or sets a value indicating whether users are allowed to change their usernames
        /// </summary>
        public bool AllowUsersToChangeUsernames { get; set; }

        /// <summary>
        /// Formato de clave por defecto
        /// </summary>
        public PasswordFormat DefaultPasswordFormat { get; set; }

        /// <summary>
        /// Formato de la constraseña (SHA1, MD5) cuando las contraseñas estan con el algoritmo de hash
        /// </summary>
        public string HashedPasswordFormat { get; set; }

        /// <summary>
        /// Logitud minima de clave
        /// </summary>
        public int PasswordMinLength { get; set; }

        /// <summary>
        /// Tipo de registro del usuario
        /// </summary>
        public UserRegistrationType UserRegistrationType { get; set; }

        /// <summary>
        /// Indica si el usuario permite subir un avatar
        /// </summary>
        public bool AllowCustomersToUploadAvatars { get; set; }

        /// <summary>
        /// Tamaño maximo de avatar (en bytes)
        /// </summary>
        public int AvatarMaximumSizeBytes { get; set; }

        /// <summary>
        /// Si se muestra un avatar por default
        /// </summary>
        public bool DefaultAvatarEnabled { get; set; }

        /// <summary>
        /// Si se muestra la locacion del usuario
        /// </summary>
        public bool ShowCustomersLocation { get; set; }

        /// <summary>
        /// Si se le muestra al usuario desde cuando se ha unido 
        /// </summary>
        public bool ShowCustomersJoinDate { get; set; }

        /// <summary>
        /// Si el usuario puede ver el perfil de otros usuarios
        /// </summary>
        public bool AllowViewingProfiles { get; set; }

        /// <summary>
        /// Si se le avisa al dueño de la tienda sobre los usuarios nuevos mediante notificacion  
        /// </summary>
        public bool NotifyNewCustomerRegistration { get; set; }

        /// <summary>
        /// Si se esconde la opcion de descargar un procucto en el tab de mi cuenta 
        /// </summary>
        public bool HideDownloadableProductsTab { get; set; }

        /// <summary>
        /// Si se esconde volver a la tienda en la suscripcion tab de mi cuenta 
        /// </summary>
        public bool HideBackInStockSubscriptionsTab { get; set; }

        /// <summary>
        /// Si se valida un usuario antes de descargar un producto 
        /// </summary>
        public bool DownloadableProductsValidateUser { get; set; }

        /// <summary>
        /// Formato del nombre del cliente
        /// </summary>
        public CustomerNameFormat CustomerNameFormat { get; set; }

        /// <summary>
        /// Si el formulario de Newsletteresta activo
        /// </summary>
        public bool NewsletterEnabled { get; set; }

        /// <summary>
        /// Si el Newsletter tiene Checkbox esta marcado por defecto en la pagina de registro
        /// </summary>
        public bool NewsletterTickedByDefault { get; set; }

        /// <summary>
        /// Si se esconde el Newsletter
        /// </summary>
        public bool HideNewsletterBlock { get; set; }

        /// <summary>
        /// Numero de minutos para usuarios online 
        /// </summary>
        public int OnlineCustomerMinutes { get; set; }

        /// <summary>
        /// Si se guarda el url de la uLtima pagina visitada para cada cliente
        /// </summary>
        public bool StoreLastVisitedPage { get; set; }

        /// <summary>}
        /// Si se borra el registro de un cliente con el prefijo "-DELETED"
        /// </summary>
        public bool SuffixDeletedCustomers { get; set; }

        #region Campos del formulario

        /// <summary>
        /// Gets or sets a value indicating whether 'Gender' is enabled
        /// </summary>
        public bool GenderEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Date of Birth' is enabled
        /// </summary>
        public bool DateOfBirthEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Company' is enabled
        /// </summary>
        public bool CompanyEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Company' is required
        /// </summary>
        public bool CompanyRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Street address' is enabled
        /// </summary>
        public bool StreetAddressEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Street address' is required
        /// </summary>
        public bool StreetAddressRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Street address 2' is enabled
        /// </summary>
        public bool StreetAddress2Enabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Street address 2' is required
        /// </summary>
        public bool StreetAddress2Required { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Zip / postal code' is enabled
        /// </summary>
        public bool ZipPostalCodeEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Zip / postal code' is required
        /// </summary>
        public bool ZipPostalCodeRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'City' is enabled
        /// </summary>
        public bool CityEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'City' is required
        /// </summary>
        public bool CityRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Country' is enabled
        /// </summary>
        public bool CountryEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Country' is required
        /// </summary>
        public bool CountryRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'State / province' is enabled
        /// </summary>
        public bool StateProvinceEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'State / province' is required
        /// </summary>
        public bool StateProvinceRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Phone number' is enabled
        /// </summary>
        public bool PhoneEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Phone number' is required
        /// </summary>
        public bool PhoneRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Fax number' is enabled
        /// </summary>
        public bool FaxEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Fax number' is required
        /// </summary>
        public bool FaxRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether privacy policy should accepted during registration
        /// </summary>
        public bool AcceptPrivacyPolicyEnabled { get; set; }

        #endregion
    }
}