using Soft.Core.Domain.Catalog;

namespace Soft.Data.Mapping.Catalog
{
    public partial class TierPriceMap : SoftEntityTypeConfiguration<TierPrice>
    {
        public TierPriceMap()
        {
            ToTable("TierPrice");
            HasKey(tp => tp.Id);
            Property(tp => tp.Price).HasPrecision(18, 4);

            HasRequired(tp => tp.Product)
                .WithMany(p => p.TierPrices)
                .HasForeignKey(tp => tp.ProductId);

            HasOptional(tp => tp.CustomerRole)
                .WithMany()
                .HasForeignKey(tp => tp.CustomerRoleId)
                .WillCascadeOnDelete(true);
        }
    }
}