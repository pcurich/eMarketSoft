using Soft.Core.Domain.Directory;

namespace Soft.Data.Mapping.Directory
{
    public partial class CountryMap : SoftEntityTypeConfiguration<Country>
    {
        public CountryMap()
        {
            ToTable("Country");
            HasKey(c =>c.Id);
            Property(c => c.Name).IsRequired().HasMaxLength(100);
            Property(c =>c.TwoLetterIsoCode).HasMaxLength(2);
            Property(c =>c.ThreeLetterIsoCode).HasMaxLength(3);
        }
    }
}