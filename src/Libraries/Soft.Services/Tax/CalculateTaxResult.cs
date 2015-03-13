using System.Collections.Generic;

namespace Soft.Services.Tax
{
    /// <summary>
    /// Represents a result of tax calculation
    /// </summary>
    public partial class CalculateTaxResult
    {
        public CalculateTaxResult()
        {
            Errors = new List<string>();
        }

        /// <summary>
        /// Gets or sets a tax rate
        /// </summary>
        public decimal TaxRate { get; set; }

        /// <summary>
        /// Gets or sets an address
        /// </summary>
        public IList<string> Errors { get; set; }

        public bool Success
        {
            get 
            { 
                return Errors.Count == 0; 
            }
        }

        public void AddError(string error)
        {
            Errors.Add(error);
        }
    }
}
