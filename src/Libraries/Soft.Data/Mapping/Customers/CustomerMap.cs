using Soft.Core.Domain.Customers;

namespace Soft.Data.Mapping.Customers
{
    public partial class CustomerMap : SoftEntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            ToTable("Customer");
            HasKey(c => c.Id);
            Property(u => u.Username).HasMaxLength(1000);
            Property(u => u.Email).HasMaxLength(1000);

            Ignore(u => u.PasswordFormat);

            HasMany(c => c.CustomerRoles)
                .WithMany()
                .Map(m => m.ToTable("Customer_CustomerRole_Mapping"));

            HasMany(c => c.Addresses)
                .WithMany()
                .Map(m => m.ToTable("CustomerAddresses"));
            HasOptional(c => c.BillingAddress);
            HasOptional(c => c.ShippingAddress);
        }
    }
}