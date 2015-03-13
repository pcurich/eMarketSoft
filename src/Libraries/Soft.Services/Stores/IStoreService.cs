using System.Collections.Generic;
using Soft.Core.Domain.Stores;

namespace Soft.Services.Stores
{
    /// <summary>
    /// IOnterfaz de los servicios de la tienda
    /// </summary>
    public partial interface IStoreService
    {
        /// <summary>
        /// Borra una tienda
        /// </summary>
        /// <param name="store">Store</param>
        void DeleteStore(Store store);

        /// <summary>
        /// Devuelve todas las tiendas
        /// </summary>
        /// <returns>Coleccion de tiendas</returns>
        IList<Store> GetAllStores();

        /// <summary>
        /// Retorna una tienda
        /// </summary>
        /// <param name="storeId">Identificador de una tienda</param>
        /// <returns>Tienda</returns>
        Store GetStoreById(int storeId);

        /// <summary>
        /// Insertar una tienda
        /// </summary>
        /// <param name="store">Tienda</param>
        void InsertStore(Store store);

        /// <summary>
        /// Actualiza una tienda
        /// </summary>
        /// <param name="store">Store</param>
        void UpdateStore(Store store);
    }
}