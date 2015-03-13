using Soft.Core.Domain.Security;

namespace Soft.Data.Mapping.Security
{
    public partial class AclRecordMap : SoftEntityTypeConfiguration<AclRecord>
    {
        public AclRecordMap()
        {
            ToTable("AclRecord");
            HasKey(ar => ar.Id);

            Property(ar => ar.EntityName).IsRequired().HasMaxLength(400);

            HasRequired(ar => ar.CustomerRole)
                .WithMany()
                .HasForeignKey(ar => ar.CustomerRoleId)
                .WillCascadeOnDelete(true);
        }
    }
}