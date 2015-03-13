using Soft.Core.Plugins;

namespace Soft.Web.Framework.Menu
{
    public interface IAdminMenuPlugin :IPlugin
    {
        /// <summary>
        /// Autenticacion de usuarios
        /// </summary>
        /// <returns></returns>
        bool Authenticate();

        /// <summary>
        /// Construye item del menu
        /// </summary>
        /// <returns></returns>
        SiteMapNode BuildMenuItem();
    }
}