using Soft.Core.Domain.Forums;

namespace Soft.Data.Mapping.Forums
{
    public partial class ForumMap : SoftEntityTypeConfiguration<Forum>
    {
        public ForumMap()
        {
            ToTable("Forums_Forum");
            HasKey(f => f.Id);
            Property(f => f.Name).IsRequired().HasMaxLength(200);
            
            HasRequired(f => f.ForumGroup)
                .WithMany(fg => fg.Forums)
                .HasForeignKey(f => f.ForumGroupId);
        }
    }
}
