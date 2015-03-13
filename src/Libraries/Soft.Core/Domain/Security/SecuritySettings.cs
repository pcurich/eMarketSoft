using System.Collections.Generic;
using Soft.Core.Configuration;

namespace Soft.Core.Domain.Security
{
    public class SecuritySettings : ISettings
    {
        /// <summary>
        /// Si todas las paginas se van a forzar a usar SSL (Sin importar el atributo de la clase [SoftHttpsRequirementAttribute]
        /// </summary>
        public bool ForceSslForAllPages { get; set; }

        /// <summary>
        /// Llave de encriptacion 
        /// </summary>
        public string EncryptionKey { get; set; }

        /// <summary>
        /// establece una lista de areas de administracion que permiten IPS
        /// </summary>
        public List<string> AdminAreaAllowedIpAddresses { get; set; }
    }
}