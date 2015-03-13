using Soft.Core.Domain.Orders;

namespace Soft.Data.Mapping.Orders
{
    public partial class RecurringPaymentHistoryMap : SoftEntityTypeConfiguration<RecurringPaymentHistory>
    {
        public RecurringPaymentHistoryMap()
        {
            ToTable("RecurringPaymentHistory");
            HasKey(rph => rph.Id);

            HasRequired(rph => rph.RecurringPayment)
                .WithMany(rp => rp.RecurringPaymentHistory)
                .HasForeignKey(rph => rph.RecurringPaymentId);

            //entity framework issue if we have navigation property to 'Order'
            //1. save recurring payment with an order
            //2. save recurring payment history with an order
            //3. update associated order => exception is thrown
        }
    }
}