//Contributor:  Nicholas Mayne

using System.Web.Routing;
using Soft.Core.Plugins;

namespace Soft.Services.Authentication.External
{
    /// <summary>
    /// Proporciona una interfaz para crear métodos de autenticación externos
    /// </summary>
    public partial interface IExternalAuthenticationMethod : IPlugin
    {
        /// <summary>
        /// Obtiene un router para el plugin de configuracion
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues);
        

        /// <summary>
        /// Obtiene un route para mostrar los plugins en la tienda publica
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        void GetPublicInfoRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues);
    }
}
