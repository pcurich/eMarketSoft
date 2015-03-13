namespace Soft.Core.Domain.Forums
{
    /// <summary>
    /// Representa un tipo de busqueda de un forum
    /// </summary>
    public enum ForumSearchType
    {
        /// <summary>
        /// Por titulo y texto
        /// </summary>
        All = 0,

        /// <summary>
        /// Solo titulo
        /// </summary>
        TopicTitlesOnly = 10,

        /// <summary>
        /// Solo Texto
        /// </summary>
        PostTextOnly = 20,
    }
}