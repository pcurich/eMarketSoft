namespace Soft.Core.Infrastructure
{
    /// <summary>
    /// Interfaz que debe ejecutarse para cada tarea creada al momento 
    /// de la ejecucion
    /// </summary>
    public interface IStartupTask
    {
        /// <summary>
        /// Ejecuta la tarea
        /// </summary>
        void Execute();

        /// <summary>
        /// Orden de la tarea
        /// </summary>
        int Order { get; }
    }
}