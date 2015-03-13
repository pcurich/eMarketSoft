using Soft.Core.Domain.Shipping;

namespace Soft.Data.Mapping.Shipping
{
    public partial class ShipmentMap : SoftEntityTypeConfiguration<Shipment>
    {
        public ShipmentMap()
        {
            ToTable("Shipment");
            HasKey(s => s.Id);

            Property(s => s.TotalWeight).HasPrecision(18, 4);
            
            HasRequired(s => s.Order)
                .WithMany(o => o.Shipments)
                .HasForeignKey(s => s.OrderId);
        }
    }
}