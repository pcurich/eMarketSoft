using Soft.Core.Domain.Localization;

namespace Soft.Core.Domain.Directory
{
    public class StateProvince : BaseEntity, ILocalizedEntity
    {
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
    }
}