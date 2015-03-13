using System;
using System.Collections.Generic;
using Soft.Core;
using Soft.Core.Domain.Customers;
using Soft.Core.Domain.Logging;

namespace Soft.Services.Logging
{
    /// <summary>
    /// Interfaz de logeo
    /// </summary>
    public interface ILogger
    {
     /// <summary>
     /// Determina cualquier nivel de logeo esta activo
     /// </summary>
     /// <param name="level"></param>
     /// <returns></returns>
        bool IsEnabled(LogLevel level);

        /// <summary>
        /// Norra los itmes de log
        /// </summary>
        /// <param name="log"></param>
        void DeleteLog(Log log);

        /// <summary>
        /// Borra todos los logs
        /// </summary>
        void ClearLog();

        /// <summary>
        /// Obtiene todos los items de log
        /// </summary>
        /// <param name="fromUtc">Log de creados desde; null carga a todos</param>
        /// <param name="toUtc">Log de creados hasta; null carga a todos</param>
        /// <param name="message">Mensaje</param>
        /// <param name="logLevel">Nivel de Log; null devuelve todos</param>
        /// <param name="pageIndex">Indice de pagina</param>
        /// <param name="pageSize">Tamaño de pagina</param>
        /// <returns></returns>
        IPagedList<Log> GetAllLogs(DateTime? fromUtc, DateTime? toUtc,
            string message, LogLevel? logLevel, int pageIndex, int pageSize);

        /// <summary>
        /// Optiene un item de log
        /// </summary>
        /// <param name="logId">Identificador de log</param>
        /// <returns>Log Item</returns>
        Log GetLogById(int logId);

        /// <summary>
        /// Optiene los item por identificadores
        /// </summary>
        /// <param name="logIds"></param>
        /// <returns></returns>
        IList<Log> GetLogByIds(int[] logIds);

        /// <summary>
        /// Inseerta un item de log
        /// </summary>
        /// <param name="logLevel">nivel de log</param>
        /// <param name="shortMessage">Mensaje corto</param>
        /// <param name="fullMessage">Mensaje completo</param>
        /// <param name="customer">El consumidor asociado al registro del log</param>
        /// <returns>Log Insertado</returns>
        Log InsertLog(LogLevel logLevel, string shortMessage, string fullMessage = "", Customer customer = null);
    }
}