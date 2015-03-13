using Soft.Core.Domain.Messages;

namespace Soft.Data.Mapping.Messages
{
    public partial class MessageTemplateMap : SoftEntityTypeConfiguration<MessageTemplate>
    {
        public MessageTemplateMap()
        {
            ToTable("MessageTemplate");
            HasKey(mt => mt.Id);

            Property(mt => mt.Name).IsRequired().HasMaxLength(200);
            Property(mt => mt.BccEmailAddresses).HasMaxLength(200);
            Property(mt => mt.Subject).HasMaxLength(1000);
            Property(mt => mt.EmailAccountId).IsRequired();
        }
    }
}