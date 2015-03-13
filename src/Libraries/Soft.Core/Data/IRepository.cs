using System.Collections.Generic;
using System.Linq;

namespace Soft.Core.Data
{
    /// <summary>
    /// Repositorio
    /// </summary>
    public partial interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Obtiene el entity por el identificador
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns>Entity</returns>
        T GetById(object id);

        /// <summary>
        /// Inserta el entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Insert(T entity);

        /// <summary>
        /// Inserta varios Entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Insert(IEnumerable<T> entities);

        /// <summary>
        /// Actualiza el entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(T entity);

        /// <summary>
        /// Borra el entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(T entity);

        /// <summary>
        /// Borra varios entities
        /// </summary>
        /// <param name="entities">entities</param>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        /// Obtiene una table
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// Obtiene tablas con "No Traking" activo (EF caracteristica). Usar este cuando se va a utilizar recursos de solo lectura
        /// </summary>
        IQueryable<T> TableNoTracking { get; }
    }
}