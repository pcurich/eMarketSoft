namespace Soft.Core.Domain.Customers
{
    /// <summary>
    /// Representa el nombre formateado del cliente
    /// </summary>
    public enum CustomerNameFormat
    {
        /// <summary>
        /// Muestra email
        /// </summary>
        ShowEmails = 1,

        /// <summary>
        /// UserName
        /// </summary>
        ShowUsernames = 2,

        /// <summary>
        /// Nombre completo
        /// </summary>
        ShowFullNames = 3,

        /// <summary>
        /// Primer nombre
        /// </summary>
        ShowFirstName = 10
    }
}