using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using Soft.Core.Infrastructure;
using Soft.Core.Plugins;

namespace Soft.Web.Framework.Mvc.Routes
{
    /// <summary>
    /// Routers Publicados
    /// </summary>
    public class RoutePublisher : IRoutePublisher
    {
        protected readonly ITypeFinder TypeFinder;

        public RoutePublisher(ITypeFinder typeFinder)
        {
            TypeFinder = typeFinder;
        }

        /// <summary>
        /// Encuentra los desxcriptores de los plugins por algun tipo que este localizable en un ensamblado
        /// </summary>
        /// <param name="providerType">Provider type</param>
        /// <returns>Descriptor del plugin</returns>
        protected virtual PluginDescriptor FindPlugin(Type providerType)
        {
            if (providerType == null)
                throw new ArgumentNullException("providerType");

            foreach (var plugin in PluginManager.ReferencedPlugins)
            {
                if (plugin.ReferencedAssembly == null)
                    continue;

                if (plugin.ReferencedAssembly.FullName == providerType.Assembly.FullName)
                    return plugin;
            }

            return null;
        }

        /// <summary>
        /// Routes registrados
        /// </summary>
        /// <param name="routes">Routes</param>
        public virtual void RegisterRoutes(RouteCollection routes)
        {
            var routeProviderTypes = TypeFinder.FindClassesOfType<IRouteProvider>();
            var routeProviders = new List<IRouteProvider>();
            foreach (var providerType in routeProviderTypes)
            {
                //Ignora los plugins no instalados
                var plugin = FindPlugin(providerType);
                if (plugin != null && !plugin.Installed)
                    continue;

                var provider = Activator.CreateInstance(providerType) as IRouteProvider;
                routeProviders.Add(provider);
            }
            routeProviders = routeProviders.OrderByDescending(rp => rp.Priority).ToList();
            routeProviders.ForEach(rp => rp.RegisterRoutes(routes));
        }

    }
}