using System.Collections.Generic;
using Soft.Core.Configuration;

namespace Soft.Core.Domain.Customers
{
    public class ExternalAuthenticationSettings : ISettings
    {
        public ExternalAuthenticationSettings()
        {
            ActiveAuthenticationMethodSystemNames = new List<string>();
        }

        public bool AutoRegisterEnabled { get; set; }

        /// <summary>
        /// Nombre del sistema de metodo de pago
        /// </summary>
        public List<string> ActiveAuthenticationMethodSystemNames { get; set; }
    }
}