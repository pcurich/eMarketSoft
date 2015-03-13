using System.Collections.Generic;
using Soft.Core.Domain.Customers;

namespace Soft.Core.Domain.Security
{
    /// <summary>
    /// Representa un registro de permisos
    /// </summary>
    public class PermissionRecord : BaseEntity
    {
        private ICollection<CustomerRole> _customerRoles;
        public string Name { get; set; }
        public string SystemName { get; set; }
        public string Category { get; set; }

        public virtual ICollection<CustomerRole> CustomerRoles
        {
            get { return _customerRoles ?? (_customerRoles = new List<CustomerRole>()); }
            protected set { _customerRoles = value; }
        }
    }
}