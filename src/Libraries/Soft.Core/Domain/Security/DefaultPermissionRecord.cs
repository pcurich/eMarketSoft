using System.Collections.Generic;

namespace Soft.Core.Domain.Security
{
    /// <summary>
    /// Representa un registro de ingresos por default
    /// </summary>
    public class DefaultPermissionRecord
    {
        public DefaultPermissionRecord()
        {
            PermissionRecords = new List<PermissionRecord>();
        }

        public string CustomerRoleSystemName { get; set; }
        public IEnumerable<PermissionRecord> PermissionRecords { get; set; }
    }
}