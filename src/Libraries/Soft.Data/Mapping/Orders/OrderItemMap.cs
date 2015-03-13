using Soft.Core.Domain.Orders;

namespace Soft.Data.Mapping.Orders
{
    public partial class OrderItemMap : SoftEntityTypeConfiguration<OrderItem>
    {
        public OrderItemMap()
        {
            ToTable("OrderItem");
            HasKey(orderItem => orderItem.Id);
            Property(orderItem => orderItem.AttributeDescription);
            Property(orderItem => orderItem.AttributesXml);

            Property(orderItem => orderItem.UnitPriceInclTax).HasPrecision(18, 4);
            Property(orderItem => orderItem.UnitPriceExclTax).HasPrecision(18, 4);
            Property(orderItem => orderItem.PriceInclTax).HasPrecision(18, 4);
            Property(orderItem => orderItem.PriceExclTax).HasPrecision(18, 4);
            Property(orderItem => orderItem.DiscountAmountInclTax).HasPrecision(18, 4);
            Property(orderItem => orderItem.DiscountAmountExclTax).HasPrecision(18, 4);
            Property(orderItem => orderItem.OriginalProductCost).HasPrecision(18, 4);
            Property(orderItem => orderItem.ItemWeight).HasPrecision(18, 4);


            HasRequired(orderItem => orderItem.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(orderItem => orderItem.OrderId);

            HasRequired(orderItem => orderItem.Product)
                .WithMany()
                .HasForeignKey(orderItem => orderItem.ProductId);
        }
    }
}