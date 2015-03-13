using System;
using Soft.Core.Domain.Customers;

namespace Soft.Core.Domain.Forums
{
    /// <summary>
    /// Representa una suscripcion a un foro
    /// </summary>
    public partial class ForumSubscription : BaseEntity
    {
        /// <summary>
        /// Identificador de la suscripcion
        /// </summary>
        public Guid SubscriptionGuid { get; set; }

        /// <summary>
        /// Identificador del cliente
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Identificador del forum
        /// </summary>
        public int ForumId { get; set; }

        /// <summary>
        /// Identificador del topico
        /// </summary>
        public int TopicId { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Cliente
        /// </summary>
        public virtual Customer Customer { get; set; }
    }
}