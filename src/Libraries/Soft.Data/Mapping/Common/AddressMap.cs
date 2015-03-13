using Soft.Core.Domain.Common;

namespace Soft.Data.Mapping.Common
{
    public partial class AddressMap : SoftEntityTypeConfiguration<Address>
    {
        public AddressMap()
        {
            ToTable("Address");
            HasKey(a => a.Id);

            HasOptional(a => a.Country)
                .WithMany()
                .HasForeignKey(a => a.CountryId).WillCascadeOnDelete(false);

            HasOptional(a => a.StateProvince)
                .WithMany()
                .HasForeignKey(a => a.StateProvinceId).WillCascadeOnDelete(false);
        }
    }
}
