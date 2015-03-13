using System.Collections.Generic;

namespace Soft.Services.Customers 
{
    public class CustomerRegistrationResult 
    {
        public IList<string> Errors { get; set; }

        public CustomerRegistrationResult() 
        {
            Errors = new List<string>();
        }

        public bool Success 
        {
            get { return Errors.Count == 0; }
        }

        public void AddError(string error) 
        {
            Errors.Add(error);
        }
    }
}
