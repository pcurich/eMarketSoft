using Soft.Core.Domain.Configuration;

namespace Soft.Data.Mapping.Configuration
{
    public partial class SettingMap : SoftEntityTypeConfiguration<Setting>
    {
        public SettingMap()
        {
            ToTable("Setting");
            HasKey(s => s.Id);
            Property(s => s.Name).IsRequired().HasMaxLength(200);
            Property(s => s.Value).IsRequired().HasMaxLength(2000);
        }
    }
}