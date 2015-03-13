using System;
using Soft.Core.Domain.Customers;

namespace Soft.Core.Domain.Forums
{
    /// <summary>
    /// Representa un post de un forum
    /// </summary>
    public partial class ForumPost : BaseEntity
    {
        /// <summary>
        /// Identificador del topico
        /// </summary>
        public int TopicId { get; set; }

        /// <summary>
        /// Identificador de un cliente
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Texto
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Direccion IP
        /// </summary>
        public string IpAddress { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Topico del forum
        /// </summary>
        public virtual ForumTopic ForumTopic { get; set; }

        /// <summary>
        /// Cliente
        /// </summary>
        public virtual Customer Customer { get; set; }
    }
}