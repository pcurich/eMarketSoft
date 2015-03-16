using Soft.Core;
using Soft.Core.Data;
using Soft.Core.Infrastructure;

namespace Soft.Data.Entities
{
    public class EfStartUpTask : IStartupTask
    {
        public void Execute()
        {
            var settings = EngineContext.Current.Resolve<DataSettings>();
            if (settings != null && settings.IsValid())
            {
                var provider = EngineContext.Current.Resolve<IDataProvider>();
                if (provider == null)
                    throw new SoftException("No IDataProvider found");
                provider.SetDatabaseInitializer();
            }
        }

        public int Order
        {
            //Esta tarea debe ser ejecutada primero
            get { return -1000; }
        }
    }
}
