using Soft.Core.Caching;
using Soft.Core.Domain.Configuration;
using Soft.Core.Events;
using Soft.Core.Infrastructure;
using Soft.Services.Events;

namespace Soft.Admin.Infrastructure.Cache
{
    /// <summary>
    /// Model cache event consumer (used for caching of presentation layer models)
    /// </summary>
    public partial class ModelCacheEventConsumer :IConsumer<EntityUpdated<Setting>> //setting
    {
        /// <summary>
        /// Key for nopCommerce.com news cache
        /// </summary>
        public const string OfficialNewsModelKey = "Soft.pres.admin.official.news";
        public const string OfficialNewsPatternKey = "Soft.pres.admin.official.news";

        private readonly ICacheManager _cacheManager;

        public ModelCacheEventConsumer()
        {
            //TODO inject static cache manager using constructor
            this._cacheManager = EngineContext.Current.ContainerManager.Resolve<ICacheManager>("soft_cache_static");
        }

        public void HandleEvent(EntityUpdated<Setting> eventMessage)
        {
            //clear models which depend on settings
            _cacheManager.RemoveByPattern(OfficialNewsPatternKey); //depends on CommonSettings.HideAdvertisementsOnAdminArea

        }
    }
}