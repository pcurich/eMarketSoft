using Soft.Core.Domain.Discounts;

namespace Soft.Data.Mapping.Discounts
{
    public partial class DiscountMap : SoftEntityTypeConfiguration<Discount>
    {
        public DiscountMap()
        {
            ToTable("Discount");
            HasKey(d => d.Id);
            Property(d => d.Name).IsRequired().HasMaxLength(200);
            Property(d => d.CouponCode).HasMaxLength(100);
            Property(d => d.DiscountPercentage).HasPrecision(18, 4);
            Property(d => d.DiscountAmount).HasPrecision(18, 4);

            Ignore(d => d.DiscountType);
            Ignore(d => d.DiscountLimitation);

            HasMany(dr => dr.DiscountRequirements)
                .WithRequired(d => d.Discount)
                .HasForeignKey(dr => dr.DiscountId);

            HasMany(dr => dr.AppliedToCategories)
                .WithMany(c => c.AppliedDiscounts)
                .Map(m => m.ToTable("Discount_AppliedToCategories"));
            
            HasMany(dr => dr.AppliedToProducts)
                .WithMany(p => p.AppliedDiscounts)
                .Map(m => m.ToTable("Discount_AppliedToProducts"));
        }
    }
}