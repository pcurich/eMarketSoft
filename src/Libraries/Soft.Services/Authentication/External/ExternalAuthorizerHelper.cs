//Contributor:  Nicholas Mayne

using System.Collections.Generic;
using System.Web;
using Soft.Core.Infrastructure;

namespace Soft.Services.Authentication.External
{
    /// <summary>
    /// Ayuda de autorizacion externa
    /// </summary>
    public static partial class ExternalAuthorizerHelper
    {
        private static HttpSessionStateBase GetSession()
        {
            var session = EngineContext.Current.Resolve<HttpSessionStateBase>();
            return session;
        }

        public static void StoreParametersForRoundTrip(OpenAuthenticationParameters parameters)
        {
            var session = GetSession();
            session["Soft.externalauth.parameters"] = parameters;
        }
        public static OpenAuthenticationParameters RetrieveParametersFromRoundTrip(bool removeOnRetrieval)
        {
            var session = GetSession();
            var parameters = session["Soft.externalauth.parameters"];
            if (parameters != null && removeOnRetrieval)
                RemoveParameters();

            return parameters as OpenAuthenticationParameters;
        }

        public static void RemoveParameters()
        {
            var session = GetSession();
            session.Remove("Soft.externalauth.parameters");
        }

        public static void AddErrorsToDisplay(string error)
        {
            var session = GetSession();
            var errors = session["Soft.externalauth.errors"] as IList<string>;
            if (errors == null)
            {
                errors = new List<string>();
                session.Add("Soft.externalauth.errors", errors);
            }
            errors.Add(error);
        }

        public static IList<string> RetrieveErrorsToDisplay(bool removeOnRetrieval)
        {
            var session = GetSession();
            var errors = session["Soft.externalauth.errors"] as IList<string>;
            if (errors != null && removeOnRetrieval)
                session.Remove("Soft.externalauth.errors");
            return errors;
        }
    }
}