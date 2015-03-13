using Soft.Core.Domain.Stores;

namespace Soft.Data.Mapping.Stores
{
    public partial class StoreMappingMap : SoftEntityTypeConfiguration<StoreMapping>
    {
        public StoreMappingMap()
        {
            ToTable("StoreMapping");
            HasKey(sm => sm.Id);

            Property(sm => sm.EntityName).IsRequired().HasMaxLength(400);

            HasRequired(sm => sm.Store)
                .WithMany()
                .HasForeignKey(sm => sm.StoreId)
                .WillCascadeOnDelete(true);
        }
    }
}