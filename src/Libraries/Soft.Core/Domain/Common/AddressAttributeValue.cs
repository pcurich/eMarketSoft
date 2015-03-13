using Soft.Core.Domain.Localization;

namespace Soft.Core.Domain.Common
{
    public class AddressAttributeValue : BaseEntity, ILocalizedEntity
    {
        public int AddressAttributeId { get; set; }
        public string Name { get; set; }
        public bool IsPreSelected { get; set; }
        public int DisplayOrder { get; set; }
        public virtual AddressAttribute AddressAttribute { get; set; }
    }
}