using Soft.Core.Domain.Vendors;

namespace Soft.Data.Mapping.Vendors
{
    public partial class VendorMap : SoftEntityTypeConfiguration<Vendor>
    {
        public VendorMap()
        {
            ToTable("Vendor");
            HasKey(v => v.Id);

            Property(v => v.Name).IsRequired().HasMaxLength(400);
            Property(v => v.Email).HasMaxLength(400);
            Property(v => v.MetaKeywords).HasMaxLength(400);
            Property(v => v.MetaTitle).HasMaxLength(400);
            Property(v => v.PageSizeOptions).HasMaxLength(200);
        }
    }
}