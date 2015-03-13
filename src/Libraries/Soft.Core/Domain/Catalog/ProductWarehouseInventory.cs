using Soft.Core.Domain.Shipping;

namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa im ,amejador del inventario de productos para el Warehouse
    /// </summary>
    public partial class ProductWarehouseInventory : BaseEntity
    {
        /// <summary>
        /// Identificador del producto
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Identificador del warehouse
        /// </summary>
        public int WarehouseId { get; set; }

        /// <summary>
        /// Cantidad de stock
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// Cantidad reservada (ordenadas pero no enviadas aun)
        /// </summary>
        public int ReservedQuantity { get; set; }

        /// <summary>
        /// Productos
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Warehouse
        /// </summary>
        public virtual Warehouse Warehouse { get; set; }
    }
}