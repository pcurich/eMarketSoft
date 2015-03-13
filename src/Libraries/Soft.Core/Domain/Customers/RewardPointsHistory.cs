using System;
using Soft.Core.Domain.Orders;

namespace Soft.Core.Domain.Customers
{
    public partial class RewardPointsHistory : BaseEntity
    {
        public int CustomerId { get; set; }
        public int Points { get; set; }
        public int PointsBalance { get; set; }

        /// <summary>
        /// Cantidad usada de puntos
        /// </summary>
        public decimal UsedAmount { get; set; }

        public string Message { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Puntos usados en una orden 
        /// </summary>
        public virtual Order UsedWithOrder { get; set; }

        public virtual Customer Customer { get; set; }
    }
}