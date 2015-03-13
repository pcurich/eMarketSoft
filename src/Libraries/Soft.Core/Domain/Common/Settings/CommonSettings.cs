using System.Collections.Generic;
using Soft.Core.Configuration;

namespace Soft.Core.Domain.Common.Settings
{
    public class CommonSettings : ISettings
    {
        public CommonSettings()
        {
            IgnoreLogWordlist = new List<string>();
        }

        public bool UseSystemEmailForContactUsForm { get; set; }
        public bool UseStoredProceduresIfSupported { get; set; }
        public bool HideAdvertisementsOnAdminArea { get; set; }
        public bool SitemapEnabled { get; set; }
        public bool SitemapIncludeCategories { get; set; }
        public bool SitemapIncludeManufacturers { get; set; }
        public bool SitemapIncludeProducts { get; set; }

        /// <summary>
        /// Indica si se debe mostrar una advertencia de warning si java-script esta desactivado
        /// </summary>
        public bool DisplayJavaScriptDisabledWarning { get; set; }

        /// <summary>
        /// Indica si la busqueda se hace con el texto completo
        /// </summary>
        public bool UseFullTextSearch { get; set; }

        /// <summary>
        /// Modo de busqueda completa de texto
        /// </summary>
        public FulltextSearchMode FullTextMode { get; set; }

        /// <summary>
        /// Indica si un error 404 ha sucedido y este debe ser loggeado 
        /// </summary>
        public bool Log404Errors { get; set; }

        /// <summary>
        /// Establece si un delimitador de breadcrumb es usado en el sitio
        /// </summary>
        public string BreadcrumbDelimiter { get; set; }

        /// <summary>
        /// Indica si se debe renderizar el tag <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
        /// </summary>
        public bool RenderXuaCompatible { get; set; }

        /// <summary>
        /// Setea un valor en la etiqueta "X-UA-Compatible"
        /// </summary>
        public string XuaCompatibleValue { get; set; }

        /// <summary>
        /// Establece las palabras que se deben ignorar cuando se loggea mensajes de errores 
        /// </summary>
        public List<string> IgnoreLogWordlist { get; set; }
    }
}