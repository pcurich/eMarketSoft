using Soft.Core.Domain.Localization;

namespace Soft.Core.Domain.Shipping
{
    /// <summary>
    /// Representa la fecha de entrega
    /// </summary>
    public partial class DeliveryDate : BaseEntity, ILocalizedEntity
    {
        /// <summary>
        /// Nombre de la entrega
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Orden de aparicion
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}