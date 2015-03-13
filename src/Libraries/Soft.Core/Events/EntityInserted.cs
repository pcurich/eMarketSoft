namespace Soft.Core.Events
{
    /// <summary>
    /// Contenedor de entities que hayan si do insertados
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityInserted<T> where T : BaseEntity
    {
        public T Entity { get; private set; }

        public EntityInserted(T entity)
        {
            Entity = entity;
        }
    }
}