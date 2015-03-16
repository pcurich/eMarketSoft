using System;
using Autofac;
using Soft.Core.Data;
using Soft.Core.Infrastructure.DependencyManagement;
using Soft.Data.Entities;

namespace Soft.Web.Framework.Mvc
{
    /// <summary>
    /// Extension para el registro de Independencias
    /// </summary>
    public static class DependencyRegistrarExtensions
    {
        /// <summary>
        /// Registra los DataContext personalizados para los plugin
        /// </summary>
        /// <typeparam name="T">Implementacion de IDbContext</typeparam>
        /// <param name="dependencyRegistrar">Registro de Independencias</param>
        /// <param name="builder">The builder.</param>
        /// <param name="contextName">Nombre del contexto.</param>
        public static void RegisterPluginDataContext<T>(this IDependencyRegistrar dependencyRegistrar,
            ContainerBuilder builder, string contextName)
             where T : IDbContext
        {
            //Capa de datos
            var dataSettingsManager = new DataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings();

            if (dataProviderSettings != null && dataProviderSettings.IsValid())
            {
                //register named context
                builder.Register(c => (IDbContext)Activator.CreateInstance(typeof(T), new object[] { dataProviderSettings.DataConnectionString }))
                    .Named<IDbContext>(contextName)
                    .InstancePerLifetimeScope();

                builder.Register(c => (T)Activator.CreateInstance(typeof(T), new object[] { dataProviderSettings.DataConnectionString }))
                    .InstancePerLifetimeScope();
            }
            else
            {
                //register named context
                builder.Register(c => (T)Activator.CreateInstance(typeof(T), new object[] { c.Resolve<DataSettings>().DataConnectionString }))
                    .Named<IDbContext>(contextName)
                    .InstancePerLifetimeScope();

                builder.Register(c => (T)Activator.CreateInstance(typeof(T), new object[] { c.Resolve<DataSettings>().DataConnectionString }))
                    .InstancePerLifetimeScope();
            }
        } 
    }
}