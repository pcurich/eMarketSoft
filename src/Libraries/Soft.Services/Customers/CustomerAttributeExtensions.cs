
using Soft.Core.Domain.Catalog;
using Soft.Core.Domain.Customers;

namespace Soft.Services.Customers
{
    /// <summary>
    /// Extensiones
    /// </summary>
    public static class CustomerAttributeExtensions
    {
        /// <summary>
        /// Indica si un atributo de un cliente deberia tener valores
        /// </summary>
        /// <param name="customerAttribute">Atributo de un cliente</param>
        /// <returns>Result</returns>
        public static bool ShouldHaveValues(this CustomerAttribute customerAttribute)
        {
            if (customerAttribute == null)
                return false;

            if (customerAttribute.AttributeControlType == AttributeControlType.TextBox ||
                customerAttribute.AttributeControlType == AttributeControlType.MultilineTextbox ||
                customerAttribute.AttributeControlType == AttributeControlType.Datepicker ||
                customerAttribute.AttributeControlType == AttributeControlType.FileUpload)
                return false;

            //Otros control de tipos soportan valores
            return true;
        }
    }
}
