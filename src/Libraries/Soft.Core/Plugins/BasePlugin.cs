namespace Soft.Core.Plugins
{
    public abstract class BasePlugin : IPlugin
    {
        /// <summary>
        /// Descriptor del plugin
        /// </summary>
        public virtual PluginDescriptor PluginDescriptor { get; set; }

        /// <summary>
        /// Instala el plugin
        /// </summary>
        public virtual void Install()
        {
            PluginManager.MarkPluginAsInstalled(PluginDescriptor.SystemName);
        }

        /// <summary>
        /// Desistala el Plugin
        /// </summary>
        public virtual void Uninstall()
        {
            PluginManager.MarkPluginAsUninstalled(PluginDescriptor.SystemName);
        }
    }
}