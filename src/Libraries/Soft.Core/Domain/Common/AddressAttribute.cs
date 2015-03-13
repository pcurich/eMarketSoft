using System.Collections.Generic;
using Soft.Core.Domain.Catalog;
using Soft.Core.Domain.Localization;

namespace Soft.Core.Domain.Common
{
    public class AddressAttribute : BaseEntity, ILocalizedEntity
    {
        private ICollection<AddressAttributeValue> _addressAttributeValues;

        public string Name { get; set; }
        public bool IsRequired { get; set; }
        public int AttributeControlTypeId { get; set; }
        public int DisplayOrder { get; set; }

        public AttributeControlType AttributeControlType
        {
            get { return (AttributeControlType) AttributeControlTypeId; }
            set { AttributeControlTypeId = (int) value; }
        }

        public virtual ICollection<AddressAttributeValue> AddressAttributeValues
        {
            get
            {
                return _addressAttributeValues ??
                       (_addressAttributeValues = new List<AddressAttributeValue>());
            }
            protected set { _addressAttributeValues = value; }
        }
    }
}