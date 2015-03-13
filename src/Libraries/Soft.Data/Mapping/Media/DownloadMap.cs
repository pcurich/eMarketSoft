using Soft.Core.Domain.Media;

namespace Soft.Data.Mapping.Media
{
    public partial class DownloadMap : SoftEntityTypeConfiguration<Download>
    {
        public DownloadMap()
        {
            ToTable("Download");
            HasKey(p => p.Id);
            Property(p => p.DownloadBinary).IsMaxLength();
        }
    }
}