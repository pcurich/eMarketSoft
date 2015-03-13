using Soft.Core.Domain.Customers;

namespace Soft.Data.Mapping.Customers
{
    public partial class ExternalAuthenticationRecordMap : SoftEntityTypeConfiguration<ExternalAuthenticationRecord>
    {
        public ExternalAuthenticationRecordMap()
        {
            ToTable("ExternalAuthenticationRecord");

            HasKey(ear => ear.Id);

            HasRequired(ear => ear.Customer)
                .WithMany(c => c.ExternalAuthenticationRecords)
                .HasForeignKey(ear => ear.CustomerId);

        }
    }
}