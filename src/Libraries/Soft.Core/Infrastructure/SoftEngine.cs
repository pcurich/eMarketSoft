﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Soft.Core.Configuration;
using Soft.Core.Infrastructure.DependencyManagement;

namespace Soft.Core.Infrastructure
{
    public class SoftEngine : IEngine
    {
        #region Propiedades

        /// <summary>
        ///     Manejador de contenedores
        /// </summary>
        public ContainerManager ContainerManager { get; private set; }

        #endregion

        #region Util

        protected virtual void RunStartupTasks()
        {
            var typeFinder = ContainerManager.Resolve<ITypeFinder>();
            var startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
            var startUpTasks = new List<IStartupTask>();
            foreach (var startUpTaskType in startUpTaskTypes)
                startUpTasks.Add((IStartupTask) Activator.CreateInstance(startUpTaskType));

            //ordenamos
            startUpTasks = startUpTasks
                .AsQueryable()
                .OrderBy(st => st.Order)
                .ToList();

            foreach (var startUpTask in startUpTasks)
                startUpTask.Execute();
        }

        protected virtual void RegisterDependencies(SoftConfig config)
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();

            //Es necesario crear una nueva instancia de ContainerBuilder
            //porque Build() o Update() pueden ser llamados una vez en un ContainerBuilder

            //Dependencias
            var typeFinder = new WebAppTypeFinder(config);
            builder = new ContainerBuilder();
            builder.RegisterInstance(config).As<SoftConfig>().SingleInstance();
            builder.RegisterInstance(this).As<IEngine>().SingleInstance();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();
            builder.Update(container);

            //Registra dependencias provenientes de otros ensamblados
            builder = new ContainerBuilder();
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var drInstances = new List<IDependencyRegistrar>();

            foreach (var drType in drTypes)
                drInstances.Add((IDependencyRegistrar) Activator.CreateInstance(drType));

            //Ordenamos
            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(builder, typeFinder);
            builder.Update(container);

            ContainerManager = new ContainerManager(container);

            //Establece el resolvedor de dependencias
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        #endregion

        #region Metodos

        public void Initialize(SoftConfig config)
        {
            //Registra las dependencias
            RegisterDependencies(config);

            //Comienza las tareas
            if (!config.IgnoreStartupTasks)
                RunStartupTasks();
        }

        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }

        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }

        #endregion
    }
}