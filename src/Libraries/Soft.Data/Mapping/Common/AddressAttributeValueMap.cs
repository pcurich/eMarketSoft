using Soft.Core.Domain.Common;

namespace Soft.Data.Mapping.Common
{
    public partial class AddressAttributeValueMap : SoftEntityTypeConfiguration<AddressAttributeValue>
    {
        public AddressAttributeValueMap()
        {
            ToTable("AddressAttributeValue");
            HasKey(aav => aav.Id);
            Property(aav => aav.Name).IsRequired().HasMaxLength(400);

            HasRequired(aav => aav.AddressAttribute)
                .WithMany(aa => aa.AddressAttributeValues)
                .HasForeignKey(aav => aav.AddressAttributeId);
        }
    }
}