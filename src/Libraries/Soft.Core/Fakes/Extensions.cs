using System;
using System.Web;

namespace Soft.Core.Fakes
{
    public static class Extensions
    {
        /// <summary>
        /// Indica si este contexto es una imitacion
        /// </summary>
        /// <param name="httpContext">Http Context</param>
        /// <returns></returns>
        public static bool IsFakeContext(this HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            return httpContext is FakeHttpContext;
        }
    }
}