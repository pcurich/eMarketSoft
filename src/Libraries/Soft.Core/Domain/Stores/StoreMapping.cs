namespace Soft.Core.Domain.Stores
{
    /// <summary>
    /// Representa un registro de mapeo de tiendas
    /// </summary>
    public partial class StoreMapping : BaseEntity
    {
        /// <summary>
        /// Identificadore del Entity
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// Nombre del entity
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Identificador de la tienda
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Tienda
        /// </summary>
        public virtual Store Store { get; set; }
    }
}