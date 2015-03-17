//Contributor:  Nicholas Mayne


namespace Soft.Services.Authentication.External
{
    /// <summary>
    ///     External authorizer
    /// </summary>
    public interface IExternalAuthorizer
    {
        /// <summary>
        ///     Determina si la autorizacion es correcta
        /// </summary>
        /// <param name="parameters">Parametros para la conexion</param>
        /// <returns>Resultado de la autorizacion </returns>
        AuthorizationResult Authorize(OpenAuthenticationParameters parameters);
    }
}