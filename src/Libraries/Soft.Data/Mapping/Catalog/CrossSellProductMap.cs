using Soft.Core.Domain.Catalog;

namespace Soft.Data.Mapping.Catalog
{
    public partial class CrossSellProductMap : SoftEntityTypeConfiguration<CrossSellProduct>
    {
        public CrossSellProductMap()
        {
            ToTable("CrossSellProduct");
            HasKey(c => c.Id);
        }
    }
}