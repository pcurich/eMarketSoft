using System.Collections.Generic;
using Soft.Core.Domain.Security;

namespace Soft.Core.Domain.Customers
{
    /// <summary>
    /// Representa un rol de consumidor
    /// </summary>
    public partial class CustomerRole : BaseEntity
    {
        private ICollection<PermissionRecord> _permissionRecords;
        public string Name { get; set; }
        public bool FreeShipping { get; set; }
        public bool TaxExempt { get; set; }
        public bool Active { get; set; }
        public bool IsSystemRole { get; set; }
        public string SystemName { get; set; }

        /// <summary>
        /// Identificador de un producto que se requiere para un rol de consumidor
        /// Un consumidor agrega a este rol de consumidor una vez identificado el producto compado 
        /// </summary>
        public int PurchasedWithProductId { get; set; }

        public virtual ICollection<PermissionRecord> PermissionRecords
        {
            get { return _permissionRecords ?? (_permissionRecords = new List<PermissionRecord>()); }
            protected set { _permissionRecords = value; }
        }
    }
}