using Soft.Core.Domain.Topics;

namespace Soft.Data.Mapping.Topics
{
    public class TopicMap : SoftEntityTypeConfiguration<Topic>
    {
        public TopicMap()
        {
            ToTable("Topic");
            HasKey(t => t.Id);
        }
    }
}
