using Soft.Core.Domain.Customers;
using Soft.Core.Domain.Directory;
using Soft.Core.Domain.Localization;
using Soft.Core.Domain.Tax;
using Soft.Core.Domain.Vendors;

namespace Soft.Core
{
    /// <summary>
    /// Contexto de trabajo
    /// </summary>
    public interface IWorkContext
    {
        /// <summary>
        /// Cliente actual
        /// </summary>
        Customer CurrentCustomer { get; set; }

        /// <summary>
        /// Establece o devuelve el cliente original (en caso de que el actual este suplantado)
        /// </summary>
        Customer OriginalCustomerIfImpersonated { get; }

        /// <summary>
        /// Vendedor actual (logged-in manager)
        /// </summary>
        Vendor CurrentVendor { get; }

        /// <summary>
        /// Lenguaje del usuario actual 
        /// </summary>
        Language WorkingLanguage { get; set; }

        /// <summary>
        /// Moneda del usuario actual 
        /// </summary>
        Currency WorkingCurrency { get; set; }

        /// <summary>
        /// Tipo de impuesto
        /// </summary>
        TaxDisplayType TaxDisplayType { get; set; }

        /// <summary>
        /// Si es adminitrador
        /// </summary>
        bool IsAdmin { get; set; }
    }
}