﻿using System.Web.Mvc;
using System.Web.Routing;
using Soft.Core;
using Soft.Core.Infrastructure;
using Soft.Web.Framework.Controllers;
using Soft.Web.Framework.Security;

namespace Soft.Admin.Controllers
{
    [SoftHttpsRequirement(SslRequirement.Yes)]
    [AdminValidateIpAddress]
    [AdminAuthorize]
    public abstract class BaseAdminController : BaseController
    {
        /// <summary>
        ///     Inicializa el controlador
        /// </summary>
        /// <param name="requestContext">Context de Request</param>
        protected override void Initialize(RequestContext requestContext)
        {
            //Establece el contexto de trabajo a modo de admin
            EngineContext.Current.Resolve<IWorkContext>().IsAdmin = true;
            base.Initialize(requestContext);
        }

        /// <summary>
        /// En una excepcion
        /// </summary>
        /// <param name="filterContext">Contexto de filtro</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
                LogException(filterContext.Exception);
            base.OnException(filterContext);
        }

        /// <summary>
        /// Vista de acceso denegado
        /// </summary>
        /// <returns>Redirecciona a la vista de acceso negado</returns>
        protected ActionResult AccessDeniedView()
        {
            //return new HttpUnauthorizedResult();
            return RedirectToAction("AccessDenied", "Security", new { pageUrl = this.Request.RawUrl });
        }

        /// <summary>
        /// Guarda el indice del seleccinado TAB
        /// </summary>
        /// <param name="index">Indice a guardar; null aitomaticamente es detectado</param>
        /// <param name="persistForTheNextRequest">Si el mensaje es persistente para el siguiente request</param>
        protected void SaveSelectedTabIndex(int? index = null, bool persistForTheNextRequest = true)
        {
            //Mantiene este metodo sincroniza con 
            //el metodo "GetSelectedTabIndex" de \Soft.Web.Framework\ViewEngines\Razor\WebViewPage.cs
            if (!index.HasValue)
            {
                int tmp;
                if (int.TryParse(Request.Form["selected-tab-index"], out tmp))
                {
                    index = tmp;
                }
            }
            if (index.HasValue)
            {
                const string dataKey = "soft.selected-tab-index";
                if (persistForTheNextRequest)
                {
                    TempData[dataKey] = index;
                }
                else
                {
                    ViewData[dataKey] = index;
                }
            }
        }
    }
}