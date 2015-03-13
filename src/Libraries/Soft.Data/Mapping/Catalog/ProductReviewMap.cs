using Soft.Core.Domain.Catalog;

namespace Soft.Data.Mapping.Catalog
{
    public partial class ProductReviewMap : SoftEntityTypeConfiguration<ProductReview>
    {
        public ProductReviewMap()
        {
            ToTable("ProductReview");
            HasKey(pr => pr.Id);

            HasRequired(pr => pr.Product)
                .WithMany(p => p.ProductReviews)
                .HasForeignKey(pr => pr.ProductId);

            HasRequired(pr => pr.Customer)
                .WithMany()
                .HasForeignKey(pr => pr.CustomerId);
        }
    }
}