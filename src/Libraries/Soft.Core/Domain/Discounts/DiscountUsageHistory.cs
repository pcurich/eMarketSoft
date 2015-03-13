using System;
using Soft.Core.Domain.Orders;

namespace Soft.Core.Domain.Discounts
{
    /// <summary>
    /// Representa los descuentos usados en la historia
    /// </summary>
    public partial class DiscountUsageHistory : BaseEntity
    {
        /// <summary>
        /// Identificador del descuento
        /// </summary>
        public int DiscountId { get; set; }

        /// <summary>
        /// Identificador del descuento
        /// </summary>
        public int OrderId { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Descuentos
        /// </summary>
        public virtual Discount Discount { get; set; }

        /// <summary>
        /// Orden        
        /// </summary>
        public virtual Order Order { get; set; }
    }
}