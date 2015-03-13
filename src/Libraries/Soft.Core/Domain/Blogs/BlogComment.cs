using System;
using Soft.Core.Domain.Customers;

namespace Soft.Core.Domain.Blogs
{
    /// <summary>
    /// Representa un comentario de un blog
    /// </summary>
    public partial class BlogComment : BaseEntity
    {
        /// <summary>
        /// Identifica a un cliente
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Texto de comentario
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// Identificador de un post de un blog
        /// </summary>
        public int BlogPostId { get; set; }

        /// <summary>
        /// Fecha de creacion de la instancia
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// El cliente
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Post de un blog
        /// </summary>
        public virtual BlogPost BlogPost { get; set; }
    }
}