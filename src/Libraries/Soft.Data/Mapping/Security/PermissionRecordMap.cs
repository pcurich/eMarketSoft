using Soft.Core.Domain.Security;

namespace Soft.Data.Mapping.Security
{
    public partial class PermissionRecordMap : SoftEntityTypeConfiguration<PermissionRecord>
    {
        public PermissionRecordMap()
        {
            ToTable("PermissionRecord");
            HasKey(pr => pr.Id);
            Property(pr => pr.Name).IsRequired();
            Property(pr => pr.SystemName).IsRequired().HasMaxLength(255);
            Property(pr => pr.Category).IsRequired().HasMaxLength(255);

            HasMany(pr => pr.CustomerRoles)
                .WithMany(cr => cr.PermissionRecords)
                .Map(m => m.ToTable("PermissionRecord_Role_Mapping"));
        }
    }
}