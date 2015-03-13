using Soft.Core.Domain.Localization;

namespace Soft.Data.Mapping.Localization
{
    public partial class LocaleStringResourceMap : SoftEntityTypeConfiguration<LocaleStringResource>
    {
        public LocaleStringResourceMap()
        {
            ToTable("LocaleStringResource");
            HasKey(lsr => lsr.Id);
            Property(lsr => lsr.ResourceName).IsRequired().HasMaxLength(200);
            Property(lsr => lsr.ResourceValue).IsRequired();


            HasRequired(lsr => lsr.Language)
                .WithMany(l => l.LocaleStringResources)
                .HasForeignKey(lsr => lsr.LanguageId);
        }
    }
}