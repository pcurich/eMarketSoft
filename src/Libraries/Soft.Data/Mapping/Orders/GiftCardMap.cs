using Soft.Core.Domain.Orders;

namespace Soft.Data.Mapping.Orders
{
    public partial class GiftCardMap : SoftEntityTypeConfiguration<GiftCard>
    {
        public GiftCardMap()
        {
            ToTable("GiftCard");
            HasKey(gc => gc.Id);

            Property(gc => gc.Amount).HasPrecision(18, 4);

            Ignore(gc => gc.GiftCardType);

            HasOptional(gc => gc.PurchasedWithOrderItem)
                .WithMany(orderItem => orderItem.AssociatedGiftCards)
                .HasForeignKey(gc => gc.PurchasedWithOrderItemId);
        }
    }
}