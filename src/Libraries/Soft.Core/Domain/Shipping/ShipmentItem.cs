namespace Soft.Core.Domain.Shipping
{
    /// <summary>
    /// Representa un envio
    /// </summary>
    public partial class ShipmentItem : BaseEntity
    {
        /// <summary>
        /// Identificador del envio
        /// </summary>
        public int ShipmentId { get; set; }

        /// <summary>
        /// Identificador del orden de envio
        /// </summary>
        public int OrderItemId { get; set; }

        /// <summary>
        /// Cantidad
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Identificador de almacen
        /// </summary>
        public int WarehouseId { get; set; }

        /// <summary>
        /// Envio
        /// </summary>
        public virtual Shipment Shipment { get; set; }
    }
}