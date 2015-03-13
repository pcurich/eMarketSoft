using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Soft.Data.Extensions
{
    public static class SoftQueryableExtensions
    {
        /// <summary>
        /// Include
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="queryable">Queryable</param>
        /// <param name="includeProperties">Lista de propiedades a incluir</param>
        /// <returns>nuevo querable</returns>
        public static IQueryable<T> IncludeProperties<T>(this IQueryable<T> queryable,
            params Expression<Func<T, object>>[] includeProperties)
        {
            if (queryable == null)
                throw new ArgumentNullException("queryable");

            return includeProperties.Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}