using System;
using Soft.Core.Domain.Customers;

namespace Soft.Core.Domain.Logging
{
    public class Log : BaseEntity
    {
        /// <summary>
        /// Nivel del log
        /// </summary>
        public int LogLevelId { get; set; }

        public LogLevel LogLevel
        {
            get { return (LogLevel) LogLevelId; }
            set { LogLevelId = (int) value; }
        }

        /// <summary>
        /// Identificador del consumidor del log
        /// </summary>
        public int? CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public string ReferrerUrl { get; set; }
        public string PageUrl { get; set; }
        public string IpAddress { get; set; }

        public string ShortMessage { get; set; }
        public string FullMessage { get; set; }

        public DateTime CreatedOnUtc { get; set; }
    }
}