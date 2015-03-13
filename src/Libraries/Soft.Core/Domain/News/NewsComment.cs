using System;
using Soft.Core.Domain.Customers;

namespace Soft.Core.Domain.News
{
    /// <summary>
    /// Representa un comentario de una noticia
    /// </summary>
    public partial class NewsComment : BaseEntity
    {
        /// <summary>
        /// Titulo del comentario
        /// </summary>
        public string CommentTitle { get; set; }

        /// <summary>
        /// Texto del comentario
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// Identificador del item de la noticia
        /// </summary>
        public int NewsItemId { get; set; }

        /// <summary>
        /// Identificador del cliente
        /// </summary>
        public int CustomerId { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Cliente
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Noticia
        /// </summary>
        public virtual NewsItem NewsItem { get; set; }
    }
}