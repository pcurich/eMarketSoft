using Soft.Core.Domain.Messages;

namespace Soft.Data.Mapping.Messages
{
    public partial class QueuedEmailMap : SoftEntityTypeConfiguration<QueuedEmail>
    {
        public QueuedEmailMap()
        {
            ToTable("QueuedEmail");
            HasKey(qe => qe.Id);

            Property(qe => qe.From).IsRequired().HasMaxLength(500);
            Property(qe => qe.FromName).HasMaxLength(500);
            Property(qe => qe.To).IsRequired().HasMaxLength(500);
            Property(qe => qe.ToName).HasMaxLength(500);
            Property(qe => qe.ReplyTo).HasMaxLength(500);
            Property(qe => qe.ReplyToName).HasMaxLength(500);
            Property(qe => qe.CC).HasMaxLength(500);
            Property(qe => qe.Bcc).HasMaxLength(500);
            Property(qe => qe.Subject).HasMaxLength(1000);


            HasRequired(qe => qe.EmailAccount)
                .WithMany()
                .HasForeignKey(qe => qe.EmailAccountId).WillCascadeOnDelete(true);
        }
    }
}