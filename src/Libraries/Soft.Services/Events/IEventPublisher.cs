namespace Soft.Services.Events
{
    /// <summary>
    /// Editor de Eventos
    /// </summary>
    public interface IEventPublisher
    {
        /// <summary>
        /// Publicar el evento
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventMessage"></param>
        void Publish<T>(T eventMessage);
    }
}