using Soft.Core.Domain.Polls;

namespace Soft.Data.Mapping.Polls
{
    public partial class PollAnswerMap : SoftEntityTypeConfiguration<PollAnswer>
    {
        public PollAnswerMap()
        {
            ToTable("PollAnswer");
            HasKey(pa => pa.Id);
            Property(pa => pa.Name).IsRequired();

            HasRequired(pa => pa.Poll)
                .WithMany(p => p.PollAnswers)
                .HasForeignKey(pa => pa.PollId).WillCascadeOnDelete(true);
        }
    }
}