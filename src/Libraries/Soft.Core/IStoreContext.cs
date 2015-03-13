using Soft.Core.Domain.Stores;

namespace Soft.Core
{
    /// <summary>
    /// Contexto de tienda
    /// </summary>
    public interface IStoreContext
    {
        /// <summary>
        /// Contexto actual de la tienda
        /// </summary>
        Store CurrentStore { get; }
    }
}