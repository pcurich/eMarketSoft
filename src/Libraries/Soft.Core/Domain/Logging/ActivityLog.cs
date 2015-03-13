using System;
using Soft.Core.Domain.Customers;

namespace Soft.Core.Domain.Logging
{
    /// <summary>
    /// Representa un registro de una actividad de log
    /// </summary>
    public class ActivityLog : BaseEntity
    {
        /// <summary>
        /// Tipo de actividad
        /// </summary>
        public int ActivityLogTypeId { get; set; }

        public virtual ActivityLogType ActivityLogType { get; set; }

        /// <summary>
        /// Identificador del consumidor
        /// </summary>
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Comentario de la actividad
        /// </summary>
        public string Comment { get; set; }

        public DateTime CreatedOnUtc { get; set; }
    }
}