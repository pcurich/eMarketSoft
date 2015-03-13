using System;
using System.Collections.Generic;
using Soft.Core;
using Soft.Core.Domain.Customers;
using Soft.Core.Domain.Logging;

namespace Soft.Services.Logging
{
    /// <summary>
    /// Interface del servicio de las actividades de un cliente
    /// </summary>
    public interface ICustomerActivityService
    {
        /// <summary>
        /// Inserta una actividad del tipo log
        /// </summary>
        /// <param name="activityLogType">Activity log type item</param>
        void InsertActivityType(ActivityLogType activityLogType);

        /// <summary>
        /// Actualiza una actividad de un tipo log
        /// </summary>
        /// <param name="activityLogType">Activity log type item</param>
        void UpdateActivityType(ActivityLogType activityLogType);

        /// <summary>
        /// Borra una actividad de log
        /// </summary>
        /// <param name="activityLogType">Activity log type</param>
        void DeleteActivityType(ActivityLogType activityLogType);

        /// <summary>
        /// Devuelve todas las actividades de log
        /// </summary>
        /// <returns>Activity log type collection</returns>
        IList<ActivityLogType> GetAllActivityTypes();

        /// <summary>
        /// Optiene una especifica actividad de log
        /// </summary>
        /// <param name="activityLogTypeId">Activity log type identifier</param>
        /// <returns>Activity log type item</returns>
        ActivityLogType GetActivityTypeById(int activityLogTypeId);

        /// <summary>
        /// Inserta una actividad de log
        /// </summary>
        /// <param name="systemKeyword">The system keyword</param>
        /// <param name="comment">The activity comment</param>
        /// <param name="commentParams">The activity comment parameters for string.Format() function.</param>
        /// <returns>Activity log item</returns>
        ActivityLog InsertActivity(string systemKeyword, string comment, params object[] commentParams);

        /// <summary>
        /// Inserta una actividad de log
        /// </summary>
        /// <param name="systemKeyword">The system keyword</param>
        /// <param name="comment">The activity comment</param>
        /// <param name="customer">The customer</param>
        /// <param name="commentParams">The activity comment parameters for string.Format() function.</param>
        /// <returns>Activity log item</returns>
        ActivityLog InsertActivity(string systemKeyword,string comment, Customer customer, params object[] commentParams);

        /// <summary>
        /// Borra una actividad de log
        /// </summary>
        /// <param name="activityLog">Activity log</param>
        void DeleteActivity(ActivityLog activityLog);

        /// <summary>
        /// Optiene todas las actividades de log
        /// </summary>
        /// <param name="createdOnFrom">Desde la fecha de creacion; null carga todos los clientes</param>
        /// <param name="createdOnTo">Hasta la fecha de creacion; null carga todos los clientes</param>
        /// <param name="customerId">Identificador del cliente; null carga todos los clientes</param>
        /// <param name="activityLogTypeId">Identificador del tipo de actividad;cero carga todas las actividades</param>
        /// <param name="pageIndex">Indice de la paguina</param>
        /// <param name="pageSize">Tamaño de paguina</param>
        /// <returns>Paguinas de las actividades de log</returns>
        IPagedList<ActivityLog> GetAllActivities(DateTime? createdOnFrom,
            DateTime? createdOnTo, int? customerId,
            int activityLogTypeId, int pageIndex, int pageSize);

        /// <summary>
        /// Optiene una actividad de log por id
        /// </summary>
        /// <param name="activityLogId">Activity log identifier</param>
        /// <returns>Activity log item</returns>
        ActivityLog GetActivityById(int activityLogId);

        /// <summary>
        /// Borra las actividades de log
        /// </summary>
        void ClearAllActivities();
    }
}