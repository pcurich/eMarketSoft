﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using Soft.Core;
using Soft.Data.Mapping;

namespace Soft.Data.Entities
{
    public class SoftContext : DbContext, IDbContext
    {
        #region Ctr

        public SoftContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            //Configuration.LazyLoadingEnabled = false; //http://sebys.com.ar/2011/06/01/entity-framework-4-1-cf-y-lazy-load/
            //Configuration.AutoDetectChangesEnabled = true;
            //Configuration.ProxyCreationEnabled = false;
            //Configuration.ValidateOnSaveEnabled = true;
            
            //((IObjectContextAdapter) this).ObjectContext.ContextOptions.LazyLoadingEnabled = true;
        }

        #endregion

        #region Util

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Se cargan dinamicamente todas las configuracones
            //System.Type configType = typeof(LanguageMap);   //Cualquiera de las clases ..Map
            //var typesToRegister = Assembly.GetAssembly(configType).GetTypes()
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null &&
                               type.BaseType.IsGenericType &&
                               type.BaseType.GetGenericTypeDefinition() == typeof (SoftEntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            //modelBuilder.Configurations.Add(new LanguageMap());

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Adjunta un entity al contexto o retorna  un entity adjuntado si es que lo esta
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>Entity adjunto</returns>
        protected virtual TEntity AttachEntityToContext<TEntity>(TEntity entity) where TEntity : BaseEntity, new()
        {
            //Truco para no utilizar Store Procedure
            //de lo contrario , las propiedades de navegación de las entidades cargadas no se cargaran
            //hasta que una entidad está unida al contexto
            var alreadyAttached = Set<TEntity>().Local.FirstOrDefault(x => x.Id == entity.Id);
            if (alreadyAttached == null)
            {
                //adjunta un new entity
                Set<TEntity>().Attach(entity);
                return entity;
            }

            //entity ya esta cargado
            return alreadyAttached;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Crea un scrip de base de datos
        /// </summary>
        /// <returns>Sql para crear la BD</returns>
        public string CreateDatabaseScript()
        {
            return (this as IObjectContextAdapter).ObjectContext.CreateDatabaseScript();
        }

        /// <summary>
        /// Obtienen el DbSet
        /// </summary>
        /// <typeparam name="TEntity">Tipo del Entity</typeparam>
        /// <returns>
        /// DbSet
        /// </returns>
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        /// <summary>
        /// Ejecuta un store procedure y carga una lista de entities al final
        /// </summary>
        /// <typeparam name="TEntity">Tipo del Entity</typeparam>
        /// <param name="commandText">El testo del comando.</param>
        /// <param name="parameters">Los parametros.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">No soporta el tipo de parametro</exception>
        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters)
            where TEntity : BaseEntity, new()
        {
            //agrega parametros al comando
            if (parameters != null && parameters.Length > 0)
            {
                for (var i = 0; i <= parameters.Length - 1; i++)
                {
                    var p = parameters[i] as DbParameter;
                    if (p == null)
                        throw new Exception("No soporta el tipo de parametro");

                    commandText += i == 0 ? " " : ", ";
                    commandText += "@" + p.ParameterName;
                    if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                    {
                        commandText += " output";
                    }
                }
            }

            var result = Database.SqlQuery<TEntity>(commandText, parameters).ToList();
            //performance hack applied as described here - http://www.nopcommerce.com/boards/t/25483/fix-very-important-speed-improvement.aspx
            var acd = Configuration.AutoDetectChangesEnabled;
            try
            {
                Configuration.AutoDetectChangesEnabled = false;

                for (var i = 0; i < result.Count; i++)
                    result[i] = AttachEntityToContext(result[i]);
            }
            finally
            {
                Configuration.AutoDetectChangesEnabled = acd;
            }

            return result;
        }

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
        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return Database.SqlQuery<TElement>(sql, parameters);
        }

        /// <summary>
        /// Ejecuta un comando DDL/DML desde la base de datos
        /// </summary>
        /// <param name="sql">La cadena del comando</param>
        /// <param name="doNotEnsureTransaction">Falso si la creacion de la transaccion no es segura, Verdadero si la transaccion es segura</param>
        /// <param name="timeout">Tiempo en segundos, si es null indica los valores por defecto del roveedor seran usados</param>
        /// <param name="parameters">Parametros que se aplicaran a las cadena de strings</param>
        /// <returns></returns>
        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null,
            params object[] parameters)
        {
            int? previousTimeout = null;
            if (timeout.HasValue)
            {
                //timeout previo de la tienda
                previousTimeout = ((IObjectContextAdapter) this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter) this).ObjectContext.CommandTimeout = timeout;
            }

            var transactionalBehavior = doNotEnsureTransaction
                ? TransactionalBehavior.DoNotEnsureTransaction
                : TransactionalBehavior.EnsureTransaction;
            var result = Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);

            if (timeout.HasValue)
            {
                //Set previous timeout back
                ((IObjectContextAdapter) this).ObjectContext.CommandTimeout = previousTimeout;
            }

            return result;
        }

        #endregion
    }
}