using System;
using Soft.Core.Domain.Customers;

namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa una orden de pedido desde una subscripcion
    /// </summary>
    public partial class BackInStockSubscription : BaseEntity
    {
        /// <summary>
        /// Identificador de la tienda
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Identificador del producto
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Identificador del ciente
        /// </summary>
        public int CustomerId { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// El producto
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// El cliente
        /// </summary>
        public virtual Customer Customer { get; set; }
    }
}