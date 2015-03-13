namespace Soft.Core.Domain.Seo
{
    /// <summary>
    /// Representa un requerimiento WWW
    /// </summary>
    public enum WwwRequirement
    {
        /// <summary>
        /// No importa (No hacer nada)
        /// </summary>
        NoMatter = 0,

        /// <summary>
        /// Las paginas deben tener el prefijo WWW
        /// </summary>
        WithWww = 10,

        /// <summary>
        /// Paginas no deberian tener el profijo WWW 
        /// </summary>
        WithoutWww = 20,
    }
}