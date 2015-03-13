using Soft.Core.Domain.Catalog;

namespace Soft.Data.Mapping.Catalog
{
    public partial class ProductTagMap : SoftEntityTypeConfiguration<ProductTag>
    {
        public ProductTagMap()
        {
            ToTable("ProductTag");
            HasKey(pt => pt.Id);
            Property(pt => pt.Name).IsRequired().HasMaxLength(400);
        }
    }
}