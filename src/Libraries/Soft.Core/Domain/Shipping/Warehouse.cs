namespace Soft.Core.Domain.Shipping
{
    /// <summary>
    /// Representa un envio
    /// </summary>
    public partial class Warehouse : BaseEntity
    {
        /// <summary>
        /// Nombre del warehouse
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Comentarios del administrador
        /// </summary>
        public string AdminComment { get; set; }

        /// <summary>
        /// Identificacion del almacen
        /// </summary>
        public int AddressId { get; set; }
    }
}