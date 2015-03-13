using System;
using System.Net;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;
using Soft.Admin.Infrastructure.Cache;
using Soft.Admin.Models.Home;
using Soft.Core;
using Soft.Services.Configuration;
using Soft.Core.Caching;
using Soft.Core.Domain.Common.Settings;

namespace Soft.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        #region Campos

        private readonly IStoreContext _storeContext;
        private readonly CommonSettings _commonSettings;
        private readonly ISettingService _settingService;
        private readonly IWorkContext _workContext;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public HomeController(IStoreContext storeContext,
            CommonSettings commonSettings,
            ISettingService settingService,
            IWorkContext workContext,
            ICacheManager cacheManager)
        {
            _storeContext = storeContext;
            _commonSettings = commonSettings;
            _settingService = settingService;
            _workContext = workContext;
            _cacheManager = cacheManager;
        }

        #endregion

        

        #region Metodos

        public ActionResult Index()
        {
            var model = new DashboardModel();
            model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult SoftCommerceNews()
        {
            try
            {
                //string feedUrl = string.Format("http://www.nopCommerce.com/NewsRSS.aspx?Version={0}&Localhost={1}&HideAdvertisements={2}&StoreURL={3}",
                var feedUrl = string.Format("http://www.nopCommerce.com/NewsRSS.aspx?Version={0}",
                    SoftVersion.CurrentVersion,
                    Request.Url.IsLoopback,
                    _commonSettings.HideAdvertisementsOnAdminArea,
                    _storeContext.CurrentStore.Url)
                    .ToLowerInvariant();

                var rssData = _cacheManager.Get(ModelCacheEventConsumer.OfficialNewsModelKey, () =>
                {
                    //specify timeout (5 secs)
                    var request = WebRequest.Create(feedUrl);
                    request.Timeout = 5000;
                    using (var response = request.GetResponse())
                    using (var reader = XmlReader.Create(response.GetResponseStream()))
                    {
                        return SyndicationFeed.Load(reader);
                    }
                });

                return PartialView(rssData);
            }
            catch (Exception)
            {
                return Content("");
            }
        }

        [HttpPost]
        public ActionResult SoftCommerceNewsHideAdv()
        {
            _commonSettings.HideAdvertisementsOnAdminArea = !_commonSettings.HideAdvertisementsOnAdminArea;
            _settingService.SaveSetting(_commonSettings);
            return Content("Setting changed");
        }

        #endregion
    }
}