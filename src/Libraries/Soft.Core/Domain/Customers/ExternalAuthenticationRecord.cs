namespace Soft.Core.Domain.Customers
{
    /// <summary>
    /// Representa una autenticacion externa 
    /// </summary>
    public partial class ExternalAuthenticationRecord : BaseEntity
    {
        /// <summary>
        /// Identificador del cliente
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Email para la autenticacion externa
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Identificador para la autentificacion externa
        /// </summary>
        public string ExternalIdentifier { get; set; }

        /// <summary>
        /// Identificador para la autentificacion externa mostrar
        /// </summary>
        public string ExternalDisplayIdentifier { get; set; }

        /// <summary>
        /// El OAuthToken
        /// </summary>
        public string OAuthToken { get; set; }

        /// <summary>
        /// El OAuthAccessToken
        /// </summary>
        public string OAuthAccessToken { get; set; }

        /// <summary>
        /// El proveedor
        /// </summary>
        public string ProviderSystemName { get; set; }

        /// <summary>
        /// El cliente
        /// </summary>
        public virtual Customer Customer { get; set; }
    }
}