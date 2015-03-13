using System;
using System.Collections.Generic;
using Soft.Core.Domain.Localization;
using Soft.Core.Domain.Seo;
using Soft.Core.Domain.Stores;

namespace Soft.Core.Domain.Blogs
{
    /// <summary>
    /// Representa un post de blogs
    /// </summary>
    public partial class BlogPost : BaseEntity, ISlugSupported, IStoreMappingSupported
    {
        private ICollection<BlogComment> _blogComments;

        /// <summary>
        /// Identificador del idioma
        /// </summary>
        public int LanguageId { get; set; }

        /// <summary>
        /// Titulo del blog
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Cuerpo del blog
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Si se permite comentarios
        /// </summary>
        public bool AllowComments { get; set; }

        /// <summary>
        /// Cantidad de comentarios
        /// <remarks>
        /// Se usa para optimizar
        /// </remarks>
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// Tag para los blogs
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Fecha en que inicia un post
        /// </summary>
        public DateTime? StartDateUtc { get; set; }

        /// <summary>
        /// Fecha en que termina el post
        /// </summary>
        public DateTime? EndDateUtc { get; set; }

        /// <summary>
        /// Meta Keywords
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Meta Descripcion
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Meta Titulo
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// Indica si el entity esta limitado a una tienda
        /// </summary>
        public virtual bool LimitedToStores { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Comentarios de un blog
        /// </summary>
        public virtual ICollection<BlogComment> BlogComments
        {
            get { return _blogComments ?? (_blogComments = new List<BlogComment>()); }
            protected set { _blogComments = value; }
        }

        /// <summary>
        /// El idioma
        /// </summary>
        public virtual Language Language { get; set; }
    }
}