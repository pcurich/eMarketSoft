using System;
using System.Collections.Generic;
using Soft.Core;
using Soft.Core.Domain.Customers;
using Soft.Core.Domain.Logging;

namespace Soft.Services.Logging
{
    /// <summary>
    /// Logger nulo
    /// </summary>
    public class NullLogger : ILogger
    {
        public bool IsEnabled(LogLevel level)
        {
            return false;
        }

        public void DeleteLog(Log log)
        {
        }

        public void ClearLog()
        {
        }

        public IPagedList<Log> GetAllLogs(DateTime? fromUtc, DateTime? toUtc, string message, LogLevel? logLevel,
            int pageIndex,
            int pageSize)
        {
            return new PagedList<Log>(new List<Log>(), pageIndex, pageSize);
        }

        public Log GetLogById(int logId)
        {
            return null;
        }

        public IList<Log> GetLogByIds(int[] logIds)
        {
            return null;
        }

        public Log InsertLog(LogLevel logLevel, string shortMessage, string fullMessage = "", Customer customer = null)
        {
            return null;
        }
    }
}