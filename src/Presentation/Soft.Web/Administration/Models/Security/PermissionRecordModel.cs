using Soft.Web.Framework.Mvc;

namespace Soft.Admin.Models.Security
{
    public partial class PermissionRecordModel : BaseSoftModel
    {
        public string Name { get; set; }
        public string SystemName { get; set; }
    }
}