using System;
using System.Web.Mvc;
using Soft.Core;
using Soft.Core.Data;
using Soft.Core.Infrastructure;
using Soft.Services.Customers;

namespace Soft.Web.Framework.Filters
{
    /// <summary>
    /// Se aplica a los request. Verifica la session de un usuario y la ip de la cual se conecta
    /// </summary>
    public class StoreIpAddressAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!DataSettingsHelper.DatabaseIsInstalled())
                return;

            if (filterContext == null || filterContext.HttpContext == null || filterContext.HttpContext.Request == null)
                return;

            //No se aplica el filtro a los metodos hijos
            if (filterContext.IsChildAction)
                return;

            //Solo se aplica a los request
            if (!String.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                return;

            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            var currentIpAddress = webHelper.GetCurrentIpAddress();

            if (String.IsNullOrEmpty(currentIpAddress)) 
                return;

            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            var customer = workContext.CurrentCustomer;

            //Actualiza la direccion de IP del cliente que accedio
            if (currentIpAddress.Equals(customer.LastIpAddress, StringComparison.InvariantCultureIgnoreCase)) 
                return;

            var customerService = EngineContext.Current.Resolve<ICustomerService>();
            customer.LastIpAddress = currentIpAddress;
            customerService.UpdateCustomer(customer);
        }
    }
}