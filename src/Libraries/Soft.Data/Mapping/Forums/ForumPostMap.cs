using Soft.Core.Domain.Forums;

namespace Soft.Data.Mapping.Forums
{
    public partial class ForumPostMap : SoftEntityTypeConfiguration<ForumPost>
    {
        public ForumPostMap()
        {
            ToTable("Forums_Post");
            HasKey(fp => fp.Id);
            Property(fp => fp.Text).IsRequired();
            Property(fp => fp.IpAddress).HasMaxLength(100);

            HasRequired(fp => fp.ForumTopic)
                .WithMany()
                .HasForeignKey(fp => fp.TopicId);

            HasRequired(fp => fp.Customer)
               .WithMany()
               .HasForeignKey(fp => fp.CustomerId)
               .WillCascadeOnDelete(false);
        }
    }
}
