using System;
using Soft.Core.Configuration;
using Soft.Core.Infrastructure.DependencyManagement;

namespace Soft.Core.Infrastructure
{
    /// <summary>
    /// Las clases que implementen esta interfaz sera como un portal para 
    /// el acceso de varios servicios compuestos.
    /// Editar funcionalidades, modulos e implementacion para acceder a las
    /// funcionalidades a traves de esta interfaz
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// Gestion de contenedores
        /// </summary>
        ContainerManager ContainerManager { get; }

        /// <summary>
        /// Inicializa los componentes y plugins en el entorno de soft
        /// </summary>
        /// <param name="config"></param>
        void Initialize(SoftConfig config);

        /// <summary>
        /// Resuelve dependencias
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>() where T : class;

        /// <summary>
        /// Resuelve dependencias
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object Resolve(Type type);

        /// <summary>
        /// Resuelve dependencias
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T[] ResolveAll<T>();
    }
}