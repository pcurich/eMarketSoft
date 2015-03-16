using System;
using System.Collections.Generic;
using System.Linq;
using Soft.Core;
using Soft.Core.Data;
using Soft.Core.Domain.Common.Settings;
using Soft.Core.Domain.Customers;
using Soft.Core.Domain.Logging;
using Soft.Data.Entities;
using Soft.Data.Extensions;

namespace Soft.Services.Logging
{
    public class DefaultLogger : ILogger
    {
        #region Campos

        private readonly IRepository<Log> _logRepository;
        private readonly IWebHelper _webHelper;
        private readonly IDbContext _dbContext;
        private readonly IDataProvider _dataProvider;
        private readonly CommonSettings _commonSettings;

        #endregion

        #region Ctr

        public DefaultLogger(IRepository<Log> logRepository,
            IWebHelper webHelper, 
            IDbContext dbContext, 
            IDataProvider dataProvider, 
            CommonSettings commonSettings)
        {
            _logRepository = logRepository;
            _webHelper = webHelper;
            _dbContext = dbContext;
            _dataProvider = dataProvider;
            _commonSettings = commonSettings;
        }

        #endregion

        #region Util
        /// <summary>
        /// Retorna un valor indicando indicando si el mensaje no deberia ser loggeable
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual bool IgnoreLog(string message)
        {
            if (_commonSettings.IgnoreLogWordlist.Count == 0)
                return false;

            if (string.IsNullOrWhiteSpace(message))
                return false;

            return _commonSettings
                .IgnoreLogWordlist
                .Any(x => message.IndexOf(x, StringComparison.InvariantCultureIgnoreCase) >= 0);
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Deterina si el nivel de log esta activo
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return false;
                default:
                    return true;
            }
        }

        /// <summary>
        /// Borra un item de log
        /// </summary>
        /// <param name="log"></param>
        public void DeleteLog(Log log)
        {
            if (log == null)
                throw new ArgumentNullException("log");

            _logRepository.Delete(log);
        }

        /// <summary>
        /// Borra el log 
        /// </summary>
        public void ClearLog()
        {
            if (_commonSettings.UseStoredProceduresIfSupported && _dataProvider.StoredProceduredSupported)
            {
                //Nos aseguramos que la base de datos soporte store procedure
                //no se puede esperar hasta que el equipo de EF lo haga http://data.uservoice.com/forums/72025-entity-framework-feature-suggestions/suggestions/1015357-batch-cud-support
                var logTableName = _dbContext.GetTableName<Log>();
                _dbContext.ExecuteSqlCommand(String.Format("TRUNCATE TABLE [{0}]", logTableName));
            }
            else
            {
                var log = _logRepository.Table.ToList();
                foreach (var logItem in log)
                    _logRepository.Delete(logItem);
            }
        }

        /// <summary>
        /// Optiene todos los items de log
        /// </summary>
        /// <param name="fromUtc"></param>
        /// <param name="toUtc"></param>
        /// <param name="message"></param>
        /// <param name="logLevel"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<Log> GetAllLogs(DateTime? fromUtc, DateTime? toUtc, string message, LogLevel? logLevel,
            int pageIndex,
            int pageSize)
        {
            var query = _logRepository.Table;
            
            if (logLevel.HasValue)
            {
                var logLevelId = (int)logLevel.Value;
                query = query.Where(l => logLevelId == l.LogLevelId);
            }
            
            if (fromUtc.HasValue)
                query = query.Where(l => fromUtc.Value <= l.CreatedOnUtc);
            
            if (toUtc.HasValue)
                query = query.Where(l => toUtc.Value >= l.CreatedOnUtc);
            
            if (!String.IsNullOrEmpty(message))
                query = query.Where(l => l.ShortMessage.Contains(message) || l.FullMessage.Contains(message));

            query = query.OrderByDescending(l => l.CreatedOnUtc);

            var log = new PagedList<Log>(query, pageIndex, pageSize);

            return log;
        }

        public Log GetLogById(int logId)
        {
            return logId == 0 ? null : _logRepository.GetById(logId);
        }

        public IList<Log> GetLogByIds(int[] logIds)
        {
            if (logIds == null || logIds.Length == 0)
                return new List<Log>();

            var query = from l in _logRepository.Table
                        where logIds.Contains(l.Id)
                        select l;
            var logItems = query.ToList();
            //sort by passed identifiers
            var sortedLogItems = new List<Log>();
            foreach (int id in logIds)
            {
                var log = logItems.Find(x => x.Id == id);
                if (log != null)
                    sortedLogItems.Add(log);
            }
            return sortedLogItems;
        }

        public Log InsertLog(LogLevel logLevel, string shortMessage, string fullMessage = "", Customer customer = null)
        {
            if (IgnoreLog(shortMessage) || IgnoreLog(fullMessage))
                return null;

            var log = new Log
            {
                LogLevel = logLevel,
                ShortMessage = shortMessage,
                FullMessage = fullMessage,
                IpAddress = _webHelper.GetCurrentIpAddress(),
                Customer = customer,
                PageUrl = _webHelper.GetThisPageUrl(true),
                ReferrerUrl = _webHelper.GetUrlReferrer(),
                CreatedOnUtc = DateTime.UtcNow
            };

            _logRepository.Insert(log);

            return log;
        }
    }
        #endregion
}