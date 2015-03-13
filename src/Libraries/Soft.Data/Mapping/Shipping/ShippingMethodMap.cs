using Soft.Core.Domain.Shipping;

namespace Soft.Data.Mapping.Shipping
{
    public class ShippingMethodMap : SoftEntityTypeConfiguration<ShippingMethod>
    {
        public ShippingMethodMap()
        {
            ToTable("ShippingMethod");
            HasKey(sm => sm.Id);
            Property(sm => sm.Name).IsRequired().HasMaxLength(400);

            HasMany(sm => sm.RestrictedCountries)
                .WithMany(c => c.RestrictedShippingMethods)
                .Map(m => m.ToTable("ShippingMethodRestrictions"));
        }
    }
}
