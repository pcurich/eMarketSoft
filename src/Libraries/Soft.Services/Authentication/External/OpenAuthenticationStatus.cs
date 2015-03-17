//Contributor:  Nicholas Mayne


namespace Soft.Services.Authentication.External
{
    /// <summary>
    /// Open authentication status
    /// </summary>
    public enum OpenAuthenticationStatus
    {
        /// <summary>
        /// Desconocido
        /// </summary>
        Unknown,
        /// <summary>
        /// Error
        /// </summary>
        Error,
        /// <summary>
        /// Autenticado
        /// </summary>
        Authenticated,
        /// <summary>
        /// Requiere redireccionamiento
        /// </summary>
        RequiresRedirect,
        /// <summary>
        /// Asociado con el logon
        /// </summary>
        AssociateOnLogon,
        /// <summary>
        /// Registro automatico por el correo electronico
        /// </summary>
        AutoRegisteredEmailValidation,
        /// <summary>
        /// Registro automatico por la aprobacion del admin
        /// </summary>
        AutoRegisteredAdminApproval,
        /// <summary>
        /// Registro automatico standar
        /// </summary>
        AutoRegisteredStandard,
    }
}