using Soft.Core.Domain.Localization;

namespace Soft.Data.Mapping.Localization
{
    public partial class LocalizedPropertyMap : SoftEntityTypeConfiguration<LocalizedProperty>
    {
        public LocalizedPropertyMap()
        {
            ToTable("LocalizedProperty");
            HasKey(lp => lp.Id);

            Property(lp => lp.LocaleKeyGroup).IsRequired().HasMaxLength(400);
            Property(lp => lp.LocaleKey).IsRequired().HasMaxLength(400);
            Property(lp => lp.LocaleValue).IsRequired();
            
            HasRequired(lp => lp.Language)
                .WithMany()
                .HasForeignKey(lp => lp.LanguageId);
        }
    }
}