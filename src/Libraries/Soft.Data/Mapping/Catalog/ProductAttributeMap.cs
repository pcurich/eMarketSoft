using Soft.Core.Domain.Catalog;

namespace Soft.Data.Mapping.Catalog
{
    public partial class ProductAttributeMap : SoftEntityTypeConfiguration<ProductAttribute>
    {
        public ProductAttributeMap()
        {
            ToTable("ProductAttribute");
            HasKey(pa => pa.Id);
            Property(pa => pa.Name).IsRequired();
        }
    }
}