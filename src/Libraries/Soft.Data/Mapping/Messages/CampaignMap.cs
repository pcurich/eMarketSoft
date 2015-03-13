using Soft.Core.Domain.Messages;

namespace Soft.Data.Mapping.Messages
{
    public partial class CampaignMap : SoftEntityTypeConfiguration<Campaign>
    {
        public CampaignMap()
        {
            ToTable("Campaign");
            HasKey(ea => ea.Id);

            Property(ea => ea.Name).IsRequired();
            Property(ea => ea.Subject).IsRequired();
            Property(ea => ea.Body).IsRequired();
        }
    }
}