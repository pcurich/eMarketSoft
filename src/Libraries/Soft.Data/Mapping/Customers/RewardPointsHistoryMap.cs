using Soft.Core.Domain.Customers;

namespace Soft.Data.Mapping.Customers
{
    public partial class RewardPointsHistoryMap : SoftEntityTypeConfiguration<RewardPointsHistory>
    {
        public RewardPointsHistoryMap()
        {
            ToTable("RewardPointsHistory");
            HasKey(rph => rph.Id);

            Property(rph => rph.UsedAmount).HasPrecision(18, 4);
            //Property(rph => rph.UsedAmountInCustomerCurrency).HasPrecision(18, 4);

            HasRequired(rph => rph.Customer)
                .WithMany(c => c.RewardPointsHistory)
                .HasForeignKey(rph => rph.CustomerId);

            HasOptional(rph => rph.UsedWithOrder)
                .WithOptionalDependent(o => o.RedeemedRewardPointsEntry)
                .WillCascadeOnDelete(false);
        }
    }
}