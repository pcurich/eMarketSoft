using Soft.Core.Domain.Logging;

namespace Soft.Data.Mapping.Logging
{
    public partial class LogMap : SoftEntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            ToTable("Log");
            HasKey(l => l.Id);
            Property(l => l.ShortMessage).IsRequired();
            Property(l => l.IpAddress).HasMaxLength(200);

            Ignore(l => l.LogLevel);

            HasOptional(l => l.Customer)
                .WithMany()
                .HasForeignKey(l => l.CustomerId)
            .WillCascadeOnDelete(true);

        }
    }
}