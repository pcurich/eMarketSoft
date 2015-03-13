using System;
using Autofac;
using Soft.Core.Domain.Tasks;
using Soft.Core.Infrastructure;
using Soft.Services.Logging;

namespace Soft.Services.Tasks
{
    /// <summary>
    /// Tarea
    /// </summary>
    public partial class Task
    {
        /// <summary>
        /// Nueva instancia de la clase <see cref="Task"/>
        /// con el estado de activado
        /// </summary>
        public Task()
        {
            Enabled = true;
        }

        /// <summary>
        /// Nueva instancia de la clase <see cref="Task"/>
        /// </summary>
        /// <param name="task">La tarea.</param>
        public Task(ScheduleTask task)
        {
            Type = task.Type;
            Enabled = task.Enabled;
            StopOnError = task.StopOnError;
            Name = task.Name;
        }

        private ITask CreateTask(ILifetimeScope scope)
        {
            ITask task = null;
            if (Enabled)
            {
                var type2 = System.Type.GetType(Type);
                if (type2 != null)
                {
                    object instance;
                    if (!EngineContext.Current.ContainerManager.TryResolve(type2, scope, out instance))
                    {
                        //not resolved
                        instance = EngineContext.Current.ContainerManager.ResolveUnregistered(type2, scope);
                    }
                    task = instance as ITask;
                }
            }
            return task;
        }

        /// <summary>
        /// Ejecuta una tarea
        /// </summary>
        /// <param name="throwException">Si es <c>true</c> arroja un [throw exception].</param>
        /// <param name="dispose">Si es <c>true</c> todas las tareas seran [dispose] despues de su ejecucion.</param>
        public void Execute(bool throwException = false, bool dispose = true)
        {
            IsRunning = true;

            //background tasks has an issue with Autofac
            //because scope is generated each time it's requested
            //that's why we get one single scope here
            //this way we can also dispose resources once a task is completed
            var scope = EngineContext.Current.ContainerManager.Scope();
            var scheduleTaskService = EngineContext.Current.ContainerManager.Resolve<IScheduleTaskService>("", scope);
            var scheduleTask = scheduleTaskService.GetTaskByType(Type);

            try
            {
                var task = CreateTask(scope);
                if (task != null)
                {
                    LastStartUtc = DateTime.UtcNow;
                    if (scheduleTask != null)
                    {
                        //update appropriate datetime properties
                        scheduleTask.LastStartUtc = LastStartUtc;
                        scheduleTaskService.UpdateTask(scheduleTask);
                    }

                    //execute task
                    task.Execute();
                    LastEndUtc = LastSuccessUtc = DateTime.UtcNow;
                }
            }
            catch (Exception exc)
            {
                Enabled = !StopOnError;
                LastEndUtc = DateTime.UtcNow;

                //log error
                var logger = EngineContext.Current.ContainerManager.Resolve<ILogger>("", scope);
                logger.Error(string.Format("Error mientras corre la '{0}' tarea programada. {1}", Name, exc.Message), exc);
                if (throwException)
                    throw;
            }

            if (scheduleTask != null)
            {
                //update appropriate datetime properties
                scheduleTask.LastEndUtc = LastEndUtc;
                scheduleTask.LastSuccessUtc = LastSuccessUtc;
                scheduleTaskService.UpdateTask(scheduleTask);
            }

            //dispose all resources
            if (dispose)
            {
                scope.Dispose();
            }

            IsRunning = false;
        }

        /// <summary>
        /// Si la tarea esta corriendo
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Ultimo Inicio
        /// </summary>
        public DateTime? LastStartUtc { get; private set; }

        /// <summary>
        /// Ultima ejecucion
        /// </summary>
        public DateTime? LastEndUtc { get; private set; }

        /// <summary>
        /// Ultima tarea exitosa
        /// </summary>
        public DateTime? LastSuccessUtc { get; private set; }

        /// <summary>
        /// Tipo de tarea
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Si se debe detener una tarea si ocurre un error
        /// </summary>
        public bool StopOnError { get; private set; }

        /// <summary>
        /// Nombre de la tarea
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Si la tarea esta activa
        /// </summary>
        public bool Enabled { get; set; }
    }
}