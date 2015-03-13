using System.Collections.Generic;

namespace Soft.Services.Payments
{
    /// <summary>
    /// Represents a CancelRecurringPaymentResult
    /// </summary>
    public partial class CancelRecurringPaymentResult
    {
        public IList<string> Errors { get; set; }

        public CancelRecurringPaymentResult() 
        {
            Errors = new List<string>();
        }

        public bool Success
        {
            get { return (Errors.Count == 0); }
        }

        public void AddError(string error) 
        {
            Errors.Add(error);
        }
    }
}
