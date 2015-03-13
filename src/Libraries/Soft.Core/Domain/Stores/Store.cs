using Soft.Core.Domain.Localization;

namespace Soft.Core.Domain.Stores
{
    public class Store : BaseEntity, ILocalizedEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public bool SslEnabled { get; set; }

        /// <summary>
        /// HTTPS
        /// </summary>
        public string SecureUrl { get; set; }

        /// <summary>
        /// Lista separada por comas de los posibles valores de HTTP_HOST
        /// </summary>
        public string Hosts { get; set; }

        public int DisplayOrder { get; set; }
        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public string CompanyTax { get; set; }
    }
}