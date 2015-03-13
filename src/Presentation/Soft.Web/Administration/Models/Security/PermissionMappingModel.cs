using System.Collections.Generic;
using Soft.Admin.Models.Customers;
using Soft.Web.Framework.Mvc;

namespace Soft.Admin.Models.Security
{
    public class PermissionMappingModel:BaseSoftModel
    {
        public IList<PermissionRecordModel> AvailablePermissions { get; set; }
        public IList<CustomerRoleModel> AvailableCustomerRoles { get; set; }

        //[permission system name] / [customer role id] / [allowed]
        public IDictionary<string, IDictionary<int, bool>> Allowed { get; set; }

        public PermissionMappingModel()
        {
            AvailablePermissions = new List<PermissionRecordModel>();
            AvailableCustomerRoles = new List<CustomerRoleModel>();
            Allowed = new Dictionary<string, IDictionary<int, bool>>();
        }
        
    }
}