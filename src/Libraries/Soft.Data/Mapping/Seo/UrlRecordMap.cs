using Soft.Core.Domain.Seo;

namespace Soft.Data.Mapping.Seo
{
    public partial class UrlRecordMap : SoftEntityTypeConfiguration<UrlRecord>
    {
        public UrlRecordMap()
        {
            ToTable("UrlRecord");
            HasKey(lp => lp.Id);

            Property(lp => lp.EntityName).IsRequired().HasMaxLength(400);
            Property(lp => lp.Slug).IsRequired().HasMaxLength(400);
        }
    }
}