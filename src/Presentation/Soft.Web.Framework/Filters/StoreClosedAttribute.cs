﻿using System;
using System.Web;
using System.Web.Mvc;
using Soft.Core;
using Soft.Core.Data;
using Soft.Core.Domain;
using Soft.Core.Domain.Customers;
using Soft.Core.Infrastructure;

namespace Soft.Web.Framework.Filters
{
    public class StoreClosedAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null || filterContext.HttpContext == null)
                return;

            HttpRequestBase request = filterContext.HttpContext.Request;
            if (request == null)
                return;

            string actionName = filterContext.ActionDescriptor.ActionName;
            if (String.IsNullOrEmpty(actionName))
                return;

            string controllerName = filterContext.Controller.ToString();
            if (String.IsNullOrEmpty(controllerName))
                return;

            //don't apply filter to child methods
            if (filterContext.IsChildAction)
                return;

            if (!DataSettingsHelper.DatabaseIsInstalled())
                return;

            var storeInformationSettings = EngineContext.Current.Resolve<StoreInformationSettings>();
            if (!storeInformationSettings.StoreClosed)
                return;

            if (//ensure it's not the Login page
                !(controllerName.Equals("Soft.Web.Controllers.CustomerController", StringComparison.InvariantCultureIgnoreCase) && actionName.Equals("Login", StringComparison.InvariantCultureIgnoreCase)) &&
                //ensure it's not the Logout page
                !(controllerName.Equals("Soft.Web.Controllers.CustomerController", StringComparison.InvariantCultureIgnoreCase) && actionName.Equals("Logout", StringComparison.InvariantCultureIgnoreCase)) &&
                //ensure it's not the method (AJAX) for accepting EU Cookie law
                !(controllerName.Equals("Soft.Web.Controllers.CommonController", StringComparison.InvariantCultureIgnoreCase) && actionName.Equals("EuCookieLawAccept", StringComparison.InvariantCultureIgnoreCase)) &&
                //ensure it's not the store closed page
                !(controllerName.Equals("Soft.Web.Controllers.CommonController", StringComparison.InvariantCultureIgnoreCase) && actionName.Equals("StoreClosed", StringComparison.InvariantCultureIgnoreCase)) &&
                //ensure it's not the change language page (request)
                !(controllerName.Equals("Soft.Web.Controllers.CommonController", StringComparison.InvariantCultureIgnoreCase) && actionName.Equals("SetLanguage", StringComparison.InvariantCultureIgnoreCase)))
            {
                if (storeInformationSettings.StoreClosedAllowForAdmins && EngineContext.Current.Resolve<IWorkContext>().CurrentCustomer.IsAdmin())
                {
                    //do nothing - allow admin access
                }
                else
                {
                    var storeClosedUrl = new UrlHelper(filterContext.RequestContext).RouteUrl("StoreClosed");
                    filterContext.Result = new RedirectResult(storeClosedUrl);
                }
            }
        }
    }
}