using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Soft.Core;
using Soft.Core.Domain.Customers;
using Soft.Core.Infrastructure;
using Soft.Services.Common;
using Soft.Services.Localization;
using Soft.Services.Logging;
using Soft.Services.Stores;
using Soft.Web.Framework.Filters;
using Soft.Web.Framework.Localization;
using Soft.Web.Framework.UI;

namespace Soft.Web.Framework.Controllers
{
    /// <summary>
    ///     Controlador base
    /// </summary>
    [StoreIpAddress]
    [CustomerLastActivity]
    [StoreLastVisitedPage]
    public abstract class BaseController : Controller
    {
        /// <summary>
        ///     Render una vista parcial a una cadena
        /// </summary>
        /// <returns></returns>
        public virtual string RenderPartialViewToString()
        {
            return RenderPartialViewToString(null, null);
        }

        /// <summary>
        ///     Render una vista parcial a una cadena
        /// </summary>
        /// <param name="viewName">Nombre de la vista.</param>
        /// <returns></returns>
        public virtual string RenderPartialViewToString(string viewName)
        {
            return RenderPartialViewToString(viewName, null);
        }

        /// <summary>
        ///     Render una vista parcial a una cadena
        /// </summary>
        /// <param name="model">Modelo</param>
        /// <returns></returns>
        public virtual string RenderPartialViewToString(object model)
        {
            return RenderPartialViewToString(null, model);
        }

        /// <summary>
        ///     Render una vista parcial a una cadena
        /// </summary>
        /// <param name="viewName">Nombre de la vista</param>
        /// <param name="model">El modelo.</param>
        /// <returns></returns>
        public virtual string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewEngineResult = System.Web.Mvc.ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewEngineResult.View, ViewData, TempData, sw);
                viewEngineResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        ///     Optiene la tienda activa(para multi-store eb ek nodo de configuracion)
        /// </summary>
        /// <param name="storeService">Store service</param>
        /// <param name="workContext">Work context</param>
        /// <returns>Identificador de la tienda; 0 Si se encuentra en modo compartido</returns>
        public virtual int GetActiveStoreScopeConfiguration(IStoreService storeService, IWorkContext workContext)
        {
            //Se asegura que se tenga 2 o mas tiendas
            if (storeService.GetAllStores().Count < 2)
                return 0;

            var storeId =
                workContext.CurrentCustomer.GetAttribute<int>(
                    SystemCustomerAttributeNames.AdminAreaStoreScopeConfiguration);
            var store = storeService.GetStoreById(storeId);
            return store != null ? store.Id : 0;
        }

        /// <summary>
        ///     Log de excepcion
        /// </summary>
        /// <param name="exc">Excepcion.</param>
        protected void LogException(Exception exc)
        {
            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            var logger = EngineContext.Current.Resolve<ILogger>();

            var customer = workContext.CurrentCustomer;
            logger.Error(exc.Message, exc, customer);
        }

        /// <summary>
        ///     Muestra notificacion de success
        /// </summary>
        /// <param name="message">Mensaje</param>
        /// <param name="persistForTheNextRequest">Si es <c>true</c> [Persiste para el proximo reuest].</param>
        protected virtual void SuccessNotification(string message, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Success, message, persistForTheNextRequest);
        }

        /// <summary>
        ///     Muestra notificacion de error
        /// </summary>
        /// <param name="message">Mensaje</param>
        /// <param name="persistForTheNextRequest">Si es <c>true</c> [Persiste para el proximo reuest].</param>
        protected virtual void ErrorNotification(string message, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Error, message, persistForTheNextRequest);
        }

        /// <summary>
        ///     Muestra notificaciones de errores
        /// </summary>
        /// <param name="exception">La exception.</param>
        /// <param name="persistForTheNextRequest">Si es <c>true</c> [Persiste para el proximo reuest].</param>
        /// <param name="logException">Si es <c>true</c> [log exception].</param>
        protected virtual void ErrorNotification(Exception exception, bool persistForTheNextRequest = true,
            bool logException = true)
        {
            if (logException)
                LogException(exception);
            AddNotification(NotifyType.Error, exception.Message, persistForTheNextRequest);
        }

        /// <summary>
        ///     Agrega las notificaciones
        /// </summary>
        /// <param name="type">Tipo de notificcion <see cref="NotifyType" /></param>
        /// <param name="message">El mensaje</param>
        /// <param name="persistForTheNextRequest">Si es <c>true</c> [Persiste para el proximo reuest].</param>
        protected virtual void AddNotification(NotifyType type, string message, bool persistForTheNextRequest)
        {
            var dataKey = string.Format("soft.notifications.{0}", type);
            if (persistForTheNextRequest)
            {
                if (TempData[dataKey] == null)
                    TempData[dataKey] = new List<string>();
                ((List<string>) TempData[dataKey]).Add(message);
            }
            else
            {
                if (ViewData[dataKey] == null)
                    ViewData[dataKey] = new List<string>();
                ((List<string>) ViewData[dataKey]).Add(message);
            }
        }

        /// <summary>
        /// Añade locales para entidades localizables
        /// </summary>
        /// <typeparam name="TLocalizedModelLocal">El tipo de un modelo localizable local.</typeparam>
        /// <param name="languageService">Servicio de lenguaje.</param>
        /// <param name="locales">Los locales.</param>
        protected virtual void AddLocales<TLocalizedModelLocal>(ILanguageService languageService,
            IList<TLocalizedModelLocal> locales)
            where TLocalizedModelLocal : ILocalizedModelLocal
        {
            AddLocales(languageService, locales, null);
        }

        /// <summary>
        /// Añade locales para entidades localizables
        /// </summary>
        /// <typeparam name="TLocalizedModelLocal">El tipo de un modelo localizable local.</typeparam>
        /// <param name="languageService">Servicio de lenguaje.</param>
        /// <param name="locales">Los locales.</param>
        /// <param name="configure">Accion de configuracion</param>
        protected virtual void AddLocales<TLocalizedModelLocal>(ILanguageService languageService, 
            IList<TLocalizedModelLocal> locales, 
            Action<TLocalizedModelLocal, int> configure) 
            where TLocalizedModelLocal : ILocalizedModelLocal
        {
            foreach (var language in languageService.GetAllLanguages(true))
            {
                var locale = Activator.CreateInstance<TLocalizedModelLocal>();
                locale.LanguageId = language.Id;
                if (configure != null)
                {
                    configure.Invoke(locale, locale.LanguageId);
                }
                locales.Add(locale);
            }
        }
    }
}