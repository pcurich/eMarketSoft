using Soft.Core.Domain.Shipping;

namespace Soft.Data.Mapping.Shipping
{
    public class WarehouseMap : SoftEntityTypeConfiguration<Warehouse>
    {
        public WarehouseMap()
        {
            ToTable("Warehouse");
            HasKey(wh => wh.Id);
            Property(wh => wh.Name).IsRequired().HasMaxLength(400);
        }
    }
}
