using Soft.Core.Domain.Catalog;

namespace Soft.Data.Mapping.Catalog
{
    public partial class CategoryTemplateMap : SoftEntityTypeConfiguration<CategoryTemplate>
    {
        public CategoryTemplateMap()
        {
            ToTable("CategoryTemplate");
            HasKey(p => p.Id);
            Property(p => p.Name).IsRequired().HasMaxLength(400);
            Property(p => p.ViewPath).IsRequired().HasMaxLength(400);
        }
    }
}