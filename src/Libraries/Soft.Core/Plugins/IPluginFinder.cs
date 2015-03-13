using System.Collections.Generic;

namespace Soft.Core.Plugins
{
    /// <summary>
    /// Buscador de plugin
    /// </summary>
    public interface IPluginFinder
    {
        /// <summary>
        /// Compruebe si el plugin está disponible en una tienda determinada
        /// </summary>
        /// <param name="pluginDescriptor">Descriptor del plugin a revisar</param>
        /// <param name="storeId">Buscar en la tienda</param>
        /// <returns></returns>
        bool AuthenticateStore(PluginDescriptor pluginDescriptor, int storeId);

        /// <summary>
        /// Optiene por grupos los plugins
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetPluginGroups();

        /// <summary>
        /// Obtiene plugins
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="loadMode"></param>
        /// <param name="storeId"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        IEnumerable<T> GetPlugins<T>(LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly,
            int storeId = 0, string group = null) where T : class, IPlugin;

        /// <summary>
        /// Obtiene descriptores de los plugins
        /// </summary>
        /// <param name="loadMode">Modo de carga</param>
        /// <param name="storeId"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        IEnumerable<PluginDescriptor> GetPluginDescriptors(LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly,
            int storeId = 0, string group = null);

        /// <summary>
        /// Obtiene descriptores de los plugins
        /// </summary>
        /// <typeparam name="T">Tipo del plugin a devolver</typeparam>
        /// <param name="loadMode">Modo de carga</param>
        /// <param name="storeId"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        IEnumerable<PluginDescriptor> GetPluginDescriptors<T>(LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly,
            int storeId = 0, string group = null) where T : class, IPlugin;

        /// <summary>
        /// Obtiene el descriptor de un plugin por el nombre del sistema
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="loadMode"></param>
        /// <returns></returns>
        PluginDescriptor GetPluginDescriptorBySystemName(string systemName,
            LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly);

        /// <summary>
        /// Obtiene el descriptor de un plugin por el nombre del sistema
        /// </summary>
        /// <typeparam name="T">Tipo del plugin a devolver</typeparam>
        /// <param name="systemName"></param>
        /// <param name="loadMode"></param>
        /// <returns></returns>
        PluginDescriptor GetPluginDescriptorBySystemName<T>(string systemName,
            LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly)
            where T : class, IPlugin;

        /// <summary>
        /// Regarga los plugins
        /// </summary>
        void ReloadPlugins();
    }
}