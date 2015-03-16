using Soft.Core.Domain.Customers;

namespace Soft.Services.Authentication
{
    /// <summary>
    ///     Interfaz para la autentificacion del servicio
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        ///     Ingreso
        /// </summary>
        /// <param name="customer">Cliente</param>
        /// <param name="createPersistentCookie">Persistencia de cockies</param>
        void SignIn(Customer customer, bool createPersistentCookie);

        /// <summary>
        ///     Salida
        /// </summary>
        void SignOut();

        /// <summary>
        ///     Cliente autentificado
        /// </summary>
        /// <returns></returns>
        Customer GetAuthenticatedCustomer();
    }
}