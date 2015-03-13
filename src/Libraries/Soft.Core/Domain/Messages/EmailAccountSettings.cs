using Soft.Core.Configuration;

namespace Soft.Core.Domain.Messages
{
    public class EmailAccountSettings : ISettings
    {
        /// <summary>
        /// Identificador de cuenta x default
        /// </summary>
        public int DefaultEmailAccountId { get; set; }
    }
}