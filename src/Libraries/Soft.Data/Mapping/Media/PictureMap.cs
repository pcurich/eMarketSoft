using Soft.Core.Domain.Media;

namespace Soft.Data.Mapping.Media
{
    public partial class PictureMap : SoftEntityTypeConfiguration<Picture>
    {
        public PictureMap()
        {
            ToTable("Picture");
            HasKey(p => p.Id);
            Property(p => p.PictureBinary).IsMaxLength();
            Property(p => p.MimeType).IsRequired().HasMaxLength(40);
            Property(p => p.SeoFilename).HasMaxLength(300);
        }
    }
}