using Soft.Core.Domain.Forums;

namespace Soft.Data.Mapping.Forums
{
    public partial class ForumTopicMap : SoftEntityTypeConfiguration<ForumTopic>
    {
        public ForumTopicMap()
        {
            ToTable("Forums_Topic");
            HasKey(ft => ft.Id);
            Property(ft => ft.Subject).IsRequired().HasMaxLength(450);
            Ignore(ft => ft.ForumTopicType);

            HasRequired(ft => ft.Forum)
                .WithMany()
                .HasForeignKey(ft => ft.ForumId);

            HasRequired(ft => ft.Customer)
               .WithMany()
               .HasForeignKey(ft => ft.CustomerId)
               .WillCascadeOnDelete(false);
        }
    }
}
