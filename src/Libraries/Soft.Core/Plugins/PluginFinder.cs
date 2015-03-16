using System;
using System.Collections.Generic;
using System.Linq;

namespace Soft.Core.Plugins
{
    public class PluginFinder : IPluginFinder
    {
        #region Propiedades

        private IList<PluginDescriptor> _plugins;
        private bool _arePluginsLoaded;

        #endregion

        #region Metodos

        /// <summary>
        /// Compruebe si el plugin está disponible en una tienda determinada
        /// </summary>
        /// <param name="pluginDescriptor">Descriptor del plugin a revisar</param>
        /// <param name="storeId">Buscar en la tienda</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">pluginDescriptor</exception>
        public bool AuthenticateStore(PluginDescriptor pluginDescriptor, int storeId)
        {
            if (pluginDescriptor == null)
                throw new ArgumentNullException("pluginDescriptor");

            //no requiere autenticacion
            if (storeId == 0)
                return true;

            return pluginDescriptor.LimitedToStores.Count == 0 || 
                pluginDescriptor.LimitedToStores.Contains(storeId);
        }


        public IEnumerable<string> GetPluginGroups()
        {
            return GetPluginDescriptors(LoadPluginsMode.All).Select(x => x.Group).Distinct().OrderBy(x => x);
        }


        public IEnumerable<T> GetPlugins<T>(
            LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly, int storeId = 0, string group = null)
            where T : class, IPlugin
        {
            return GetPluginDescriptors<T>(loadMode, storeId, group).Select(p => p.Instance<T>());
        }

        /// <summary>
        /// Obtiene descriptores de los plugins
        /// </summary>
        /// <param name="loadMode">Modo de carga de los plugins</param>
        /// <param name="storeId">Identificador de la tienda</param>
        /// <param name="group">Grupo al que pertenece</param>
        /// <returns>
        /// Lista de descriptores de plugins
        /// </returns>
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
        /// Obtiene descriptores de los plugins
        /// </summary>
        /// <typeparam name="T">Tipo del plugin a devolver</typeparam>
        /// <param name="loadMode">Modo de carga de los plugins</param>
        /// <param name="storeId">Identificador de la tienda</param>
        /// <param name="group">Grupo al que pertenece</param>
        /// <returns></returns>
        public IEnumerable<PluginDescriptor> GetPluginDescriptors<T>(
            LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly, int storeId = 0, string group = null)
            where T : class, IPlugin
        {
            return GetPluginDescriptors(loadMode, storeId, group)
                .Where(p => typeof (T).IsAssignableFrom(p.PluginType));
        }

        /// <summary>
        /// Obtiene el descriptor de un plugin por el nombre del sistema
        /// </summary>
        /// <param name="systemName">Nombre del sistema</param>
        /// <param name="loadMode">Modo de carga de los plugins</param>
        /// <returns>
        /// Descriptor de un plugin
        /// </returns>
        public PluginDescriptor GetPluginDescriptorBySystemName(string systemName,
            LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly)
        {
            return GetPluginDescriptors(loadMode)
                .SingleOrDefault(p => p.SystemName.Equals(systemName, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Obtiene el descriptor de un plugin por el nombre del sistema
        /// </summary>
        /// <typeparam name="T">Tipo del plugin a devolver</typeparam>
        /// <param name="systemName">Nombre del sistema</param>
        /// <param name="loadMode">Modo de carga de los plugins</param>
        /// <returns>
        /// Descriptor de un plugin
        /// </returns>
        public PluginDescriptor GetPluginDescriptorBySystemName<T>(string systemName,
            LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly) where T : class, IPlugin
        {
            return GetPluginDescriptors<T>(loadMode)
                .SingleOrDefault(p => p.SystemName.Equals(systemName, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Regarga los plugins
        /// </summary>
        public void ReloadPlugins()
        {
            _arePluginsLoaded = false;
            EnsurePluginsAreLoaded();
        }

        #endregion

        #region Util

        /// <summary>
        /// Asegura que el plugin haya sido cargado
        /// </summary>
        protected virtual void EnsurePluginsAreLoaded()
        {
            if (_arePluginsLoaded)
                return;

            var foundPlugins = PluginManager.ReferencedPlugins.ToList();
            foundPlugins.Sort();
            _plugins = foundPlugins.ToList();
            _arePluginsLoaded = true;
        }

        /// <summary>
        /// Compruebe si el plugin está disponible en una 
        /// tienda determinada
        /// </summary>
        /// <param name="pluginDescriptor">Descriptor del plugin</param>
        /// <param name="loadMode">Metodo de Carga</param>
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
                    throw new Exception("No soporta el modo de carga del plugin");
            }
        }

        /// <summary>
        /// Compruebe si el plugin está disponible en un grupo determinado
        /// </summary>
        /// <param name="pluginDescriptor">Descriptor de un plugin</param>
        /// <param name="group">Grupo de un plugin</param>
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
    }
}