using Soft.Core.Domain.Common;

namespace Soft.Data.Mapping.Common
{
    public partial class GenericAttributeMap : SoftEntityTypeConfiguration<GenericAttribute>
    {
        public GenericAttributeMap()
        {
            ToTable("GenericAttribute");
            HasKey(ga => ga.Id);

            Property(ga => ga.KeyGroup).IsRequired().HasMaxLength(400);
            Property(ga => ga.Key).IsRequired().HasMaxLength(400);
            Property(ga => ga.Value).IsRequired();
        }
    }
}