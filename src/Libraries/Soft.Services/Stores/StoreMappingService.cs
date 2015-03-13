using System;
using System.Collections.Generic;
using System.Linq;
using Soft.Core;
using Soft.Core.Caching;
using Soft.Core.Data;
using Soft.Core.Domain.Catalog;
using Soft.Core.Domain.Stores;

namespace Soft.Services.Stores
{
    /// <summary>
    /// Store mapping service
    /// </summary>
    public partial class StoreMappingService : IStoreMappingService
    {
        #region Constantes

        /// <summary>
        /// Llave para el cache
        /// </summary>
        /// <remarks>
        /// {0} : entity ID
        /// {1} : entity nombre
        /// </remarks>
        private const string StoremappingByEntityidNameKey = "Soft.storemapping.entityid-name-{0}-{1}";

        /// <summary>
        /// Llave para borrar el patron 
        /// </summary>
        private const string StoremappingPatternKey = "Soft.storemapping.";

        #endregion

        #region Campos

        private readonly IRepository<StoreMapping> _storeMappingRepository;
        private readonly IStoreContext _storeContext;
        private readonly ICacheManager _cacheManager;
        private readonly CatalogSettings _catalogSettings;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="storeContext">Store context</param>
        /// <param name="storeMappingRepository">Store mapping repository</param>
        /// <param name="catalogSettings">Catalog settings</param>
        public StoreMappingService(ICacheManager cacheManager,
            IStoreContext storeContext,
            IRepository<StoreMapping> storeMappingRepository,
            CatalogSettings catalogSettings)
        {
            _cacheManager = cacheManager;
            _storeContext = storeContext;
            _storeMappingRepository = storeMappingRepository;
            _catalogSettings = catalogSettings;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Borra un mapeo de tienda
        /// </summary>
        /// <param name="storeMapping">Store mapping record</param>
        public virtual void DeleteStoreMapping(StoreMapping storeMapping)
        {
            if (storeMapping == null)
                throw new ArgumentNullException("storeMapping");

            _storeMappingRepository.Delete(storeMapping);

            //cache
            _cacheManager.RemoveByPattern(StoremappingPatternKey);
        }

        /// <summary>
        /// Obtiene un registro de un mapeo de tienda
        /// </summary>
        /// <param name="storeMappingId">Store mapping record identifier</param>
        /// <returns>Store mapping record</returns>
        public virtual StoreMapping GetStoreMappingById(int storeMappingId)
        {
            if (storeMappingId == 0)
                return null;

            return _storeMappingRepository.GetById(storeMappingId);
        }

        /// <summary>
        /// Obtiene todos los registros de un mapeo de tienda
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>Store mapping records</returns>
        public virtual IList<StoreMapping> GetStoreMappings<T>(T entity) where T : BaseEntity, IStoreMappingSupported
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var entityId = entity.Id;
            var entityName = typeof (T).Name;

            var query = from sm in _storeMappingRepository.Table
                where sm.EntityId == entityId &&
                      sm.EntityName == entityName
                select sm;
            var storeMappings = query.ToList();
            return storeMappings;
        }


        /// <summary>
        /// Inserta un registro de mapeo de tienda
        /// </summary>
        /// <param name="storeMapping">Store mapping</param>
        public virtual void InsertStoreMapping(StoreMapping storeMapping)
        {
            if (storeMapping == null)
                throw new ArgumentNullException("storeMapping");

            _storeMappingRepository.Insert(storeMapping);

            //cache
            _cacheManager.RemoveByPattern(StoremappingPatternKey);
        }

        /// <summary>
        /// Inserta un registro de mapeo de tienda
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="storeId">Store id</param>
        /// <param name="entity">Entity</param>
        public virtual void InsertStoreMapping<T>(T entity, int storeId) where T : BaseEntity, IStoreMappingSupported
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (storeId == 0)
                throw new ArgumentOutOfRangeException("storeId");

            var entityId = entity.Id;
            var entityName = typeof (T).Name;

            var storeMapping = new StoreMapping
            {
                EntityId = entityId,
                EntityName = entityName,
                StoreId = storeId
            };

            InsertStoreMapping(storeMapping);
        }

        /// <summary>
        /// Actualiza un registro de mapeo de tienda
        /// </summary>
        /// <param name="storeMapping">Store mapping</param>
        public virtual void UpdateStoreMapping(StoreMapping storeMapping)
        {
            if (storeMapping == null)
                throw new ArgumentNullException("storeMapping");

            _storeMappingRepository.Update(storeMapping);

            //cache
            _cacheManager.RemoveByPattern(StoremappingPatternKey);
        }

        /// <summary>
        /// Encuentra los identificadores de tiendas con accesos (mapeados para los entities)
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Wntity</param>
        /// <returns>Store identifiers</returns>
        public virtual int[] GetStoresIdsWithAccess<T>(T entity) where T : BaseEntity, IStoreMappingSupported
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var entityId = entity.Id;
            var entityName = typeof (T).Name;

            var key = string.Format(StoremappingByEntityidNameKey, entityId, entityName);
            return _cacheManager.Get(key, () =>
            {
                var query = from sm in _storeMappingRepository.Table
                    where sm.EntityId == entityId &&
                          sm.EntityName == entityName
                    select sm.StoreId;
                return query.ToArray();
            });
        }

        /// <summary>
        /// Autoriza si el entity puede acceder en la tienda actual (mapeado a esta tienda)
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>true - Autorzado; otros, false</returns>
        public virtual bool Authorize<T>(T entity) where T : BaseEntity, IStoreMappingSupported
        {
            return Authorize(entity, _storeContext.CurrentStore.Id);
        }

        /// <summary>
        /// Autoriza si el entity puede acceder en la tienda (mapeado a esta tienda)
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="storeId">Store identifier</param>
        /// <returns>true - autorizado; otros, false</returns>
        public virtual bool Authorize<T>(T entity, int storeId) where T : BaseEntity, IStoreMappingSupported
        {
            if (entity == null)
                return false;

            if (storeId == 0)
                //return true if no store specified/found
                return true;

            if (_catalogSettings.IgnoreStoreLimitations)
                return true;

            if (!entity.LimitedToStores)
                return true;

            foreach (var storeIdWithAccess in GetStoresIdsWithAccess(entity))
                if (storeId == storeIdWithAccess)
                    //yes, we have such permission
                    return true;

            //no permission found
            return false;
        }

        #endregion
    }
}