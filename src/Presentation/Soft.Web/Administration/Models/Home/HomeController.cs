using Soft.Web.Framework.Mvc;

namespace Soft.Admin.Models.Home
{
    public partial class DashboardModel : BaseSoftModel
    {
        public bool IsLoggedInAsVendor { get; set; }
    }
}