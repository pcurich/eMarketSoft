using Soft.Core.Domain.Forums;

namespace Soft.Data.Mapping.Forums
{
    public partial class ForumGroupMap : SoftEntityTypeConfiguration<ForumGroup>
    {
        public ForumGroupMap()
        {
            ToTable("Forums_Group");
            HasKey(fg => fg.Id);
            Property(fg => fg.Name).IsRequired().HasMaxLength(200);
        }
    }
}
