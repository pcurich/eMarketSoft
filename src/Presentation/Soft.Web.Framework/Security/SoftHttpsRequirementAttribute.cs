using System;
using System.Web.Mvc;
using Soft.Core;
using Soft.Core.Data;
using Soft.Core.Domain.Security;
using Soft.Core.Infrastructure;

namespace Soft.Web.Framework.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SoftHttpsRequirementAttribute : FilterAttribute, IAuthorizationFilter
    {
        public SslRequirement SslRequirement { get; set; }
 
        public SoftHttpsRequirementAttribute(SslRequirement sslRequirement)
        {
            SslRequirement = sslRequirement;
        }

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            //No se aplica el filtro a los metodos hijos
            if (filterContext.IsChildAction)
                return;

            //Solo se aplica a los request
            if (!String.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                return;

            if (!DataSettingsHelper.DatabaseIsInstalled())
                return;

            var securitySettings = EngineContext.Current.Resolve<SecuritySettings>();
            
            if (securitySettings.ForceSslForAllPages)
                //Todas las paguinas son forzadas a usar SSL sin importar su valor
                SslRequirement = SslRequirement.Yes;

            switch (SslRequirement)
            {
                case SslRequirement.Yes:
                {
                    var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                    var currentConnectionSecured = webHelper.IsCurrentConnectionSecured();
                    if (!currentConnectionSecured)
                    {
                        var storeContext = EngineContext.Current.Resolve<IStoreContext>();
                        if (storeContext.CurrentStore.SslEnabled)
                        {
                            //Redirecciona a la version HTTPS de la paguina
                            //string url = "https://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;
                            var url = webHelper.GetThisPageUrl(true, true);
                            
                            //301 (permanent) redirection
                            filterContext.Result = new RedirectResult(url, true);
                        }
                    }
                }
                    break;
                case SslRequirement.No:
                {
                    var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                    var currentConnectionSecured = webHelper.IsCurrentConnectionSecured();
                    if (currentConnectionSecured)
                    {
                        //Redirecciona a la version HTTP de la paguina
                        //string url = "http://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;
                        string url = webHelper.GetThisPageUrl(true, false);
                        //301 (permanent) redirection
                        filterContext.Result = new RedirectResult(url, true);
                    }
                }
                    break;
                case SslRequirement.NoMatter:
                {
                    //no hacer nada
                }
                    break;
                default:
                    throw new SoftException("No soporta parametro SslProtegido");
            }



        }
    }
}