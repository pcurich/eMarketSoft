using Soft.Core.Configuration;

namespace Soft.Core.Domain.Common.Settings
{
    public class PdfSettings : ISettings
    {
        public int LogoPictureId { get; set; }

        public bool LetterPageSizeEnabled { get; set; }

        public bool RenderOrderNotes { get; set; }

        public string FontFileName { get; set; }

        /// <summary>
        /// Indica el texto que aparecera en la parte de abajo de la invocacion
        /// </summary>
        public string InvoiceFooterTextColumn1 { get; set; }

        /// <summary>
        /// Indica el texto que aparecera en la parte de abajo de la invocacion
        /// </summary>
        public string InvoiceFooterTextColumn2 { get; set; }
    }
}