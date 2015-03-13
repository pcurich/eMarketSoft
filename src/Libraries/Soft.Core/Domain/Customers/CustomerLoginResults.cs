namespace Soft.Core.Domain.Customers
{
    /// <summary>
    /// Representa el resultado de un login 
    /// </summary>
    public enum CustomerLoginResults
    {
        /// <summary>
        /// Login successful
        /// </summary>
        Successful = 1,

        /// <summary>
        /// Cliente no existe  (email or username)
        /// </summary>
        CustomerNotExist = 2,

        /// <summary>
        /// Clave incorrecta
        /// </summary>
        WrongPassword = 3,

        /// <summary>
        /// Cuenta no activa
        /// </summary>
        NotActive = 4,

        /// <summary>
        /// Usuario borrado
        /// </summary>
        Deleted = 5,

        /// <summary>
        /// Usuario  no registrado
        /// </summary>
        NotRegistered = 6,
    }
}