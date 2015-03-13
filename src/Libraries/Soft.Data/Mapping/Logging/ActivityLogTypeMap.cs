using Soft.Core.Domain.Logging;

namespace Soft.Data.Mapping.Logging
{
    public partial class ActivityLogTypeMap : SoftEntityTypeConfiguration<ActivityLogType>
    {
        public ActivityLogTypeMap()
        {
            ToTable("ActivityLogType");
            HasKey(alt => alt.Id);

            Property(alt => alt.SystemKeyword).IsRequired().HasMaxLength(100);
            Property(alt => alt.Name).IsRequired().HasMaxLength(200);
        }
    }
}
