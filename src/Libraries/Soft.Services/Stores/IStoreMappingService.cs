using System.Collections.Generic;
using Soft.Core;
using Soft.Core.Domain.Stores;

namespace Soft.Services.Stores
{
    /// <summary>
    /// Interfaz de Servicio de mapeo de tiendas
    /// </summary>
    public partial interface IStoreMappingService
    {
        /// <summary>
        /// Borra un mapeo de tienda
        /// </summary>
        /// <param name="storeMapping">Store mapping record</param>
        void DeleteStoreMapping(StoreMapping storeMapping);

        /// <summary>
        /// Obtiene un registro de un mapeo de tienda
        /// </summary>
        /// <param name="storeMappingId">Store mapping record identifier</param>
        /// <returns>Store mapping record</returns>
        StoreMapping GetStoreMappingById(int storeMappingId);

        /// <summary>
        /// Obtiene todos los registros de un mapeo de tienda
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>Store mapping records</returns>
        IList<StoreMapping> GetStoreMappings<T>(T entity) where T : BaseEntity, IStoreMappingSupported;

        /// <summary>
        /// Inserta un registro de mapeo de tienda
        /// </summary>
        /// <param name="storeMapping">Store mapping</param>
        void InsertStoreMapping(StoreMapping storeMapping);

        /// <summary>
        /// Inserta un registro de mapeo de tienda
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="storeId">Store id</param>
        /// <param name="entity">Entity</param>
        void InsertStoreMapping<T>(T entity, int storeId) where T : BaseEntity, IStoreMappingSupported;

        /// <summary>
        /// Actualiza un registro de mapeo de tienda
        /// </summary>
        /// <param name="storeMapping">Store mapping</param>
        void UpdateStoreMapping(StoreMapping storeMapping);

        /// <summary>
        /// Encuentra los identificadores de tiendas con accesos (mapeados para los entities)
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Wntity</param>
        /// <returns>Store identifiers</returns>
        int[] GetStoresIdsWithAccess<T>(T entity) where T : BaseEntity, IStoreMappingSupported;

        /// <summary>
        /// Autoriza si el entity puede acceder en la tienda actual (mapeado a esta tienda)
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Wntity</param>
        /// <returns>true - autorizado; otros, false</returns>
        bool Authorize<T>(T entity) where T : BaseEntity, IStoreMappingSupported;

        /// <summary>
        /// Autoriza si el entity puede acceder en la tienda (mapeado a esta tienda)
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="storeId">Store identifier</param>
        /// <returns>true - autorizado; otros, false</returns>
        bool Authorize<T>(T entity, int storeId) where T : BaseEntity, IStoreMappingSupported;
    }
}