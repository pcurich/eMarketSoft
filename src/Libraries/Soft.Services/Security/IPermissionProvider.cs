using System.Collections.Generic;
using Soft.Core.Domain.Security;

namespace Soft.Services.Security
{
    /// <summary>
    /// Proveedor de permisos
    /// </summary>
    public interface IPermissionProvider
    {
        /// <summary>
        /// Optiene los permisos
        /// </summary>
        /// <returns></returns>
        IEnumerable<PermissionRecord> GetPermissions();
        /// <summary>
        /// Permisos x default
        /// </summary>
        /// <returns></returns>
        IEnumerable<DefaultPermissionRecord> GetDefaultPermissions();
    }
}