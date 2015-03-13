using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soft.Core.Plugins
{
    public abstract class BasePlugin : IPlugin
    {
        public virtual PluginDescriptor PluginDescriptor { get; set; }

        public virtual void Install()
        {
            PluginManager.MarkPluginAsInstalled(PluginDescriptor.SystemName);
        }

        public virtual void Uninstall()
        {
            PluginManager.MarkPluginAsUninstalled(PluginDescriptor.SystemName);
        }
    }
}