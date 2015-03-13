using Soft.Core.Domain.Blogs;

namespace Soft.Data.Mapping.Blogs
{
    public partial class BlogCommentMap : SoftEntityTypeConfiguration<BlogComment>
    {
        public BlogCommentMap()
        {
            ToTable("BlogComment");
            HasKey(pr => pr.Id);

            HasRequired(bc => bc.BlogPost)
                .WithMany(bp => bp.BlogComments)
                .HasForeignKey(bc => bc.BlogPostId);

            HasRequired(cc => cc.Customer)
                .WithMany()
                .HasForeignKey(cc => cc.CustomerId);
        }
    }
}