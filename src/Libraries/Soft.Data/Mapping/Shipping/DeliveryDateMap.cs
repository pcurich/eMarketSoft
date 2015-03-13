using Soft.Core.Domain.Shipping;

namespace Soft.Data.Mapping.Shipping
{
    public class DeliveryDateMap : SoftEntityTypeConfiguration<DeliveryDate>
    {
        public DeliveryDateMap()
        {
            ToTable("DeliveryDate");
            HasKey(dd => dd.Id);
            Property(dd => dd.Name).IsRequired().HasMaxLength(400);
        }
    }
}
