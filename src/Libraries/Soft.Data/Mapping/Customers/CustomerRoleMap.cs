using Soft.Core.Domain.Customers;

namespace Soft.Data.Mapping.Customers
{
    public partial class CustomerRoleMap : SoftEntityTypeConfiguration<CustomerRole>
    {
        public CustomerRoleMap()
        {
            ToTable("CustomerRole");
            HasKey(cr => cr.Id);
            Property(cr => cr.Name).IsRequired().HasMaxLength(255);
            Property(cr => cr.SystemName).HasMaxLength(255);
        }
    }
}