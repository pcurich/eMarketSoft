using System;

namespace Soft.Core.Domain.Forums
{
    /// <summary>
    /// Represents a forum
    /// </summary>
    public partial class Forum : BaseEntity
    {
        /// <summary>
        /// Identificador del grupo del forum
        /// </summary>
        public int ForumGroupId { get; set; }

        /// <summary>
        /// Nombre del forum
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descripcion del forum
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Numeros de topicos
        /// </summary>
        public int NumTopics { get; set; }

        /// <summary>
        /// Numero de posts
        /// </summary>
        public int NumPosts { get; set; }

        /// <summary>
        /// Identificador del ultimo topico
        /// </summary>
        public int LastTopicId { get; set; }

        /// <summary>
        /// Identificador del ultimo post
        /// </summary>
        public int LastPostId { get; set; }

        /// <summary>
        /// Identificador del ultimo post por un cliente
        /// </summary>
        public int LastPostCustomerId { get; set; }

        /// <summary>
        /// Fecha del ultimo post
        /// </summary>
        public DateTime? LastPostTime { get; set; }

        /// <summary>
        /// Orden de aparicion
        /// </summary>
        public int DisplayOrder { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Gets the ForumGroup
        /// </summary>
        public virtual ForumGroup ForumGroup { get; set; }
    }
}