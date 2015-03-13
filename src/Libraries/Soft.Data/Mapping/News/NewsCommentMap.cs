using Soft.Core.Domain.News;

namespace Soft.Data.Mapping.News
{
    public partial class NewsCommentMap : SoftEntityTypeConfiguration<NewsComment>
    {
        public NewsCommentMap()
        {
            ToTable("NewsComment");
            HasKey(pr => pr.Id);

            HasRequired(nc => nc.NewsItem)
                .WithMany(n => n.NewsComments)
                .HasForeignKey(nc => nc.NewsItemId);

            HasRequired(cc => cc.Customer)
                .WithMany()
                .HasForeignKey(cc => cc.CustomerId);
        }
    }
}