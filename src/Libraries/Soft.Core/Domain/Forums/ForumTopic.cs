using System;
using Soft.Core.Domain.Customers;

namespace Soft.Core.Domain.Forums
{
    /// <summary>
    /// Representa un topico de un forum
    /// </summary>
    public partial class ForumTopic : BaseEntity
    {
        /// <summary>
        /// Identificador de un forum
        /// </summary>
        public int ForumId { get; set; }

        /// <summary>
        /// Identificador de un cliente
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Identificador de un tipo de topico
        /// </summary>
        public int TopicTypeId { get; set; }

        /// <summary>
        /// Asunto
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Numero de posts
        /// </summary>
        public int NumPosts { get; set; }

        /// <summary>
        /// Numero de vistas
        /// </summary>
        public int Views { get; set; }

        /// <summary>
        /// Identificador del ultimo post
        /// </summary>
        public int LastPostId { get; set; }

        /// <summary>
        /// Identificador del ultimo post realizado or un cliente
        /// </summary>
        public int LastPostCustomerId { get; set; }

        /// <summary>
        /// Fecha y hora del ultimo post
        /// </summary>
        public DateTime? LastPostTime { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Topico de forum
        /// Gets or sets the forum topic type
        /// </summary>
        public ForumTopicType ForumTopicType
        {
            get { return (ForumTopicType) TopicTypeId; }
            set { TopicTypeId = (int) value; }
        }

        /// <summary>
        /// El forum
        /// </summary>
        public virtual Forum Forum { get; set; }

        /// <summary>
        /// El cliente
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Numero de replicas
        /// </summary>
        public int NumReplies
        {
            get
            {
                var result = 0;
                if (NumPosts > 0)
                    result = NumPosts - 1;
                return result;
            }
        }
    }
}