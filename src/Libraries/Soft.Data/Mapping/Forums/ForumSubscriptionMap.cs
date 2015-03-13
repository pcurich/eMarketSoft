using Soft.Core.Domain.Forums;

namespace Soft.Data.Mapping.Forums
{
    public partial class ForumSubscriptionMap : SoftEntityTypeConfiguration<ForumSubscription>
    {
        public ForumSubscriptionMap()
        {
            ToTable("Forums_Subscription");
            HasKey(fs => fs.Id);

            HasRequired(fs => fs.Customer)
                .WithMany()
                .HasForeignKey(fs => fs.CustomerId)
                .WillCascadeOnDelete(false);
        }
    }
}
