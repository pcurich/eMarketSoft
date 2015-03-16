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
        /// Optiene los plugins por grupos
        /// </summary>
        /// <returns>Lista de las categorias de los plugins</returns>
        IEnumerable<string> GetPluginGroups();

        /// <summary>
        /// Obtiene los plugins
        /// </summary>
        /// <typeparam name="T">Tipo de plugin</typeparam>
        /// <param name="loadMode">Metodo de carga de los plugins</param>
        /// <param name="storeId">Identificador de una tienda</param>
        /// <param name="group">Grupo al que pertenece</param>
        /// <returns>Lista de plugins</returns>
        IEnumerable<T> GetPlugins<T>(LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly,
            int storeId = 0, string group = null) where T : class, IPlugin;

        /// <summary>
        /// Obtiene descriptores de los plugins
        /// </summary>
        /// <param name="loadMode">Modo de carga de los plugins</param>
        /// <param name="storeId">Identificador de la tienda</param>
        /// <param name="group">Grupo al que pertenece</param>
        /// <returns>Lista de descriptores de plugins</returns>
        IEnumerable<PluginDescriptor> GetPluginDescriptors(LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly,
            int storeId = 0, string group = null);

        /// <summary>
        /// Obtiene descriptores de los plugins
        /// </summary>
        /// <typeparam name="T">Tipo del plugin a devolver</typeparam>
        /// <param name="loadMode">Modo de carga de los plugins</param>
        /// <param name="storeId">Identificador de la tienda</param>
        /// <param name="group">Grupo al que pertenece</param>
        /// <returns></returns>
        IEnumerable<PluginDescriptor> GetPluginDescriptors<T>(LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly,
            int storeId = 0, string group = null) where T : class, IPlugin;

        /// <summary>
        /// Obtiene el descriptor de un plugin por el nombre del sistema
        /// </summary>
        /// <param name="systemName">Nombre del sistema</param>
        /// <param name="loadMode">Modo de carga de los plugins</param>
        /// <returns>Descriptor de un plugin</returns>
        PluginDescriptor GetPluginDescriptorBySystemName(string systemName,
            LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly);

        /// <summary>
        /// Obtiene el descriptor de un plugin por el nombre del sistema
        /// </summary>
        /// <typeparam name="T">Tipo del plugin a devolver</typeparam>
        /// <param name="systemName">Nombre del sistema</param>
        /// <param name="loadMode">Modo de carga de los plugins</param>
        /// <returns>Descriptor de un plugin</returns>
        PluginDescriptor GetPluginDescriptorBySystemName<T>(string systemName,
            LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly)
            where T : class, IPlugin;

        /// <summary>
        /// Regarga los plugins
        /// </summary>
        void ReloadPlugins();
    }
}