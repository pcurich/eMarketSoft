using Soft.Core.Domain.Blogs;

namespace Soft.Data.Mapping.Blogs
{
    public partial class BlogPostMap : SoftEntityTypeConfiguration<BlogPost>
    {
        public BlogPostMap()
        {
            ToTable("BlogPost");
            HasKey(bp => bp.Id);
            Property(bp => bp.Title).IsRequired();
            Property(bp => bp.Body).IsRequired();
            Property(bp => bp.MetaKeywords).HasMaxLength(400);
            Property(bp => bp.MetaTitle).HasMaxLength(400);

            HasRequired(bp => bp.Language)
                .WithMany()
                .HasForeignKey(bp => bp.LanguageId).WillCascadeOnDelete(true);
        }
    }
}