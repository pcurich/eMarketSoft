using System.Collections.Generic;
using Soft.Core.Configuration;

namespace Soft.Core.Domain.Seo
{
    /// <summary>
    /// Entorno de SEO
    /// </summary>
    public class SeoSettings : ISettings
    {
        /// <summary>
        /// Separador del titulo
        /// </summary>
        public string PageTitleSeparator { get; set; }

        /// <summary>
        /// Titulo de la pagina ajustada con SEO
        /// </summary>
        public PageTitleSeoAdjustment PageTitleSeoAdjustment { get; set; }

        /// <summary>
        /// Titulo por default
        /// </summary>
        public string DefaultTitle { get; set; }

        /// <summary>
        /// Default META llaves
        /// </summary>
        public string DefaultMetaKeywords { get; set; }

        /// <summary>
        /// Default META descripcion
        /// </summary>
        public string DefaultMetaDescription { get; set; }

        /// <summary>
        /// Indica si la META descripcion de un producto se va a generar automaticamente
        /// </summary>
        public bool GenerateProductMetaDescription { get; set; }

        /// <summary>
        /// Indica si se sebe convertir no occidentales a occidentales caracteres
        /// </summary>
        public bool ConvertNonWesternChars { get; set; }

        /// <summary>
        /// Indica si el UNICO esta activo
        /// </summary>
        public bool AllowUnicodeCharsInUrls { get; set; }

        /// <summary>
        /// Indica si se deben usar URL canonicas
        /// </summary>
        public bool CanonicalUrlsEnabled { get; set; }

        /// <summary>
        /// WWW requiere (with or without WWW)
        /// </summary>
        public WwwRequirement WwwRequirement { get; set; }

        /// <summary>
        /// Indica si los JS file bundling and minification is enabled
        /// </summary>
        public bool EnableJsBundling { get; set; }

        /// <summary>
        /// A value indicating whether CSS file bundling and minification is enabled
        /// </summary>
        public bool EnableCssBundling { get; set; }

        /// <summary>
        /// A value indicating whether Twitter META tags should be generated
        /// </summary>
        public bool TwitterMetaTags { get; set; }

        /// <summary>
        /// A value indicating whether Open Graph META tags should be generated
        /// </summary>
        public bool OpenGraphMetaTags { get; set; }

        /// <summary>
        /// Slugs (sename) reserved for some other needs
        /// </summary>
        public List<string> ReservedUrlRecordSlugs { get; set; }
    }
}