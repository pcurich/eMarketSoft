using System;
using System.Data.Entity.Core.Objects;
using Soft.Core;

namespace Soft.Data.Extensions
{

    public static class SoftExtensions
    {
        /// <summary>
        /// Obtiene un entity tipo unproxied
        /// </summary>
        /// <remarks>
        /// Si el contexto del entity framework tiene abilitado el proxy
        /// El tiempo de ejecucion creara una instancia del proxy en los entities
        /// x ejemplo una clase creada dinamicamente que herede de una clase entity 
        /// y su sobreescritura tiene una propiedad virtual para insertar codigo especifico
        /// util como x ejemplo para rastrear cambios y carga lazy
        /// </remarks>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Type GetUnproxiedEntityType(this BaseEntity entity)
        {
            var userType = ObjectContext.GetObjectType(entity.GetType());
            return userType;
        }
    }
}