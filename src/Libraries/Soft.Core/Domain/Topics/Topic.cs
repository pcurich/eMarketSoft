using Soft.Core.Domain.Localization;
using Soft.Core.Domain.Seo;
using Soft.Core.Domain.Stores;

namespace Soft.Core.Domain.Topics
{
    /// <summary>
    /// Representa un tema
    /// </summary>
    public class Topic : BaseEntity, ILocalizedEntity, ISlugSupported, IStoreMappingSupported
    {
        public string SystemName { get; set; }
        public bool IncludeInSitemap { get; set; }
        public bool IncludeInTopMenu { get; set; }
        public bool IsPasswordProtected { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }

        public bool LimitedToStores { get; set; }
    }
}