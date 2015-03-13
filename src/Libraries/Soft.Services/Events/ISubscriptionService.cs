using System.Collections.Generic;

namespace Soft.Services.Events
{
    /// <summary>
    /// Evento de servicios de subcripcion
    /// </summary>
    public interface ISubscriptionService
    {
        /// <summary>
        /// Obtiene las subscripciones
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IList<IConsumer<T>> GetSubscriptions<T>();
    }
}