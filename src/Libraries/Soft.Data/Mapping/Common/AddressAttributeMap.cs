using Soft.Core.Domain.Common;

namespace Soft.Data.Mapping.Common
{
    public partial class AddressAttributeMap : SoftEntityTypeConfiguration<AddressAttribute>
    {
        public AddressAttributeMap()
        {
            ToTable("AddressAttribute");
            HasKey(aa => aa.Id);
            Property(aa => aa.Name).IsRequired().HasMaxLength(400);

            Ignore(aa => aa.AttributeControlType);
        }
    }
}