using Soft.Core.Domain.Discounts;

namespace Soft.Data.Mapping.Discounts
{
    public partial class DiscountRequirementMap : SoftEntityTypeConfiguration<DiscountRequirement>
    {
        public DiscountRequirementMap()
        {
            ToTable("DiscountRequirement");
            HasKey(dr => dr.Id);
        }
    }
}