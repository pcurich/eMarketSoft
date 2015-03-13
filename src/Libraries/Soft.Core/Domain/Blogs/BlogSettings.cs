using Soft.Core.Configuration;

namespace Soft.Core.Domain.Blogs
{
    public class BlogSettings : ISettings
    {
        /// <summary>
        /// Si el blog esta activo
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Tamaño de la pagina para el post
        /// </summary>
        public int PostsPageSize { get; set; }

        /// <summary>
        /// Usuarios no registrados pueden comentar
        /// </summary>
        public bool AllowNotRegisteredUsersToLeaveComments { get; set; }

        /// <summary>
        /// Si se va a notificar cuando haya un nuevo comentario
        /// </summary>
        public bool NotifyAboutNewBlogComments { get; set; }

        /// <summary>
        /// Numero de tags que apareceran en la seccion de tags
        /// </summary>
        public int NumberOfTags { get; set; }

        /// <summary>
        /// Si se va a activar el RSS en los browser de los clientes
        /// </summary>
        public bool ShowHeaderRssUrl { get; set; }
    }
}