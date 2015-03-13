using Soft.Core.Domain.Common;

namespace Soft.Data.Mapping.Common
{
    public partial class SearchTermMap : SoftEntityTypeConfiguration<SearchTerm>
    {
        public SearchTermMap()
        {
            ToTable("SearchTerm");
            HasKey(st => st.Id);
        }
    }
}
