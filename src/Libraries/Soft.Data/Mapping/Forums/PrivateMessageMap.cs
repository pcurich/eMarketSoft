using Soft.Core.Domain.Forums;

namespace Soft.Data.Mapping.Forums
{
    public partial class PrivateMessageMap : SoftEntityTypeConfiguration<PrivateMessage>
    {
        public PrivateMessageMap()
        {
            ToTable("Forums_PrivateMessage");
            HasKey(pm => pm.Id);
            Property(pm => pm.Subject).IsRequired().HasMaxLength(450);
            Property(pm => pm.Text).IsRequired();

            HasRequired(pm => pm.FromCustomer)
               .WithMany()
               .HasForeignKey(pm => pm.FromCustomerId)
               .WillCascadeOnDelete(false);

            HasRequired(pm => pm.ToCustomer)
               .WithMany()
               .HasForeignKey(pm => pm.ToCustomerId)
               .WillCascadeOnDelete(false);
        }
    }
}
