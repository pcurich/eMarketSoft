using Soft.Core.Domain.Catalog;

namespace Soft.Data.Mapping.Catalog
{
    public partial class RelatedProductMap : SoftEntityTypeConfiguration<RelatedProduct>
    {
        public RelatedProductMap()
        {
            ToTable("RelatedProduct");
            HasKey(c => c.Id);
        }
    }
}