using Soft.Core.Domain.Localization;
using Soft.Core.Domain.Seo;

namespace Soft.Core.Domain.Vendors
{
    /// <summary>
    /// Representa un vendedor
    /// </summary>
    public class Vendor : BaseEntity, ILocalizedEntity, ISlugSupported
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string AdminComment { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public int DisplayOrder { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public int PageSize { get; set; }
        public bool AllowCustomersToSelectPageSize { get; set; }
        public string PageSizeOptions { get; set; }
    }
}