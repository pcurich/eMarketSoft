using System.Collections.Generic;
using Soft.Core.Infrastructure;

namespace Soft.Services.Events
{
    public class SubscriptionService : ISubscriptionService
    {

        public IList<IConsumer<T>> GetSubscriptions<T>()
        {
            return EngineContext.Current.ResolveAll<IConsumer<T>>();
        }
    }
}
