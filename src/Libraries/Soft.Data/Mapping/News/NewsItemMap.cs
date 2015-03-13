using Soft.Core.Domain.News;

namespace Soft.Data.Mapping.News
{
    public partial class NewsItemMap : SoftEntityTypeConfiguration<NewsItem>
    {
        public NewsItemMap()
        {
            ToTable("News");
            HasKey(bp => bp.Id);
            Property(bp => bp.Title).IsRequired();
            Property(bp => bp.Short).IsRequired();
            Property(bp => bp.Full).IsRequired();
            Property(bp => bp.MetaKeywords).HasMaxLength(400);
            Property(bp => bp.MetaTitle).HasMaxLength(400);
            
            HasRequired(bp => bp.Language)
                .WithMany()
                .HasForeignKey(bp => bp.LanguageId).WillCascadeOnDelete(true);
        }
    }
}