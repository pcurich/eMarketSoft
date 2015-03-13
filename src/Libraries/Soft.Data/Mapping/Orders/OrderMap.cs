using Soft.Core.Domain.Orders;

namespace Soft.Data.Mapping.Orders
{
    public partial class OrderMap : SoftEntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            ToTable("Order");
            HasKey(o => o.Id);
            Property(o => o.CurrencyRate).HasPrecision(18, 8);
            Property(o => o.OrderSubtotalInclTax).HasPrecision(18, 4);
            Property(o => o.OrderSubtotalExclTax).HasPrecision(18, 4);
            Property(o => o.OrderSubTotalDiscountInclTax).HasPrecision(18, 4);
            Property(o => o.OrderSubTotalDiscountExclTax).HasPrecision(18, 4);
            Property(o => o.OrderShippingInclTax).HasPrecision(18, 4);
            Property(o => o.OrderShippingExclTax).HasPrecision(18, 4);
            Property(o => o.PaymentMethodAdditionalFeeInclTax).HasPrecision(18, 4);
            Property(o => o.PaymentMethodAdditionalFeeExclTax).HasPrecision(18, 4);
            Property(o => o.OrderTax).HasPrecision(18, 4);
            Property(o => o.OrderDiscount).HasPrecision(18, 4);
            Property(o => o.OrderTotal).HasPrecision(18, 4);
            Property(o => o.RefundedAmount).HasPrecision(18, 4);

            Ignore(o => o.OrderStatus);
            Ignore(o => o.PaymentStatus);
            Ignore(o => o.ShippingStatus);
            Ignore(o => o.CustomerTaxDisplayType);
            Ignore(o => o.TaxRatesDictionary);
            
            HasRequired(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId);
            
            //code below is commented because it causes some issues on big databases - http://www.Softcommerce.com/boards/t/11126/bug-version-20-command-confirm-takes-several-minutes-using-big-databases.aspx
            //HasRequired(o => o.BillingAddress).WithOptional().Map(x => x.MapKey("BillingAddressId")).WillCascadeOnDelete(false);
            //HasOptional(o => o.ShippingAddress).WithOptionalDependent().Map(x => x.MapKey("ShippingAddressId")).WillCascadeOnDelete(false);
            HasRequired(o => o.BillingAddress)
                .WithMany()
                .HasForeignKey(o => o.BillingAddressId)
                .WillCascadeOnDelete(false);
            HasOptional(o => o.ShippingAddress)
                .WithMany()
                .HasForeignKey(o => o.ShippingAddressId)
                .WillCascadeOnDelete(false);
        }
    }
}