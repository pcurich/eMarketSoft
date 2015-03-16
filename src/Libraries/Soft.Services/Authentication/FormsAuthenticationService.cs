using System;
using System.Web;
using System.Web.Security;
using Soft.Core.Domain.Customers;
using Soft.Services.Customers;

namespace Soft.Services.Authentication
{
    /// <summary>
    ///     Authentication service
    /// </summary>
    public class FormsAuthenticationService : IAuthenticationService
    {
        private readonly ICustomerService _customerService;
        private readonly CustomerSettings _customerSettings;
        private readonly TimeSpan _expirationTimeSpan;
        private readonly HttpContextBase _httpContext;

        private Customer _cachedCustomer;

        /// <summary>
        ///     Ctr
        /// </summary>
        /// <param name="httpContext">HTTP contexto</param>
        /// <param name="customerService">Servicio de cliente</param>
        /// <param name="customerSettings">Configuracion del cliente</param>
        public FormsAuthenticationService(HttpContextBase httpContext, ICustomerService customerService,
            CustomerSettings customerSettings)
        {
            _httpContext = httpContext;
            _customerService = customerService;
            _customerSettings = customerSettings;
            _expirationTimeSpan = FormsAuthentication.Timeout;
        }

        /// <summary>
        ///     Ingreso
        /// </summary>
        /// <param name="customer">Cliente</param>
        /// <param name="createPersistentCookie">Persistencia de cockies</param>
        public virtual void SignIn(Customer customer, bool createPersistentCookie)
        {
            DateTime now = DateTime.UtcNow.ToLocalTime();

            var ticket = new FormsAuthenticationTicket(
                1 /*version*/,
                _customerSettings.UsernamesEnabled ? customer.Username : customer.Email,
                now,
                now.Add(_expirationTimeSpan),
                createPersistentCookie,
                _customerSettings.UsernamesEnabled ? customer.Username : customer.Email,
                FormsAuthentication.FormsCookiePath);

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true
            };

            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }

            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;

            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            _httpContext.Response.Cookies.Add(cookie);
            _cachedCustomer = customer;
        }

        /// <summary>
        ///     Salida
        /// </summary>
        public virtual void SignOut()
        {
            _cachedCustomer = null;
            FormsAuthentication.SignOut();
        }

        /// <summary>
        ///     Cliente autentificado
        /// </summary>
        /// <returns></returns>
        public virtual Customer GetAuthenticatedCustomer()
        {
            if (_cachedCustomer != null)
                return _cachedCustomer;

            if (_httpContext == null ||
                _httpContext.Request == null ||
                !_httpContext.Request.IsAuthenticated ||
                !(_httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }

            var formsIdentity = (FormsIdentity) _httpContext.User.Identity;
            Customer customer = GetAuthenticatedCustomerFromTicket(formsIdentity.Ticket);
            if (customer != null && customer.Active && !customer.Deleted && customer.IsRegistered())
                _cachedCustomer = customer;
            return _cachedCustomer;
        }

        /// <summary>
        ///     Obtiene al cliente autentificado desde el ticket creado
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public virtual Customer GetAuthenticatedCustomerFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            string usernameOrEmail = ticket.UserData;

            if (String.IsNullOrWhiteSpace(usernameOrEmail))
                return null;

            Customer customer = _customerSettings.UsernamesEnabled
                ? _customerService.GetCustomerByUsername(usernameOrEmail)
                : _customerService.GetCustomerByEmail(usernameOrEmail);
            return customer;
        }
    }
}