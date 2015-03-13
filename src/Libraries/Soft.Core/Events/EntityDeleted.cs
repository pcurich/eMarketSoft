namespace Soft.Core.Events
{
    /// <summary>
    /// Contenedor para pasar entities que hayan sido borrados.
    /// Esto no es usado por entities que hayan sido borrados logicamente via
    /// por volumna bit
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityDeleted<T> where T : BaseEntity
    {
        public T Entity { get; private set; }

        public EntityDeleted(T entity)
        {
            Entity = entity;
        }
    }
}