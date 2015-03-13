using Soft.Core.Domain.Catalog;

namespace Soft.Data.Mapping.Catalog
{
    public partial class CategoryMap : SoftEntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            ToTable("Category");
            HasKey(c => c.Id);
            Property(c => c.Name).IsRequired().HasMaxLength(400);
            Property(c => c.MetaKeywords).HasMaxLength(400);
            Property(c => c.MetaTitle).HasMaxLength(400);
            Property(c => c.PriceRanges).HasMaxLength(400);
            Property(c => c.PageSizeOptions).HasMaxLength(200);
        }
    }
}