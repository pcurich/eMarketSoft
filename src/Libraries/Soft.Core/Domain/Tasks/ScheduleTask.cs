using System;

namespace Soft.Core.Domain.Tasks
{
    public class ScheduleTask : BaseEntity
    {
        /// <summary>
        /// Nombre de la tarea
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Periodo en el que corre (en segundos)
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// Tipo de la tarea
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Si esta activa
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Si la tarea debe parar si sucede un error
        /// </summary>
        public bool StopOnError { get; set; }

        public DateTime? LastStartUtc { get; set; }

        public DateTime? LastEndUtc { get; set; }

        public DateTime? LastSuccessUtc { get; set; }
    }
}