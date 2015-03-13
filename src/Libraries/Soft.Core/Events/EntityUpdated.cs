namespace Soft.Core.Events
{
    /// <summary>
    /// Contenedor de entities que hayan si do actualizados
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityUpdated<T> where T : BaseEntity
    {
        public T Entity { get; private set; }

        public EntityUpdated(T entity)
        {
            Entity = entity;
        }
    }
}