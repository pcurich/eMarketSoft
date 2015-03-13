using System;
using Soft.Core.Domain.Customers;

namespace Soft.Core.Domain.Forums
{
    /// <summary>
    /// Representa un mensaje privado
    /// </summary>
    public partial class PrivateMessage : BaseEntity
    {
        /// <summary>
        /// Identifiador de la tienda
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Identificador de quien envio el mensaje
        /// </summary>
        public int FromCustomerId { get; set; }

        /// <summary>
        /// Identificador de hacia quien va el mensaje
        /// </summary>
        public int ToCustomerId { get; set; }

        /// <summary>
        /// Asunto
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Texto
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Si a sido leido
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// Si el mensaje a sido borrado por el autor
        /// </summary>
        public bool IsDeletedByAuthor { get; set; }

        /// <summary>
        /// Si el mensaje a sido borrado por el que recibio el mensaje
        /// </summary>
        public bool IsDeletedByRecipient { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Cliente que envia
        /// </summary>
        public virtual Customer FromCustomer { get; set; }

        /// <summary>
        /// Cliente que recive
        /// </summary>
        public virtual Customer ToCustomer { get; set; }
    }
}