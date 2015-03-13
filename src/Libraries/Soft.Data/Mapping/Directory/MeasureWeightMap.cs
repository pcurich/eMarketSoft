using Soft.Core.Domain.Directory;

namespace Soft.Data.Mapping.Directory
{
    public partial class MeasureWeightMap : SoftEntityTypeConfiguration<MeasureWeight>
    {
        public MeasureWeightMap()
        {
            ToTable("MeasureWeight");
            HasKey(m => m.Id);
            Property(m => m.Name).IsRequired().HasMaxLength(100);
            Property(m => m.SystemKeyword).IsRequired().HasMaxLength(100);
            Property(m => m.Ratio).HasPrecision(18, 8);
        }
    }
}