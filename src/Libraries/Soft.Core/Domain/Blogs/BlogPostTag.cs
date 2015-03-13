namespace Soft.Core.Domain.Blogs
{
    /// <summary>
    /// Tag de un blog
    /// </summary>
    public partial class BlogPostTag
    {
        /// <summary>
        /// Nombre del tag
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// La cantidad de productos con tags
        /// </summary>
        public int BlogPostCount { get; set; }
    }
}