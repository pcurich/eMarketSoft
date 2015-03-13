using Soft.Core.Domain.Localization;

namespace Soft.Core.Domain.Customers
{
    /// <summary>
    /// Representa un valor de un atributo de un cliente
    /// </summary>
    public partial class CustomerAttributeValue : BaseEntity, ILocalizedEntity
    {
        /// <summary>
        /// Identificador del atrinuto de un cliente
        /// </summary>
        public int CustomerAttributeId { get; set; }

        /// <summary>
        /// Nombre del the checkout attributo
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Si debe estar pre-seleccionado
        /// </summary>
        public bool IsPreSelected { get; set; }

        /// <summary>
        /// Orden de aparicion
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Atributo del cliente
        /// </summary>
        public virtual CustomerAttribute CustomerAttribute { get; set; }
    }
}