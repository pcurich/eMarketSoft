using Soft.Core.Domain.Orders;

namespace Soft.Data.Mapping.Orders
{
    public partial class CheckoutAttributeMap : SoftEntityTypeConfiguration<CheckoutAttribute>
    {
        public CheckoutAttributeMap()
        {
            ToTable("CheckoutAttribute");
            HasKey(ca => ca.Id);
            Property(ca => ca.Name).IsRequired().HasMaxLength(400);

            Ignore(ca => ca.AttributeControlType);
        }
    }
}