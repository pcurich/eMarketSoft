using Soft.Core.Domain.Stores;

namespace Soft.Data.Mapping.Stores
{
    public partial class StoreMap : SoftEntityTypeConfiguration<Store>
    {
        public StoreMap()
        {
            ToTable("Store");
            HasKey(s => s.Id);
            Property(s => s.Name).IsRequired().HasMaxLength(400);
            Property(s => s.Url).IsRequired().HasMaxLength(400);
            Property(s => s.SecureUrl).HasMaxLength(400);
            Property(s => s.Hosts).HasMaxLength(1000);

            Property(s => s.CompanyName).HasMaxLength(1000);
            Property(s => s.CompanyAddress).HasMaxLength(1000);
            Property(s => s.CompanyPhoneNumber).HasMaxLength(1000);
            Property(s => s.CompanyTax).HasMaxLength(1000);
        }
    }
}