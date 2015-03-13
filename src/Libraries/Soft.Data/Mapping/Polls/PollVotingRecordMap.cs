using Soft.Core.Domain.Polls;

namespace Soft.Data.Mapping.Polls
{
    public partial class PollVotingRecordMap : SoftEntityTypeConfiguration<PollVotingRecord>
    {
        public PollVotingRecordMap()
        {
            ToTable("PollVotingRecord");
            HasKey(pr => pr.Id);

            HasRequired(pvr => pvr.PollAnswer)
                .WithMany(pa => pa.PollVotingRecords)
                .HasForeignKey(pvr => pvr.PollAnswerId);

            HasRequired(cc => cc.Customer)
                .WithMany()
                .HasForeignKey(cc => cc.CustomerId);
        }
    }
}