using Soft.Core.Domain.Catalog;

namespace Soft.Data.Mapping.Catalog
{
    public partial class BackInStockSubscriptionMap : SoftEntityTypeConfiguration<BackInStockSubscription>
    {
        public BackInStockSubscriptionMap()
        {
            ToTable("BackInStockSubscription");
            HasKey(x => x.Id);

            HasRequired(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(true);
            
            HasRequired(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .WillCascadeOnDelete(true);
        }
    }
}