using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Soft.Core.Infrastructure;

namespace Soft.Core.Plugins
{
    public sealed class PluginDescriptor : IComparable<PluginDescriptor>
    {
        public PluginDescriptor()
        {
            SupportedVersions = new List<string>();
            LimitedToStores = new List<int>();
        }


        public PluginDescriptor(Assembly referencedAssembly, FileInfo originalAssemblyFile, Type pluginType)
            : this()
        {
            ReferencedAssembly = referencedAssembly;
            OriginalAssemblyFile = originalAssemblyFile;
            PluginType = pluginType;
        }

        public string PluginFileName { get; set; }

        public Type PluginType { get; set; }

        /// <summary>
        /// El asembler que a sido copiado y activado en la aplicaicon
        /// </summary>
        public Assembly ReferencedAssembly { get; internal set; }

        /// <summary>
        /// El archivo assamblado original que se hace una copia de el
        /// </summary>
        public FileInfo OriginalAssemblyFile { get; internal set; }

        public string Group { get; set; }

        public string FriendlyName { get; set; }

        public string SystemName { get; set; }

        public string Version { get; set; }

        public IList<string> SupportedVersions { get; set; }

        public string Author { get; set; }

        public int DisplayOrder { get; set; }

        /// <summary>
        /// Lista de los identificadores de para los que este plugin esta disponible, si es nulo
        /// estara disponible para todos
        /// </summary>
        public IList<int> LimitedToStores { get; set; }

        /// <summary>
        /// Indica si esta instalado
        /// </summary>
        public bool Installed { get; set; }

        public T Instance<T>() where T : class, IPlugin
        {
            object instance;
            if (!EngineContext.Current.ContainerManager.TryResolve(PluginType, null, out instance))
            {
                //no resuelto
                instance = EngineContext.Current.ContainerManager.ResolveUnregistered(PluginType);
            }

            var typedInstance = instance as T;

            if (typedInstance != null)
                typedInstance.PluginDescriptor = this;

            return typedInstance;
        }

        public IPlugin Instance()
        {
            return Instance<IPlugin>();
        }

        public int CompareTo(PluginDescriptor other)
        {
            return DisplayOrder != other.DisplayOrder
                ? DisplayOrder.CompareTo(other.DisplayOrder)
                : String.Compare(FriendlyName, other.FriendlyName, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return FriendlyName;
        }

        public override bool Equals(object obj)
        {
            var other = obj as PluginDescriptor;
            return other != null && SystemName != null && SystemName.Equals(other.SystemName);
        }

        public override int GetHashCode()
        {
            return SystemName.GetHashCode();
        }
    }
}