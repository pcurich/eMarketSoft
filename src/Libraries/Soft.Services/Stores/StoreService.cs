using System;
using System.Collections.Generic;
using System.Linq;
using Soft.Core.Caching;
using Soft.Core.Data;
using Soft.Core.Domain.Stores;
using Soft.Services.Events;

namespace Soft.Services.Stores
{
    /// <summary>
    /// Servicio de tienda
    /// </summary>
    public partial class StoreService : IStoreService
    {
        #region Constantes

        /// <summary>
        /// Llave para todos los caches de tienda
        /// </summary>
        private const string StoresAllKey = "Soft.stores.all";
        /// <summary>
        /// Llave para el cache
        /// </summary>
        /// <remarks>
        /// {0} : store ID
        /// </remarks>
        private const string StoresByIdKey = "Soft.stores.id-{0}";
        /// <summary>
        /// Patron de llave para borrar el cache
        /// </summary>
        private const string StoresPatternKey = "Soft.stores.";

        #endregion

        #region Campos

        private readonly IRepository<Store> _storeRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="storeRepository">Store repository</param>
        /// <param name="eventPublisher">Event published</param>
        public StoreService(ICacheManager cacheManager,
            IRepository<Store> storeRepository,
            IEventPublisher eventPublisher)
        {
            _cacheManager = cacheManager;
            _storeRepository = storeRepository;
            _eventPublisher = eventPublisher;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes a store
        /// </summary>
        /// <param name="store">Store</param>
        public virtual void DeleteStore(Store store)
        {
            if (store == null)
                throw new ArgumentNullException("store");

            var allStores = GetAllStores();
            if (allStores.Count == 1)
                throw new Exception("You cannot delete the only configured store");

            _storeRepository.Delete(store);

            _cacheManager.RemoveByPattern(StoresPatternKey);

            //event notification
            _eventPublisher.EntityDeleted(store);
        }

        /// <summary>
        /// Retorna todas las tiendas
        /// </summary>
        /// <returns>Store collection</returns>
        public virtual IList<Store> GetAllStores()
        {
            const string key = StoresAllKey;
            return _cacheManager.Get(key, () =>
            {
                var query = from s in _storeRepository.Table
                            orderby s.DisplayOrder, s.Id
                            select s;
                var stores = query.ToList();
                return stores;
            });
        }

        /// <summary>
        /// Retorna una tienda
        /// </summary>
        /// <param name="storeId">Store identifier</param>
        /// <returns>Store</returns>
        public virtual Store GetStoreById(int storeId)
        {
            if (storeId == 0)
                return null;

            var key = string.Format(StoresByIdKey, storeId);
            return _cacheManager.Get(key, () => _storeRepository.GetById(storeId));
        }

        /// <summary>
        /// Inserta una tienda
        /// </summary>
        /// <param name="store">Store</param>
        public virtual void InsertStore(Store store)
        {
            if (store == null)
                throw new ArgumentNullException("store");

            _storeRepository.Insert(store);

            _cacheManager.RemoveByPattern(StoresPatternKey);

            //event notification
            _eventPublisher.EntityInserted(store);
        }

        /// <summary>
        /// Actualiza la tienda
        /// </summary>
        /// <param name="store">Store</param>
        public virtual void UpdateStore(Store store)
        {
            if (store == null)
                throw new ArgumentNullException("store");

            _storeRepository.Update(store);

            _cacheManager.RemoveByPattern(StoresPatternKey);

            //event notification
            _eventPublisher.EntityUpdated(store);
        }

        #endregion
    }
}