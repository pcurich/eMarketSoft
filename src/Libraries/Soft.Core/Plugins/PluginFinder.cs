using System;
using System.Collections.Generic;
using System.Linq;

namespace Soft.Core.Plugins
{
    public class PluginFinder : IPluginFinder
    {
        #region Campos

        private IList<PluginDescriptor> _plugins;
        private bool _arePluginsLoaded;

        #endregion

        #region Util

        /// <summary>
        /// Asegura que el plugin haya sifdo cargado
        /// </summary>
        protected virtual void EnsurePluginsAreLoaded()
        {
            if (!_arePluginsLoaded)
            {
                var foundPlugins = PluginManager.ReferencedPlugins.ToList();
                foundPlugins.Sort();
                _plugins = foundPlugins.ToList();
                _arePluginsLoaded = true;
            }
        }

        /// <summary>
        /// Compruebe si el plugin está disponible en una tienda determinada
        /// </summary>
        /// <param name="pluginDescriptor"></param>
        /// <param name="loadMode"></param>
        /// <returns></returns>
        protected virtual bool CheckLoadMode(PluginDescriptor pluginDescriptor, LoadPluginsMode loadMode)
        {
            if (pluginDescriptor == null)
                throw new ArgumentNullException("pluginDescriptor");

            switch (loadMode)
            {
                case LoadPluginsMode.All:
                    //no se filtran
                    return true;
                case LoadPluginsMode.InstalledOnly:
                    return pluginDescriptor.Installed;
                case LoadPluginsMode.NotInstalledOnly:
                    return !pluginDescriptor.Installed;
                default:
                    throw new Exception("Not supported LoadPluginsMode");
            }
        }

        /// <summary>
        /// Compruebe si el plugin está disponible en un grupo determinado
        /// </summary>
        /// <param name="pluginDescriptor"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        protected virtual bool CheckGroup(PluginDescriptor pluginDescriptor, String group)
        {
            if (pluginDescriptor == null)
                throw new ArgumentNullException("pluginDescriptor");

            if (String.IsNullOrEmpty(group))
                return true;

            return group.Equals(pluginDescriptor.Group, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Verifica si el plugin esta disponible en una determinada tienda
        /// </summary>
        /// <param name="pluginDescriptor"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public bool AuthenticateStore(PluginDescriptor pluginDescriptor, int storeId)
        {
            if (pluginDescriptor == null)
                throw new ArgumentNullException("pluginDescriptor");

            //no requiere autenticacion
            if (storeId == 0)
                return true;

            if (pluginDescriptor.LimitedToStores.Count == 0)
                return true;

            return pluginDescriptor.LimitedToStores.Contains(storeId);
        }


        /// <summary>
        /// Optiene los grupos de los plugins
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetPluginGroups()
        {
            return GetPluginDescriptors(LoadPluginsMode.All).Select(x => x.Group).Distinct().OrderBy(x => x);
        }

        /// <summary>
        /// Optiene los plugins
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="loadMode"></param>
        /// <param name="storeId"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public IEnumerable<T> GetPlugins<T>(
            LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly, int storeId = 0, string group = null)
            where T : class, IPlugin
        {
            return GetPluginDescriptors<T>(loadMode, storeId, group).Select(p => p.Instance<T>());
        }

        /// <summary>
        /// Optiene los descriptores de los plugins
        /// </summary>
        /// <param name="loadMode"></param>
        /// <param name="storeId"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public IEnumerable<PluginDescriptor> GetPluginDescriptors(
            LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly, int storeId = 0, string group = null)
        {
            EnsurePluginsAreLoaded();
            return _plugins.Where(p =>
                CheckLoadMode(p, loadMode) &&
                AuthenticateStore(p, storeId) &&
                CheckGroup(p, group));
        }

        /// <summary>
        /// Optiene los descriptores de los plugins
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="loadMode"></param>
        /// <param name="storeId"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public IEnumerable<PluginDescriptor> GetPluginDescriptors<T>(
            LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly, int storeId = 0, string group = null)
            where T : class, IPlugin
        {
            return GetPluginDescriptors(loadMode, storeId, group)
                .Where(p => typeof (T).IsAssignableFrom(p.PluginType));
        }

        /// <summary>
        /// Optiene los descriptores de los plugins agrupados por el nombre del sistema
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="loadMode"></param>
        /// <returns></returns>
        public PluginDescriptor GetPluginDescriptorBySystemName(string systemName,
            LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly)
        {
            return GetPluginDescriptors(loadMode)
                .SingleOrDefault(p => p.SystemName.Equals(systemName, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Optiene los descriptores de los plugins agrupados por el nombre del sistema
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="systemName"></param>
        /// <param name="loadMode"></param>
        /// <returns></returns>
        public PluginDescriptor GetPluginDescriptorBySystemName<T>(string systemName,
            LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly) where T : class, IPlugin
        {
            return GetPluginDescriptors<T>(loadMode)
                .SingleOrDefault(p => p.SystemName.Equals(systemName, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Recarga los plugins
        /// </summary>
        public void ReloadPlugins()
        {
            _arePluginsLoaded = false;
            EnsurePluginsAreLoaded();
        }

        #endregion
    }
}