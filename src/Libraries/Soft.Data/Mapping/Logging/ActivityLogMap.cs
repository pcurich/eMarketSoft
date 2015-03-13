using Soft.Core.Domain.Logging;

namespace Soft.Data.Mapping.Logging
{
    public partial class ActivityLogMap : SoftEntityTypeConfiguration<ActivityLog>
    {
        public ActivityLogMap()
        {
            ToTable("ActivityLog");
            HasKey(al => al.Id);
            Property(al => al.Comment).IsRequired();

            HasRequired(al => al.ActivityLogType)
                .WithMany()
                .HasForeignKey(al => al.ActivityLogTypeId);

            HasRequired(al => al.Customer)
                .WithMany()
                .HasForeignKey(al => al.CustomerId);
        }
    }
}
