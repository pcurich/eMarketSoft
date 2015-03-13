//Contributor:  Nicholas Mayne

using System.Collections.Generic;

namespace Soft.Services.Authentication.External
{
    /// <summary>
    /// Authorization result
    /// </summary>
    public partial class AuthorizationResult
    {
        public AuthorizationResult(OpenAuthenticationStatus status)
        {
            Errors = new List<string>();
            Status = status;
        }

        public void AddError(string error)
        {
            Errors.Add(error);
        }

        public bool Success
        {
            get { return Errors.Count == 0; }
        }

        public OpenAuthenticationStatus Status { get; private set; }

        public IList<string> Errors { get; set; }
    }
}