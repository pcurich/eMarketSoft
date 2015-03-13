using Soft.Services.Tasks;

namespace Soft.Services.Logging
{
    /// <summary>
    /// Representa una larea de borrado [log]
    /// </summary>
    public class ClearLogTask : ITask
    {
        private readonly ILogger _logger;

        public ClearLogTask(ILogger logger)
        {
            _logger = logger;
        }

        public void Execute()
        {
            _logger.ClearLog();
        }
    }
}