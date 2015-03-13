using Soft.Core.Domain.Tasks;

namespace Soft.Data.Mapping.Tasks
{
    public partial class ScheduleTaskMap : SoftEntityTypeConfiguration<ScheduleTask>
    {
        public ScheduleTaskMap()
        {
            ToTable("ScheduleTask");
            HasKey(t => t.Id);
            Property(t => t.Name).IsRequired();
            Property(t => t.Type).IsRequired();
        }
    }
}