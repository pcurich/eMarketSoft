using Soft.Core.Domain.Orders;

namespace Soft.Data.Mapping.Orders
{
    public partial class ReturnRequestMap : SoftEntityTypeConfiguration<ReturnRequest>
    {
        public ReturnRequestMap()
        {
            ToTable("ReturnRequest");
            HasKey(rr => rr.Id);
            Property(rr => rr.ReasonForReturn).IsRequired();
            Property(rr => rr.RequestedAction).IsRequired();

            Ignore(rr => rr.ReturnRequestStatus);

            HasRequired(rr => rr.Customer)
                .WithMany(c => c.ReturnRequests)
                .HasForeignKey(rr => rr.CustomerId);
        }
    }
}