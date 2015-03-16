using System;
using System.Collections.Generic;
using System.Reflection;

namespace Soft.Core.Infrastructure
{
    /// <summary>
    /// Las clases que implementen esta interfaz van a proveer
    /// informacion acerca de los tipos de varios servicios dentro del
    /// proyecto
    /// </summary>
    public interface ITypeFinder
    {
        /// <summary>
        /// Lista de ensamblados relacionados a la aplicacion actual, usualmente librerias dll
        /// </summary>
        IList<Assembly> GetAssemblies();

        /// <summary>
        /// Encuentra clases de tipo Type
        /// </summary>
        /// <param name="assignTypeFrom">Tipo de clase a buscar.</param>
        /// <param name="onlyConcreteClasses">Si es <c>true</c> [solo clases concretas].</param>
        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true);

        /// <summary>
        /// Encuentra clases de tipo Type
        /// </summary>
        /// <param name="assignTypeFrom">Tipo de clase a buscar.</param>
        /// <param name="assemblies">Lista de ensambaldos de donde debe buscar</param>
        /// <param name="onlyConcreteClasses">Si es <c>true</c> [solo clases concretas].</param>
        /// <returns></returns>
        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies,bool onlyConcreteClasses = true);

        /// <summary>
        /// Encuentra clases de tipo T
        /// </summary>
        /// <typeparam name="T">Tipo de clase a buscar</typeparam>
        /// <param name="onlyConcreteClasses">Si es <c>true</c> [solo clases concretas].</param>
        /// <returns></returns>
        IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true);

        /// <summary>
        /// Encuentra clases Dde tipo T
        /// </summary>
        /// <typeparam name="T">Tipo de clase a buscar</typeparam>
        /// <param name="assemblies">Lista de ensambaldos de donde debe buscar</param>
        /// <param name="onlyConcreteClasses">Si es <c>true</c> [solo clases concretas].</param>
        /// <returns></returns>
        IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);
    }
}