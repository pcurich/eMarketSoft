using Soft.Core.Domain.Discounts;

namespace Soft.Data.Mapping.Discounts
{
    public partial class DiscountUsageHistoryMap : SoftEntityTypeConfiguration<DiscountUsageHistory>
    {
        public DiscountUsageHistoryMap()
        {
            ToTable("DiscountUsageHistory");
            HasKey(duh => duh.Id);
            
            HasRequired(duh => duh.Discount)
                .WithMany()
                .HasForeignKey(duh => duh.DiscountId);

            HasRequired(duh => duh.Order)
                .WithMany(o => o.DiscountUsageHistory)
                .HasForeignKey(duh => duh.OrderId);
        }
    }
}