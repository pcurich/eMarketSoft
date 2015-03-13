using Soft.Core.Domain.Customers;

namespace Soft.Data.Mapping.Customers
{
    public partial class CustomerAttributeMap : SoftEntityTypeConfiguration<CustomerAttribute>
    {
        public CustomerAttributeMap()
        {
            ToTable("CustomerAttribute");
            HasKey(ca => ca.Id);
            Property(ca => ca.Name).IsRequired().HasMaxLength(400);

            Ignore(ca => ca.AttributeControlType);
        }
    }
}