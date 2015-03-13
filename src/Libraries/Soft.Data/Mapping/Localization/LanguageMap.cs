using Soft.Core.Domain.Localization;

namespace Soft.Data.Mapping.Localization
{
    public partial class LanguageMap : SoftEntityTypeConfiguration<Language>
    {
        public LanguageMap()
        {
            ToTable("Language");
            HasKey(l => l.Id);
            Property(l => l.Name).IsRequired().HasMaxLength(100);
            Property(l => l.LanguageCulture).IsRequired().HasMaxLength(20);
            Property(l => l.UniqueSeoCode).HasMaxLength(2);
            Property(l => l.FlagImageFileName).HasMaxLength(50);
        
        }
    }
}