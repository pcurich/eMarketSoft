using System;
using System.Runtime.Serialization;

namespace Soft.Core
{
    /// <summary>
    /// Representa errores que ocurren durante la ejecucion de la aplicacion
    /// </summary>
    [Serializable]
    public class SoftException : Exception
    {
        /// <summary>
        /// Nueva instancia
        /// </summary>
        public SoftException()
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de una excepcion con un mensaje especifico
        /// </summary>
        /// <param name="message">mensaje que describe el error</param>
        public SoftException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de una excepcion con un mensaje especifico
        /// </summary>
        /// <param name="messageFormat">Formato de la excepcion</param>
        /// <param name="args">Argumentos de la excepcion</param>
        public SoftException(string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args))
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de una excepcion con un mensaje serializado
        /// </summary>
        /// <param name="info">La informacion serializada que tiene info hacerca de la excepcion que fue lanzada</param>
        /// <param name="context">Contiene informacion contextual hacerca de la fuente o destino </param>
        public SoftException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de una excepcion con un mensaje especifico referenciado a una inner excepcion
        /// </summary>
        /// <param name="message">Razones que explican el porque de la excepcion</param>
        /// <param name="innerException">Causa de la excepcion</param>
        public SoftException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}