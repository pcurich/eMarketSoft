
using Soft.Core.Domain.Orders;

namespace Soft.Data.Mapping.Orders
{
    public partial class GiftCardUsageHistoryMap : SoftEntityTypeConfiguration<GiftCardUsageHistory>
    {
        public GiftCardUsageHistoryMap()
        {
            ToTable("GiftCardUsageHistory");
            HasKey(gcuh => gcuh.Id);
            Property(gcuh => gcuh.UsedValue).HasPrecision(18, 4);
            //Property(gcuh => gcuh.UsedValueInCustomerCurrency).HasPrecision(18, 4);

            HasRequired(gcuh => gcuh.GiftCard)
                .WithMany(gc => gc.GiftCardUsageHistory)
                .HasForeignKey(gcuh => gcuh.GiftCardId);

            HasRequired(gcuh => gcuh.UsedWithOrder)
                .WithMany(o => o.GiftCardUsageHistory)
                .HasForeignKey(gcuh => gcuh.UsedWithOrderId);
        }
    }
}