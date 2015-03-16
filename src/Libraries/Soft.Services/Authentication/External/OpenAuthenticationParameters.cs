//Contributor:  Nicholas Mayne

using System;
using System.Collections.Generic;

namespace Soft.Services.Authentication.External
{
    /// <summary>
    /// Parametros para abrir una autenticacion
    /// </summary>
    [Serializable]
    public abstract partial class OpenAuthenticationParameters
    {
        /// <summary>
        /// Nombre del sistema proveedor
        /// </summary>
        public abstract string ProviderSystemName { get; }
        /// <summary>
        /// Identificador externo
        /// </summary>
        public string ExternalIdentifier { get; set; }
        /// <summary>
        /// Identificador externo
        /// </summary>
        public string ExternalDisplayIdentifier { get; set; }
        /// <summary>
        /// Token de Autenticacion
        /// </summary>
        public string OAuthToken { get; set; }
        /// <summary>
        /// Token de acceso 
        /// </summary>
        public string OAuthAccessToken { get; set; }
        /// <summary>
        /// Usuarios de reclamaciones
        /// </summary>
        public virtual IList<UserClaims> UserClaims
        {
            get { return new List<UserClaims>(0); }
        }
    }
}