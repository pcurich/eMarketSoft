using Soft.Core.Domain.Affiliates;

namespace Soft.Data.Mapping.Affiliates
{
    public partial class AffiliateMap : SoftEntityTypeConfiguration<Affiliate>
    {
        public AffiliateMap()
        {
            ToTable("Affiliate");
            HasKey(a => a.Id);

            HasRequired(a => a.Address).WithMany().HasForeignKey(x => x.AddressId).WillCascadeOnDelete(false);
        }
    }
}