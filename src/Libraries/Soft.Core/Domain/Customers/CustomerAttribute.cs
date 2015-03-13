using System.Collections.Generic;
using Soft.Core.Domain.Catalog;
using Soft.Core.Domain.Localization;

namespace Soft.Core.Domain.Customers
{
    /// <summary>
    /// Representa un atributo de un cliente
    /// </summary>
    public partial class CustomerAttribute : BaseEntity, ILocalizedEntity
    {
        private ICollection<CustomerAttributeValue> _customerAttributeValues;

        /// <summary>
        /// Nombre de un atributo
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Si el atributo es requerido
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Identificador del tipo de control
        /// </summary>
        public int AttributeControlTypeId { get; set; }

        /// <summary>
        /// Orden para aparecer
        /// </summary>
        public int DisplayOrder { get; set; }


        /// <summary>
        /// Tipo de control del atributo
        /// </summary>
        public AttributeControlType AttributeControlType
        {
            get { return (AttributeControlType) AttributeControlTypeId; }
            set { AttributeControlTypeId = (int) value; }
        }

        /// <summary>
        /// Valores de los atributos de los clientes
        /// </summary>
        public virtual ICollection<CustomerAttributeValue> CustomerAttributeValues
        {
            get { return _customerAttributeValues ?? (_customerAttributeValues = new List<CustomerAttributeValue>()); }
            protected set { _customerAttributeValues = value; }
        }
    }
}