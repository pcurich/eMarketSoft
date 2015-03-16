namespace Soft.Core.Plugins
{
    /// <summary>
    /// Modo de carga de los plugins
    /// </summary>
    public enum LoadPluginsMode
    {
        /// <summary>
        /// Todos los instalados y no instalados
        /// </summary>
        All = 0,

        /// <summary>
        /// Solo instalados
        /// </summary>
        InstalledOnly = 10,

        /// <summary>
        /// No instalados solamente
        /// </summary>
        NotInstalledOnly = 20
    }
}