using System.Collections.Generic;
using System.Data.Entity;
using Soft.Core;

namespace Soft.Data.Ef
{
    public interface IDbContext
    {
        /// <summary>
        /// Obtienen el DbSet
        /// </summary>
        /// <typeparam name="TEntity">Tipo del Entity</typeparam>
        /// <returns>DbSet</returns>
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        /// Guarda los cambios
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        //Ejecuta un store procedure y carga una lista de entities al final
        IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters)
            where TEntity : BaseEntity, new();

        /// <summary>
        /// Crea una celda Sql query y retorna un elemento generico 
        /// Puede ser de cuakquier tipo que tenga la propiedad que cruce con el nombre de las columnas 
        /// retornadas por el query o puede ser datos primitivos
        /// El tipo no tiene que ser un Entity 
        /// El resultado de este consulta nunca sera trackeado por un evento del contexto 
        /// Si el objeto retornado es del tipo enity
        /// </summary>
        /// <typeparam name="TElement">El tipo de objeto retornado por la query</typeparam>
        /// <param name="sql">la cadena de la consulta</param>
        /// <param name="parameters">parametros que se aplicaran a la consulta</param>
        /// <returns></returns>
        IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters);

        /// <summary>
        /// Ejecuta un comando DDL/DML desde la base de datos
        /// </summary>
        /// <param name="sql">La cadena del comando</param>
        /// <param name="doNotEnsureTransaction">Falso si la creacion de la transaccion no es segura, Verdadero si la transaccion es segura </param>
        /// <param name="timeout">Tiempo en segundos, si es null indica los valores por defecto del roveedor seran usados </param>
        /// <param name="parameters">Parametros que se aplicaran a las cadena de strings</param>
        /// <returns></returns>
        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false,
            int? timeout = null, params object[] parameters);
    }
}