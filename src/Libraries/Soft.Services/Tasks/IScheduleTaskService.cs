using System.Collections.Generic;
using Soft.Core.Domain.Tasks;

namespace Soft.Services.Tasks
{
    /// <summary>
    ///     Interfaz de servicio de tareas
    /// </summary>
    public interface IScheduleTaskService
    {
        /// <summary>
        ///     Borra una tarea
        /// </summary>
        /// <param name="task">Tarea</param>
        void DeleteTask(ScheduleTask task);

        /// <summary>
        ///     Retorna una tarea
        /// </summary>
        /// <param name="taskId">Identificador de una tarea</param>
        /// <returns>Task</returns>
        ScheduleTask GetTaskById(int taskId);

        /// <summary>
        ///     Obitene una tarea usando un tipo
        /// </summary>
        /// <param name="type">Tipo de tarea</param>
        /// <returns>Task</returns>
        ScheduleTask GetTaskByType(string type);

        /// <summary>
        ///     Retorna todas las tareas
        /// </summary>
        /// <param name="showHidden">
        ///     Si se esconden registros
        /// </param>
        /// <returns>Tareas</returns>
        IList<ScheduleTask> GetAllTasks(bool showHidden = false);

        /// <summary>
        ///     Inserta una tarea
        /// </summary>
        /// <param name="task">Tarea</param>
        void InsertTask(ScheduleTask task);

        /// <summary>
        ///     Actualiza una tarea
        /// </summary>
        /// <param name="task">Tarea</param>
        void UpdateTask(ScheduleTask task);
    }
}