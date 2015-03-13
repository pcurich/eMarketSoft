using System.Web.Routing;

namespace Soft.Web.Framework.Mvc.Routes
{
    /// <summary>
    /// Routers publicados 
    /// </summary>
    public interface IRoutePublisher
    {
        /// <summary>
        /// Routes registrados
        /// </summary>
        /// <param name="routes">Routes</param>
        void RegisterRoutes(RouteCollection routes); 
    }
}