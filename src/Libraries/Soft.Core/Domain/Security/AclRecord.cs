using Soft.Core.Domain.Customers;

namespace Soft.Core.Domain.Security
{
    /// <summary>
    /// Representa un registoro de ACL
    /// </summary>
    public class AclRecord : BaseEntity
    {
        public int EntityId { get; set; }
        public string EntityName { get; set; }
        public int CustomerRoleId { get; set; }
        public virtual CustomerRole CustomerRole { get; set; }
    }
}