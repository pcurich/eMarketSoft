using Soft.Core.Domain.Tax;

namespace Soft.Data.Mapping.Tax
{
    public class TaxCategoryMap : SoftEntityTypeConfiguration<TaxCategory>
    {
        public TaxCategoryMap()
        {
            ToTable("TaxCategory");
            HasKey(tc => tc.Id);
            Property(tc => tc.Name).IsRequired().HasMaxLength(400);
        }
    }
}
