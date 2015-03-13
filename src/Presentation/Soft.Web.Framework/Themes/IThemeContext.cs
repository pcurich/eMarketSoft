namespace Soft.Web.Framework.Themes
{
    /// <summary>
    /// Contexto de trabajo
    /// </summary>
    public interface IThemeContext
    {
        /// <summary>
        /// Obtiene el nombre del thema del sistema actual 
        /// </summary>
        string WorkingThemeName { get; set; }
    }
}