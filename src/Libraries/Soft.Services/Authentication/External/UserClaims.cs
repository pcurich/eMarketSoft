//Contributor:  Nicholas Mayne

using System;

namespace Soft.Services.Authentication.External
{
    /// <summary>
    ///     Usuario de relacmaciones
    /// </summary>
    [Serializable]
    public class UserClaims
    {
        /// <summary>
        ///     Fecha de cumpleaños
        /// </summary>
        public BirthDateClaims BirthDate { get; set; }

        /// <summary>
        ///     Contacto
        /// </summary>
        public ContactClaims Contact { get; set; }

        /// <summary>
        ///     Preferencias
        /// </summary>
        public PreferenceClaims Preferences { get; set; }

        /// <summary>
        ///     Nombre
        /// </summary>
        public NameClaims Name { get; set; }

        /// <summary>
        ///     Persona
        /// </summary>
        public PersonClaims Person { get; set; }

        /// <summary>
        ///     Esta firmado por el proveedor
        /// </summary>
        public bool IsSignedByProvider { get; set; }

        /// <summary>
        ///     Version
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        ///     Comapñia
        /// </summary>
        public CompanyClaims Company { get; set; }

        /// <summary>
        ///     Medios
        /// </summary>
        public MediaClaims Media { get; set; }
    }

    /// <summary>
    ///     Image claims
    /// </summary>
    [Serializable]
    public class ImageClaims
    {
        /// <summary>
        ///     Aspect11
        /// </summary>
        public string Aspect11 { get; set; }

        /// <summary>
        ///     Aspect34
        /// </summary>
        public string Aspect34 { get; set; }

        /// <summary>
        ///     Aspect43
        /// </summary>
        public string Aspect43 { get; set; }

        /// <summary>
        ///     Default
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        ///     FavIcon
        /// </summary>
        public string FavIcon { get; set; }
    }

    /// <summary>
    ///     Media reclamaciones
    /// </summary>
    [Serializable]
    public class MediaClaims
    {
        /// <summary>
        ///     Audio de saludos
        /// </summary>
        public string AudioGreeting { get; set; }

        /// <summary>
        ///     Nombre hablado
        /// </summary>
        public string SpokenName { get; set; }

        /// <summary>
        ///     Video saludos
        /// </summary>
        public string VideoGreeting { get; set; }

        /// <summary>
        ///     Imagen
        /// </summary>
        public ImageClaims Images { get; set; }
    }

    /// <summary>
    ///     Web reclamaciones
    /// </summary>
    [Serializable]
    public class WebClaims
    {
        /// <summary>
        ///     Amazon
        /// </summary>
        public string Amazon { get; set; }

        /// <summary>
        ///     Blog
        /// </summary>
        public string Blog { get; set; }

        /// <summary>
        ///     Delicious
        /// </summary>
        public string Delicious { get; set; }

        /// <summary>
        ///     Flickr
        /// </summary>
        public string Flickr { get; set; }

        /// <summary>
        ///     Homepage
        /// </summary>
        public string Homepage { get; set; }

        /// <summary>
        ///     LinkedIn
        /// </summary>
        public string LinkedIn { get; set; }
    }

    /// <summary>
    ///     Telefono de reclamaciones
    /// </summary>
    [Serializable]
    public class TelephoneClaims
    {
        /// <summary>
        ///     Fax
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        ///     Hogar
        /// </summary>
        public string Home { get; set; }

        /// <summary>
        ///     Movil
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        ///     Preferido
        /// </summary>
        public string Preferred { get; set; }

        /// <summary>
        ///     Trabajo
        /// </summary>
        public string Work { get; set; }
    }

    /// <summary>
    ///     Mensajeria instantane de reclamaciones
    /// </summary>
    [Serializable]
    public class InstantMessagingClaims
    {
        /// <summary>
        ///     AOL
        /// </summary>
        public string Aol { get; set; }

        /// <summary>
        ///     ICQ
        /// </summary>
        public string Icq { get; set; }

        /// <summary>
        ///     JABBER
        /// </summary>
        public string Jabber { get; set; }

        /// <summary>
        ///     MSN
        /// </summary>
        public string Msn { get; set; }

        /// <summary>
        ///     SKYPE
        /// </summary>
        public string Skype { get; set; }

        /// <summary>
        ///     YAHOO
        /// </summary>
        public string Yahoo { get; set; }
    }

    /// <summary>
    ///     compañia de reclamaciones
    /// </summary>
    [Serializable]
    public class CompanyClaims
    {
        /// <summary>
        ///     Nombre de compañia
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        ///     Titulo de trabajo
        /// </summary>
        public string JobTitle { get; set; }
    }

    /// <summary>
    ///     Direccion de reclamaciones
    /// </summary>
    [Serializable]
    public class AddressClaims
    {
        /// <summary>
        ///     Direccion simple
        /// </summary>
        public string SingleLineAddress { get; set; }

        /// <summary>
        ///     Nombre a mostrar
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     Host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        ///     Usuario
        /// </summary>
        public string User { get; set; }

        /// <summary>
        ///     Ciudad
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///     Pais
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        ///     Codigo postal
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        ///     Estado
        /// </summary>
        public string State { get; set; }

        /// <summary>
        ///     Direccion 1
        /// </summary>
        public string StreetAddressLine1 { get; set; }

        /// <summary>
        ///     Direccion 2
        /// </summary>
        public string StreetAddressLine2 { get; set; }
    }

    /// <summary>
    ///     Persona reclamacinoes
    /// </summary>
    [Serializable]
    public class PersonClaims
    {
        /// <summary>
        ///     Genero
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        ///     Biografia
        /// </summary>
        public string Biography { get; set; }
    }

    /// <summary>
    ///     Nombre de reclamaciones
    /// </summary>
    [Serializable]
    public class NameClaims
    {
        /// <summary>
        ///     Nombre completo
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        ///     Nick
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        ///     Alias
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        ///     Primer nombre
        /// </summary>
        public string First { get; set; }

        /// <summary>
        ///     Apellido
        /// </summary>
        public string Last { get; set; }

        /// <summary>
        ///     Nombre corto
        /// </summary>
        public string Middle { get; set; }

        /// <summary>
        ///     Profijo
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        ///     Sufijo
        /// </summary>
        public string Suffix { get; set; }
    }

    /// <summary>
    ///     preferencia de reclamaciones
    /// </summary>
    [Serializable]
    public class PreferenceClaims
    {
        /// <summary>
        ///     Lenguaje
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        ///     Lenguaje principal
        /// </summary>
        public string PrimaryLanguage { get; set; }

        /// <summary>
        ///     Zona horaria
        /// </summary>
        public string TimeZone { get; set; }
    }

    /// <summary>
    ///     Contacto de reclamaciones
    /// </summary>
    [Serializable]
    public class ContactClaims
    {
        /// <summary>
        ///     Correo electronico de reclamaciones
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Direccion
        /// </summary>
        public AddressClaims Address { get; set; }

        /// <summary>
        ///     Mensajeria instantanea
        /// </summary>
        public InstantMessagingClaims Im { get; set; }

        /// <summary>
        ///     Telefono de reclamos
        /// </summary>
        public TelephoneClaims Phone { get; set; }

        /// <summary>
        ///     Web de reclamaciones
        /// </summary>
        public WebClaims Web { get; set; }

        /// <summary>
        ///     Direccion de correos
        /// </summary>
        public AddressClaims MailAddress { get; set; }

        /// <summary>
        ///     Direccion de trabajo
        /// </summary>
        public AddressClaims WorkAddress { get; set; }
    }

    /// <summary>
    ///     Birth date claims
    /// </summary>
    [Serializable]
    public class BirthDateClaims
    {
        /// <summary>
        ///     Dia del mes
        /// </summary>
        public int DayOfMonth { get; set; }

        /// <summary>
        ///     Mes
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        ///     Fecha de nacimiento completa
        /// </summary>
        public DateTime? WholeBirthDate { get; set; }

        /// <summary>
        ///     Año
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        ///     Celda
        /// </summary>
        public string Raw { get; set; }

        /// <summary>
        ///     Generacion de fecha de nacimiento
        /// </summary>
        public DateTime GeneratedBirthDate
        {
            get { return new DateTime(Year, Month, DayOfMonth); }
        }
    }
}