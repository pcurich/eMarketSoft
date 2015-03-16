using Soft.Core;
using Soft.Core.Events;

namespace Soft.Services.Events
{
    /// <summary>
    /// Meotods definidos para publicar los eventos a los entities
    /// </summary>
    public static class EventPublisherExtensions
    {
        /// <summary>
        /// Evento que se activa cuando un entity es insertado 
        /// </summary>
        /// <param name="eventPublisher">evento publicador</param>
        /// <param name="entity">entity sobre el que cae la accion</param>
        /// <typeparam name="T">Tipo del entity</typeparam>
        public static void EntityInserted<T>(this IEventPublisher eventPublisher, T entity) where T : BaseEntity
        {
            eventPublisher.Publish(new EntityInserted<T>(entity));
        }

        /// <summary>
        /// Evento que se activa cuando un entity es actualizado 
        /// </summary>
        /// <param name="eventPublisher">evento publicador</param>
        /// <param name="entity">entity sobre el que cae la accion</param>
        /// <typeparam name="T">Tipo del entity</typeparam>
        public static void EntityUpdated<T>(this IEventPublisher eventPublisher, T entity) where T : BaseEntity
        {
            eventPublisher.Publish(new EntityUpdated<T>(entity));
        }

        /// <summary>
        /// Evento que se activa cuando un entity es borradop 
        /// </summary>
        /// <param name="eventPublisher">evento publicador</param>
        /// <param name="entity">entity sobre el que cae la accion</param>
        /// <typeparam name="T">Tipo del entity</typeparam>
        public static void EntityDeleted<T>(this IEventPublisher eventPublisher, T entity) where T : BaseEntity
        {
            eventPublisher.Publish(new EntityDeleted<T>(entity));
        }
    }
}
