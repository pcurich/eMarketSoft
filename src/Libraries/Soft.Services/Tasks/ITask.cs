namespace Soft.Services.Tasks
{
    /// <summary>
    /// Debeeria ser implementada por cada tarea
    /// </summary>
    public partial interface ITask
    {
        /// <summary>
        /// Ejecuta la tarea
        /// </summary>
        void Execute(); 
    }
}