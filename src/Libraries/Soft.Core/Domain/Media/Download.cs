using System;

namespace Soft.Core.Domain.Media
{
    /// <summary>
    /// Representa una descarga
    /// </summary>
    public partial class Download : BaseEntity
    {
        /// <summary>
        /// GUID de la descarga
        /// </summary>
        public Guid DownloadGuid { get; set; }

        /// <summary>
        /// Si hay una Url de la descarga
        /// </summary>
        public bool UseDownloadUrl { get; set; }

        /// <summary>
        /// URL de la descarga
        /// </summary>
        public string DownloadUrl { get; set; }

        /// <summary>
        /// Descarga en binario
        /// </summary>
        public byte[] DownloadBinary { get; set; }

        /// <summary>
        /// El mime-type de la descarga
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// El nombre del archivo de la descarga
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// La extension
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Si la descarga es nueva
        /// </summary>
        public bool IsNew { get; set; }
    }
}