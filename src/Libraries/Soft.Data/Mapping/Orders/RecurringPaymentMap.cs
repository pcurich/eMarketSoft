using Soft.Core.Domain.Orders;

namespace Soft.Data.Mapping.Orders
{
    public partial class RecurringPaymentMap : SoftEntityTypeConfiguration<RecurringPayment>
    {
        public RecurringPaymentMap()
        {
            ToTable("RecurringPayment");
            HasKey(rp => rp.Id);

            Ignore(rp => rp.NextPaymentDate);
            Ignore(rp => rp.CyclesRemaining);
            Ignore(rp => rp.CyclePeriod);



            //HasRequired(rp => rp.InitialOrder).WithOptional().Map(x => x.MapKey("InitialOrderId")).WillCascadeOnDelete(false);
            HasRequired(rp => rp.InitialOrder)
                .WithMany()
                .HasForeignKey(o => o.InitialOrderId)
                .WillCascadeOnDelete(false);
        }
    }
}