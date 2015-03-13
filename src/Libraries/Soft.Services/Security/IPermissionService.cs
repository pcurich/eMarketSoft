using System.Collections.Generic;
using Soft.Core.Domain.Customers;
using Soft.Core.Domain.Security;

namespace Soft.Services.Security
{
    /// <summary>
    /// Interfaz de servicio de permisos
    /// </summary>
    public partial interface IPermissionService
    {
        void DeletePermissionRecord(PermissionRecord permission);
        PermissionRecord GetPermissionRecordById(int permissionId);
        PermissionRecord GetPermissionRecordBySystemName(string systemName);
        IList<PermissionRecord> GetAllPermissionRecords();
        void InsertPermissionRecord(PermissionRecord permission);
        void UpdatePermissionRecord(PermissionRecord permission);
        void InstallPermissions(IPermissionProvider permissionProvider);
        void UninstallPermissions(IPermissionProvider permissionProvider);
        bool Authorize(PermissionRecord permission);
        bool Authorize(PermissionRecord permission, Customer customer);
        bool Authorize(string permissionRecordSystemName);
        bool Authorize(string permissionRecordSystemName, Customer customer);
    }
}