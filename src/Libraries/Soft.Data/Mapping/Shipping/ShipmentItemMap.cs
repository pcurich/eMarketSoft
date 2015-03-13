using Soft.Core.Domain.Shipping;

namespace Soft.Data.Mapping.Shipping
{
    public partial class ShipmentItemMap : SoftEntityTypeConfiguration<ShipmentItem>
    {
        public ShipmentItemMap()
        {
            ToTable("ShipmentItem");
            HasKey(si => si.Id);

            HasRequired(si => si.Shipment)
                .WithMany(s => s.ShipmentItems)
                .HasForeignKey(si => si.ShipmentId);
        }
    }
}