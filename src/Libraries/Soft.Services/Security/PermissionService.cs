using System;
using System.Collections.Generic;
using Soft.Core;
using Soft.Core.Caching;
using Soft.Core.Data;
using Soft.Core.Domain.Customers;
using Soft.Core.Domain.Security;
using Soft.Services.Customers;
using Soft.Services.Localization;
using ILanguageService = Soft.Services.Localization.ILanguageService;

namespace Soft.Services.Security
{
    public partial class PermissionService : IPermissionService
    {
        #region Constantes

        /// <summary>
        /// Llave para el cache
        /// </summary>
        /// <remarks>
        /// {0} : customer role ID
        /// {1} : permission system name
        /// </remarks>
        private const string PERMISSIONS_ALLOWED_KEY = "Soft.permission.allowed-{0}-{1}";

        /// <summary>
        /// Llave para borrar el cache
        /// </summary>
        private const string PERMISSIONS_PATTERN_KEY = "Soft.permission.";

        #endregion

        #region Campos

        private readonly IRepository<PermissionRecord> _permissionPecordRepository;
        private readonly ICustomerService _customerService;
        private readonly IWorkContext _workContext;
        private readonly ILocalizationService _localizationService;
        private readonly ILanguageService _languageService;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctr

        public PermissionService
            (IRepository<PermissionRecord> permissionPecordRepository,
                ICustomerService customerService,
                IWorkContext workContext,
                ILocalizationService localizationService,
                ILanguageService languageService,
                ICacheManager cacheManager)
        {
            _customerService = customerService;
            _permissionPecordRepository = permissionPecordRepository;
            _workContext = workContext;
            _localizationService = localizationService;
            _languageService = languageService;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Util

        /// <summary>
        /// Permiso de autorizacion
        /// </summary>
        /// <param name="permissionRecordSystemName">Nombre del permiso del sistema</param>
        /// <param name="customerRole">Rol del consumidor</param>
        /// <returns>true - autorizado; otros, false</returns>
        protected virtual bool Authorize(string permissionRecordSystemName, CustomerRole customerRole)
        {
            if (String.IsNullOrEmpty(permissionRecordSystemName))
                return false;

            var key = string.Format(PERMISSIONS_ALLOWED_KEY, customerRole.Id, permissionRecordSystemName);
            return _cacheManager.Get(key, () =>
            {
                foreach (var permission1 in customerRole.PermissionRecords)
                    if (permission1.SystemName.Equals(permissionRecordSystemName, StringComparison.InvariantCultureIgnoreCase))
                        return true;

                return false;
            });
        }

        #endregion

        #region Metodos

        public void DeletePermissionRecord(PermissionRecord permission)
        {
            throw new System.NotImplementedException();
        }

        public PermissionRecord GetPermissionRecordById(int permissionId)
        {
            throw new System.NotImplementedException();
        }

        public PermissionRecord GetPermissionRecordBySystemName(string systemName)
        {
            throw new System.NotImplementedException();
        }

        public IList<PermissionRecord> GetAllPermissionRecords()
        {
            throw new System.NotImplementedException();
        }

        public void InsertPermissionRecord(PermissionRecord permission)
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePermissionRecord(PermissionRecord permission)
        {
            throw new System.NotImplementedException();
        }

        public void InstallPermissions(IPermissionProvider permissionProvider)
        {
            throw new System.NotImplementedException();
        }

        public void UninstallPermissions(IPermissionProvider permissionProvider)
        {
            throw new System.NotImplementedException();
        }

        public bool Authorize(PermissionRecord permission)
        {
            throw new System.NotImplementedException();
        }

        public bool Authorize(PermissionRecord permission, Customer customer)
        {
            throw new System.NotImplementedException();
        }

        public bool Authorize(string permissionRecordSystemName)
        {
            throw new System.NotImplementedException();
        }

        public bool Authorize(string permissionRecordSystemName, Customer customer)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}