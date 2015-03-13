using System;
using System.Web.Mvc;
using Soft.Core;
using Soft.Core.Data;
using Soft.Core.Infrastructure;
using Soft.Services.Customers;

namespace Soft.Web.Framework.Filters
{
    public class CustomerLastActivityAttribute : ActionFilterAttribute
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

            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            var customer = workContext.CurrentCustomer;

            //Actualiza la fecha de la ultima actividad minuto a minuto
            if (customer.LastActivityDateUtc.AddMinutes(1.0) >= DateTime.UtcNow)
                return;

            var customerService = EngineContext.Current.Resolve<ICustomerService>();
            customer.LastActivityDateUtc = DateTime.UtcNow;
            customerService.UpdateCustomer(customer);
        }
    }
}