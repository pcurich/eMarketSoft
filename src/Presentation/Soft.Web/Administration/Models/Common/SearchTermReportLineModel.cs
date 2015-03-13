using Soft.Web.Framework;
using Soft.Web.Framework.Mvc;

namespace Soft.Admin.Models.Common
{
    public partial class SearchTermReportLineModel : BaseSoftModel
    {
        [SoftResourceDisplayName("Admin.SearchTermReport.Keyword")]
        public string Keyword { get; set; }

        [SoftResourceDisplayName("Admin.SearchTermReport.Count")]
        public int Count { get; set; }
    }
}