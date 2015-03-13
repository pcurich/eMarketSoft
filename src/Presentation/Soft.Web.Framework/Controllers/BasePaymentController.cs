using System.Collections.Generic;
using System.Web.Mvc;
using Soft.Services.Payments;

namespace Soft.Web.Framework.Controllers
{
    /// <summary>
    /// Base controller para pagos en plugins
    /// </summary>
    public abstract class BasePaymentController : BasePluginController
    {
        public abstract IList<string> ValidatePaymentForm(FormCollection form);
        public abstract ProcessPaymentRequest GetPaymentInfo(FormCollection form);
    }
}