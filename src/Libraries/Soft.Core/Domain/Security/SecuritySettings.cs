using System.Collections.Generic;
using Soft.Core.Configuration;

namespace Soft.Core.Domain.Security
{
    public class SecuritySettings : ISettings
    {
        public bool ForceSslForAllPages { get; set; }
        public string EncryptionKey { get; set; }

        /// <summary>
        /// establece una lista de areas de administracion que permiten IPS
        /// </summary>
        public List<string> AdminAreaAllowedIpAddresses { get; set; }
    }
}