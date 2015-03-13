using Soft.Core.Domain.Orders;

namespace Soft.Data.Mapping.Orders
{
    public partial class OrderNoteMap : SoftEntityTypeConfiguration<OrderNote>
    {
        public OrderNoteMap()
        {
            ToTable("OrderNote");
            HasKey(on => on.Id);
            Property(on => on.Note).IsRequired();

            HasRequired(on => on.Order)
                .WithMany(o => o.OrderNotes)
                .HasForeignKey(on => on.OrderId);
        }
    }
}