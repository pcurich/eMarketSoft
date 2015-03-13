namespace Soft.Core.Domain.Discounts
{
    /// <summary>
    /// Representa un requerimiento de un descuento 
    /// </summary>
    public partial class DiscountRequirement : BaseEntity
    {
        /// <summary>
        /// Identificador del descuento
        /// </summary>
        public int DiscountId { get; set; }

        /// <summary>
        /// Requerimientos del descuento reglas del nombre del systema
        /// </summary>
        public string DiscountRequirementRuleSystemName { get; set; }

        /// <summary>
        /// Descuent 
        /// </summary>
        public virtual Discount Discount { get; set; }
    }
}