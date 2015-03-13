using Soft.Core.Domain.Catalog;

namespace Soft.Data.Mapping.Catalog
{
    public partial class ProductTemplateMap : SoftEntityTypeConfiguration<ProductTemplate>
    {
        public ProductTemplateMap()
        {
            ToTable("ProductTemplate");
            HasKey(p => p.Id);
            Property(p => p.Name).IsRequired().HasMaxLength(400);
            Property(p => p.ViewPath).IsRequired().HasMaxLength(400);
        }
    }
}