using Soft.Core.Domain.Customers;

namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa un nivel de precio
    /// </summary>
    public partial class TierPrice : BaseEntity
    {
        /// <summary>
        /// Identificador del producto
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Identificador de la tienda (0 - todas)
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Identificador del rol del cliente
        /// </summary>
        public int? CustomerRoleId { get; set; }

        /// <summary>
        /// CantidaD
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Precio
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Precio
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Rol del cliente
        /// </summary>
        public virtual CustomerRole CustomerRole { get; set; }
    }
}