using Soft.Core.Domain.Messages;

namespace Soft.Data.Mapping.Messages
{
    public partial class NewsLetterSubscriptionMap : SoftEntityTypeConfiguration<NewsLetterSubscription>
    {
        public NewsLetterSubscriptionMap()
        {
            ToTable("NewsLetterSubscription");
            HasKey(nls => nls.Id);

            Property(nls => nls.Email).IsRequired().HasMaxLength(255);
        }
    }
}