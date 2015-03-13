namespace Soft.Core.Plugins
{
    /// <summary>
    /// Interfaz que denota los atributos de los plug-in  que se 
    /// mostraran a travez del editor de interfaces
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Descriptor del plugin
        /// </summary>
        PluginDescriptor PluginDescriptor { get; set; }

        /// <summary>
        /// Instala el plugin
        /// </summary>
        void Install();

        /// <summary>
        /// Desistala el Plugin
        /// </summary>
        void Uninstall();
    }
}