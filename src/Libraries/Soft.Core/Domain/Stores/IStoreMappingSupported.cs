namespace Soft.Core.Domain.Stores
{
    /// <summary>
    /// Representa un Entity que soportara mapeo de tienda
    /// </summary>
    public interface IStoreMappingSupported
    {
        /// <summary>
        /// Obtiene o establece un valor que indica si la entidad es limitado / restringido a ciertas tiendas
        /// </summary>
        bool LimitedToStores { get; set; }
    }
}