using System;

namespace Soft.Core.Domain.Directory
{
    /// <summary>
    /// Representa el tipo de cambio
    /// </summary>
    public class ExchangeRate
    {
        public string CurrencyCode { get; set; }
        public decimal Rate { get; set; }
        public DateTime UpdatedOn { get; set; }

        public ExchangeRate()
        {
            CurrencyCode = string.Empty;
            Rate = 1.0m;
        }

        /// <summary>
        /// Formato del tipo de cambio "USD 0.72543"
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", CurrencyCode, Rate);
        }
    }
}