namespace Soft.Core.Domain.Directory
{
    /// <summary>
    /// Representa una dimencion medible
    /// </summary>
    public class MeasureDimension : BaseEntity
    {
        public string Name { get; set; }
        public string SystemKeyword { get; set; }
        public decimal Ratio { get; set; }
        public int DisplayOrder { get; set; }
    }
}