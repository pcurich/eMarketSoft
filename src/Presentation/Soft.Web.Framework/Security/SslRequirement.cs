namespace Soft.Web.Framework.Security
{
    public enum SslRequirement
    {
        /// <summary>
        /// Paguina deberia ser segura
        /// </summary>
        Yes,
        /// <summary>
        /// Paguinan o deberia ser segura
        /// </summary>
        No,
        /// <summary>
        /// No importa (as requested)
        /// </summary>
        NoMatter,
    }
}