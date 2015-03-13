using System;

namespace Soft.Core.Domain.Messages
{
    /// <summary>
    /// Representa una campaña
    /// </summary>
    public partial class Campaign : BaseEntity
    {
        /// <summary>
        /// Nombre de la campaña
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Asunto
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Cuerpo
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Identificador de la tienda con suscriptores a quienes se les enviaran; 0 se enviara a todos
        /// </summary>
        public int StoreId { get; set; }

        public DateTime CreatedOnUtc { get; set; }
    }
}