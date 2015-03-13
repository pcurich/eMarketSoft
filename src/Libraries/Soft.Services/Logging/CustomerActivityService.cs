using System;
using System.Collections.Generic;
using System.Linq;
using Soft.Core;
using Soft.Core.Caching;
using Soft.Core.Data;
using Soft.Core.Domain.Common.Settings;
using Soft.Core.Domain.Customers;
using Soft.Core.Domain.Logging;
using Soft.Data.Ef;
using Soft.Data.Extensions;

namespace Soft.Services.Logging
{
    public class CustomerActivityService : ICustomerActivityService
    {
        #region Constantes

        /// <summary>
        /// Llave para el cache
        /// </summary>
        private const string ActivitytypeAllKey = "Soft.activitytype.all";

        /// <summary>
        /// Llave para borrar la cache
        /// </summary>
        private const string ActivitytypePatternKey = "Soft.activitytype.";

        #endregion

        #region Campos

        /// <summary>
        /// Gestion de cache
        /// </summary>
        private readonly ICacheManager _cacheManager;

        private readonly IRepository<ActivityLog> _activityLogRepository;
        private readonly IRepository<ActivityLogType> _activityLogTypeRepository;
        private readonly IWorkContext _workContext;
        private readonly IDbContext _dbContext;
        private readonly IDataProvider _dataProvider;
        private readonly CommonSettings _commonSettings;

        #endregion

        #region Ctr

        public CustomerActivityService(
            ICacheManager cacheManager,
            IRepository<ActivityLog> activityLogRepository,
            IRepository<ActivityLogType> activityLogTypeRepository,
            IWorkContext workContext,
            IDbContext dbContext,
            IDataProvider dataProvider,
            CommonSettings commonSettings)
        {
            _cacheManager = cacheManager;
            _activityLogRepository = activityLogRepository;
            _activityLogTypeRepository = activityLogTypeRepository;
            _workContext = workContext;
            _dbContext = dbContext;
            _dataProvider = dataProvider;
            _commonSettings = commonSettings;
        }

        #endregion

        #region Util

        /// <summary>
        /// Devuelve todas las actividades de log (clase para  caching)
        /// </summary>
        /// <returns>Activity log types</returns>
        protected virtual IList<ActivityLogTypeForCaching> GetAllActivityTypesCached()
        {
            //cache
            var key = string.Format(ActivitytypeAllKey);
            return _cacheManager.Get(key, () =>
            {
                var result = new List<ActivityLogTypeForCaching>();
                var activityLogTypes = GetAllActivityTypes();
                foreach (var alt in activityLogTypes)
                {
                    var altForCaching = new ActivityLogTypeForCaching
                    {
                        Id = alt.Id,
                        SystemKeyword = alt.SystemKeyword,
                        Name = alt.Name,
                        Enabled = alt.Enabled
                    };
                    result.Add(altForCaching);
                }
                return result;
            });
        }

        #endregion

        #region Inner Class

        [Serializable]
        public class ActivityLogTypeForCaching
        {
            public int Id { get; set; }
            public string SystemKeyword { get; set; }
            public string Name { get; set; }
            public bool Enabled { get; set; }
        }

        #endregion

        #region Metodos

        public void InsertActivityType(ActivityLogType activityLogType)
        {
            if (activityLogType == null)
                throw new ArgumentNullException("activityLogType");

            _activityLogTypeRepository.Insert(activityLogType);
            _cacheManager.RemoveByPattern(ActivitytypePatternKey);
        }

        public void UpdateActivityType(ActivityLogType activityLogType)
        {
            if (activityLogType == null)
                throw new ArgumentNullException("activityLogType");

            _activityLogTypeRepository.Update(activityLogType);
            _cacheManager.RemoveByPattern(ActivitytypePatternKey);
        }

        public void DeleteActivityType(ActivityLogType activityLogType)
        {
            if (activityLogType == null)
                throw new ArgumentNullException("activityLogType");

            _activityLogTypeRepository.Delete(activityLogType);
            _cacheManager.RemoveByPattern(ActivitytypePatternKey);
        }

        public IList<ActivityLogType> GetAllActivityTypes()
        {
            var query = from alt in _activityLogTypeRepository.Table
                orderby alt.Name
                select alt;

            var activityLogTypes = query.ToList();
            return activityLogTypes;
        }

        public ActivityLogType GetActivityTypeById(int activityLogTypeId)
        {
            return activityLogTypeId == 0 ? null : _activityLogTypeRepository.GetById(activityLogTypeId);
        }

        public ActivityLog InsertActivity(string systemKeyword, string comment, params object[] commentParams)
        {
            return InsertActivity(systemKeyword, comment, _workContext.CurrentCustomer, commentParams);
        }

        public ActivityLog InsertActivity(string systemKeyword, string comment, Customer customer,
            params object[] commentParams)
        {
            if (customer == null)
                return null;

            var activityTypes = GetAllActivityTypesCached();
            var activityType = activityTypes.ToList().Find(at => at.SystemKeyword == systemKeyword);
            if (activityType == null || !activityType.Enabled)
                return null;

            comment = CommonHelper.EnsureNotNull(comment);
            comment = string.Format(comment, commentParams);
            comment = CommonHelper.EnsureMaximumLength(comment, 4000);

            var activity = new ActivityLog
            {
                ActivityLogTypeId = activityType.Id,
                Customer = customer,
                Comment = comment,
                CreatedOnUtc = DateTime.UtcNow
            };

            _activityLogRepository.Insert(activity);

            return activity;
        }

        public void DeleteActivity(ActivityLog activityLog)
        {
            if (activityLog == null)
                throw new ArgumentNullException("activityLog");

            _activityLogRepository.Delete(activityLog);
        }

        /// <summary>
        /// Optiene todas las actividades de log
        /// </summary>
        /// <param name="createdOnFrom">Desde la fecha de creacion; null carga todos los clientes</param>
        /// <param name="createdOnTo">Hasta la fecha de creacion; null carga todos los clientes</param>
        /// <param name="customerId">Identificador del cliente; null carga todos los clientes</param>
        /// <param name="activityLogTypeId">Identificador del tipo de actividad</param>
        /// <param name="pageIndex">Indice de la paguina</param>
        /// <param name="pageSize">Tamaño de paguina</param>
        /// <returns>
        /// Paguinas de las actividades de log
        /// </returns>
        public IPagedList<ActivityLog> GetAllActivities(DateTime? createdOnFrom, DateTime? createdOnTo, int? customerId,
            int activityLogTypeId,
            int pageIndex, int pageSize)
        {
            var query = _activityLogRepository.Table;

            if (customerId.HasValue)
                query = query.Where(al => customerId.Value == al.CustomerId);

            if (activityLogTypeId > 0)
                query = query.Where(al => activityLogTypeId == al.ActivityLogTypeId);

            if (createdOnFrom.HasValue)
                query = query.Where(al => createdOnFrom.Value <= al.CreatedOnUtc);

            if (createdOnTo.HasValue)
                query = query.Where(al => createdOnTo.Value >= al.CreatedOnUtc);

            query = query.OrderByDescending(al => al.CreatedOnUtc);

            var activityLog = new PagedList<ActivityLog>(query, pageIndex, pageSize);

            return activityLog;
        }

        public ActivityLog GetActivityById(int activityLogId)
        {
            return activityLogId == 0 ? null : _activityLogRepository.GetById(activityLogId);
        }

        public void ClearAllActivities()
        {
            if (_commonSettings.UseStoredProceduresIfSupported && _dataProvider.StoredProceduredSupported)
            {
                //although it's not a stored procedure we use it to ensure that a database supports them
                //we cannot wait until EF team has it implemented - http://data.uservoice.com/forums/72025-entity-framework-feature-suggestions/suggestions/1015357-batch-cud-support


                //do all databases support "Truncate command"?
                string activityLogTableName = _dbContext.GetTableName<ActivityLog>();
                _dbContext.ExecuteSqlCommand(String.Format("TRUNCATE TABLE [{0}]", activityLogTableName));
            }
            else
            {
                var activityLog = _activityLogRepository.Table.ToList();
                foreach (var activityLogItem in activityLog)
                    _activityLogRepository.Delete(activityLogItem);
            }
        }

        #endregion
    }
}