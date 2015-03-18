//Contributor:  Nicholas Mayne

using System;
using Soft.Core;
using Soft.Core.Domain.Customers;
using Soft.Core.Domain.Localization;
using Soft.Services.Common;
using Soft.Services.Customers;
using Soft.Services.Localization;
using Soft.Services.Logging;
using Soft.Services.Messages;
using Soft.Services.Orders;

namespace Soft.Services.Authentication.External
{
    /// <summary>
    /// Autenticacion externa
    /// </summary>
    public partial class ExternalAuthorizer : IExternalAuthorizer
    {
        #region campos

        private readonly IAuthenticationService _authenticationService;
        private readonly IOpenAuthenticationService _openAuthenticationService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly CustomerSettings _customerSettings;
        private readonly ExternalAuthenticationSettings _externalAuthenticationSettings;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly LocalizationSettings _localizationSettings;
       
        #endregion

        #region Ctr


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="authenticationService">Servicio de Autenticacion</param>
        /// <param name="openAuthenticationService">Servicio para abrir una autenticacion</param>
        /// <param name="genericAttributeService">Servicio de atributos genericos</param>
        /// <param name="customerRegistrationService">Servicio de personalizacion de registro</param>
        /// <param name="customerActivityService">Servicio de personalizacion de actividades</param>
        /// <param name="localizationService">Servicio de Localizacion</param>
        /// <param name="workContext">Contexto de trabajo</param>
        /// <param name="customerSettings">Configuracion de clientes</param>
        /// <param name="externalAuthenticationSettings">Configuracion de autenticacion externa</param>
        /// <param name="shoppingCartService">Servicio de carrito de compras</param>
        /// <param name="workflowMessageService">Servicio de mensajes de flujo de trabajo</param>
        /// <param name="localizationSettings">Configuracion de Localizacion</param>
        public ExternalAuthorizer(IAuthenticationService authenticationService,
            IOpenAuthenticationService openAuthenticationService,
            IGenericAttributeService genericAttributeService,
            ICustomerRegistrationService customerRegistrationService, 
            ICustomerActivityService customerActivityService, ILocalizationService localizationService,
            IWorkContext workContext, CustomerSettings customerSettings,
            ExternalAuthenticationSettings externalAuthenticationSettings,
            IShoppingCartService shoppingCartService,
            IWorkflowMessageService workflowMessageService, LocalizationSettings localizationSettings)
        {
            _authenticationService = authenticationService;
            _openAuthenticationService = openAuthenticationService;
            _genericAttributeService = genericAttributeService;
            _customerRegistrationService = customerRegistrationService;
            _customerActivityService = customerActivityService;
            _localizationService = localizationService;
            _workContext = workContext;
            _customerSettings = customerSettings;
            _externalAuthenticationSettings = externalAuthenticationSettings;
            _shoppingCartService = shoppingCartService;
            _workflowMessageService = workflowMessageService;
            _localizationSettings = localizationSettings;
        }
        
        #endregion

        #region Util

        /// <summary>
        /// Verifica si un usuario no esta desactivado y que este desabilitada 
        /// el registro automatico de la configuracion de la cuenta externa
        /// </summary>
        /// <returns>Si es <c>true</c> activa el registro</returns>
        private bool RegistrationIsEnabled()
        {
            return _customerSettings.UserRegistrationType != UserRegistrationType.Disabled && !_externalAuthenticationSettings.AutoRegisterEnabled;
        }

        /// <summary>
        /// Verifica si un usuario no esta desactivado y que este habilitado
        /// el registro automatico de la configuracion de la cuenta externa
        /// </summary>
        /// <returns>Si es <c>true</c> activa el auto registro</returns>
        private bool AutoRegistrationIsEnabled()
        {
            return _customerSettings.UserRegistrationType != UserRegistrationType.Disabled && _externalAuthenticationSettings.AutoRegisterEnabled;
        }

        /// <summary>
        /// La cuenta no existe y el usuario no esta logeado
        /// </summary>
        /// <param name="userFound">Usuario encontrado</param>
        /// <param name="userLoggedIn">Usuario logeado</param>
        /// <returns>Si es <c>true</c> el usuario activo y el usuario logeado son nulos</returns>
        private static bool AccountDoesNotExistAndUserIsNotLoggedOn(BaseEntity userFound, BaseEntity userLoggedIn)
        {
            return userFound == null && userLoggedIn == null;
        }

        /// <summary>
        /// La cuenta esta asignada a una cuenta logeada
        /// </summary>
        /// <param name="userFound">Usuario encontrado</param>
        /// <param name="userLoggedIn">Usuario logeado</param>
        /// <returns>Si es <c>true</c> el usuario encontrado es el mismo que el usuario logeado</returns>
        private static bool AccountIsAssignedToLoggedOnAccount(BaseEntity userFound, BaseEntity userLoggedIn)
        {
            return userFound.Id.Equals(userLoggedIn.Id);
        }

        /// <summary>
        /// La cuenta existe
        /// </summary>
        /// <param name="userFound">Usuario encontrado</param>
        /// <param name="userLoggedIn">Usuario logeado</param>
        /// <returns>Si es <c>true</c> el usuario encontrado y el usuario logeado no son nulos</returns>
        private static bool AccountAlreadyExists(BaseEntity userFound, BaseEntity userLoggedIn)
        {
            return userFound != null && userLoggedIn != null;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Autorizar
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual AuthorizationResult Authorize(OpenAuthenticationParameters parameters)
        {
            var userFound = _openAuthenticationService.GetUser(parameters);

            var userLoggedIn = _workContext.CurrentCustomer.IsRegistered() ? _workContext.CurrentCustomer : null;

            if (AccountAlreadyExists(userFound, userLoggedIn))
            {
                if (AccountIsAssignedToLoggedOnAccount(userFound, userLoggedIn))
                {
                    // La persona está tratando de conectarse como a sí mismo.. poco raro
                    return new AuthorizationResult(OpenAuthenticationStatus.Authenticated);
                }

                var result = new AuthorizationResult(OpenAuthenticationStatus.Error);
                result.AddError("La cuenta ya esta asignada");
                return result;
            }
            if (AccountDoesNotExistAndUserIsNotLoggedOn(userFound, userLoggedIn))
            {
                ExternalAuthorizerHelper.StoreParametersForRoundTrip(parameters);

                if (AutoRegistrationIsEnabled())
                {
                    #region Register user

                    var currentCustomer = _workContext.CurrentCustomer;
                    var details = new RegistrationDetails(parameters);
                    var randomPassword = CommonHelper.GenerateRandomDigitCode(20);


                    bool isApproved = _customerSettings.UserRegistrationType == UserRegistrationType.Standard;
                    var registrationRequest = new CustomerRegistrationRequest(currentCustomer, details.EmailAddress,
                        _customerSettings.UsernamesEnabled ? details.UserName : details.EmailAddress, randomPassword, PasswordFormat.Clear, isApproved);
                    var registrationResult = _customerRegistrationService.RegisterCustomer(registrationRequest);
                    if (registrationResult.Success)
                    {
                        //store other parameters (form fields)
                        if (!String.IsNullOrEmpty(details.FirstName))
                            _genericAttributeService.SaveAttribute(currentCustomer, SystemCustomerAttributeNames.FirstName, details.FirstName);
                        if (!String.IsNullOrEmpty(details.LastName))
                            _genericAttributeService.SaveAttribute(currentCustomer, SystemCustomerAttributeNames.LastName, details.LastName);
                    

                        userFound = currentCustomer;
                        _openAuthenticationService.AssociateExternalAccountWithUser(currentCustomer, parameters);
                        ExternalAuthorizerHelper.RemoveParameters();

                        //code below is copied from CustomerController.Register method

                        //authenticate
                        if (isApproved)
                            _authenticationService.SignIn(userFound ?? userLoggedIn, false);

                        //notifications
                        if (_customerSettings.NotifyNewCustomerRegistration)
                            _workflowMessageService.SendCustomerRegisteredNotificationMessage(currentCustomer, _localizationSettings.DefaultAdminLanguageId);

                        switch (_customerSettings.UserRegistrationType)
                        {
                            case UserRegistrationType.EmailValidation:
                                {
                                    //email validation message
                                    _genericAttributeService.SaveAttribute(currentCustomer, SystemCustomerAttributeNames.AccountActivationToken, Guid.NewGuid().ToString());
                                    _workflowMessageService.SendCustomerEmailValidationMessage(currentCustomer, _workContext.WorkingLanguage.Id);

                                    //result
                                    return new AuthorizationResult(OpenAuthenticationStatus.AutoRegisteredEmailValidation);
                                }
                            case UserRegistrationType.AdminApproval:
                                {
                                    //result
                                    return new AuthorizationResult(OpenAuthenticationStatus.AutoRegisteredAdminApproval);
                                }
                            case UserRegistrationType.Standard:
                                {
                                    //send customer welcome message
                                    _workflowMessageService.SendCustomerWelcomeMessage(currentCustomer, _workContext.WorkingLanguage.Id);

                                    //result
                                    return new AuthorizationResult(OpenAuthenticationStatus.AutoRegisteredStandard);
                                }
                            default:
                                break;
                        }
                    }
                    else
                    {
                        ExternalAuthorizerHelper.RemoveParameters();

                        var result = new AuthorizationResult(OpenAuthenticationStatus.Error);
                        foreach (var error in registrationResult.Errors)
                            result.AddError(string.Format(error));
                        return result;
                    }

                    #endregion
                }
                else if (RegistrationIsEnabled())
                {
                    return new AuthorizationResult(OpenAuthenticationStatus.AssociateOnLogon);
                }
                else
                {
                    ExternalAuthorizerHelper.RemoveParameters();

                    var result = new AuthorizationResult(OpenAuthenticationStatus.Error);
                    result.AddError("Registration is disabled");
                    return result;
                }
            }
            if (userFound == null)
            {
                _openAuthenticationService.AssociateExternalAccountWithUser(userLoggedIn, parameters);
            }

            //migrate shopping cart
            _shoppingCartService.MigrateShoppingCart(_workContext.CurrentCustomer, userFound ?? userLoggedIn, true);
            //authenticate
            _authenticationService.SignIn(userFound ?? userLoggedIn, false);
            //activity log
            _customerActivityService.InsertActivity("PublicStore.Login", _localizationService.GetResource("ActivityLog.PublicStore.Login"), 
                userFound ?? userLoggedIn);
            
            return new AuthorizationResult(OpenAuthenticationStatus.Authenticated);
        }

        #endregion
    }
}