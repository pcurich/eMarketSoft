using System;

namespace Soft.Core.Domain.Messages
{
    /// <summary>
    /// Representa una cuenta de email
    /// </summary>
    public partial class EmailAccount : BaseEntity
    {
        /// <summary>
        /// Direccion de correo
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Nombre para mostrar
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Host del correo
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Puerto del servidor de correos
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// User name del correo
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// password del correo
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Si el SmtpClient usa para encriptar la conexion 
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// Si las credenciales por default del sistema se envian como request
        /// </summary>
        public bool UseDefaultCredentials { get; set; }

        /// <summary>
        /// Nombre amigable
        /// </summary>
        public string FriendlyName
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(DisplayName))
                    return Email + " (" + DisplayName + ")";
                return Email;
            }
        }
    }
}