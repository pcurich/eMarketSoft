using Soft.Core.Domain.Polls;

namespace Soft.Data.Mapping.Polls
{
    public partial class PollMap : SoftEntityTypeConfiguration<Poll>
    {
        public PollMap()
        {
            ToTable("Poll");
            HasKey(p => p.Id);
            Property(p => p.Name).IsRequired();
            
            HasRequired(p => p.Language)
                .WithMany()
                .HasForeignKey(p => p.LanguageId).WillCascadeOnDelete(true);
        }
    }
}