using Soft.Core.Domain.Catalog;

namespace Soft.Data.Mapping.Catalog
{
    public partial class SpecificationAttributeMap : SoftEntityTypeConfiguration<SpecificationAttribute>
    {
        public SpecificationAttributeMap()
        {
            ToTable("SpecificationAttribute");
            HasKey(sa => sa.Id);
            Property(sa => sa.Name).IsRequired();
        }
    }
}