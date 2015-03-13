using System;
using System.Configuration;
using System.Xml;
using Soft.Core.Infrastructure;

namespace Soft.Core.Configuration
{
    public partial class SoftConfig : IConfigurationSectionHandler
    {
        /// <summary>
        /// Ademas de configurar los ensamblados tambien los examina y carga en el directorio bin
        /// </summary>
        public bool DynamicDiscovery { get; private set; }

        /// <summary>
        /// personalizacion <see cref="IEngine"/> para manejar la instancia por default de la aplicacion
        /// </summary>
        public string EngineType { get; private set; }

        /// <summary>
        /// Especifica donde esta almacenados los temas (~/Themes/)
        /// </summary>
        public string ThemeBasePath { get; private set; }

        /// <summary>
        /// Indica si se ignaroan las tareas de inicio
        /// </summary>
        public bool IgnoreStartupTasks { get; private set; }

        /// <summary>
        /// Ruta de la base de datos con usuarios 
        /// </summary>
        public string UserAgentStringsPath { get; private set; }

        public object Create(object parent, object configContext, XmlNode section)
        {
            var config = new SoftConfig();

            //ver los ensamblados
            var dynamicDiscoveryNode = section.SelectSingleNode("DynamicDiscovery");
            if (dynamicDiscoveryNode != null && dynamicDiscoveryNode.Attributes != null)
            {
                var attribute = dynamicDiscoveryNode.Attributes["Enabled"];
                if (attribute != null)
                    config.DynamicDiscovery = Convert.ToBoolean(attribute.Value);
            }

            //Instancia por default de la aplicacion
            var engineNode = section.SelectSingleNode("Engine");
            if (engineNode != null && engineNode.Attributes != null)
            {
                var attribute = engineNode.Attributes["Type"];
                if (attribute != null)
                    config.EngineType = attribute.Value;
            }

            //Tareas a realizarse antes de iniciar el sistema
            var startupNode = section.SelectSingleNode("Startup");
            if (startupNode != null && startupNode.Attributes != null)
            {
                var attribute = startupNode.Attributes["IgnoreStartupTasks"];
                if (attribute != null)
                    config.IgnoreStartupTasks = Convert.ToBoolean(attribute.Value);
            }

            //Directorio base para los temas 
            var themeNode = section.SelectSingleNode("Themes");
            if (themeNode != null && themeNode.Attributes != null)
            {
                var attribute = themeNode.Attributes["basePath"];
                if (attribute != null)
                    config.ThemeBasePath = attribute.Value;
            }

            var userAgentStringsNode = section.SelectSingleNode("UserAgentStrings");
            if (userAgentStringsNode != null && userAgentStringsNode.Attributes != null)
            {
                var attribute = userAgentStringsNode.Attributes["databasePath"];
                if (attribute != null)
                    config.UserAgentStringsPath = attribute.Value;
            }

            return config;
        }
    }
}