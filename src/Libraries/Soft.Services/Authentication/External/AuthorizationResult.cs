//Contributor:  Nicholas Mayne

using System.Collections.Generic;

namespace Soft.Services.Authentication.External
{
    /// <summary>
    ///     Resultado de la autorizacion
    /// </summary>
    public class AuthorizationResult
    {
        /// <summary>
        ///     Nueva instancia de la clase <see cref="AuthorizationResult" />
        /// </summary>
        /// <param name="status">The status.</param>
        public AuthorizationResult(OpenAuthenticationStatus status)
        {
            Errors = new List<string>();
            Status = status;
        }

        /// <summary>
        ///     Indica si <see cref="AuthorizationResult" /> es success.
        /// </summary>
        /// <value>
        ///     <c>true</c> Si es success; otros, <c>false</c>.
        /// </value>
        public bool Success
        {
            get { return Errors.Count == 0; }
        }

        /// <summary>
        ///     Estatus de la autenticacion
        /// </summary>
        /// <value>
        ///     El estatus.
        /// </value>
        public OpenAuthenticationStatus Status { get; private set; }

        /// <summary>
        ///     Lista de errores
        /// </summary>
        /// <value>
        ///     Los errores.
        /// </value>
        public IList<string> Errors { get; set; }

        /// <summary>
        ///     Agregar error
        /// </summary>
        /// <param name="error">El error.</param>
        public void AddError(string error)
        {
            Errors.Add(error);
        }
    }
}