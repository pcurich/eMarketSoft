using Soft.Web.Framework.Mvc;

namespace Soft.Admin.Models.Common
{
    public class SystemWarningModel : BaseSoftModel
    {
        public SystemWarningLevel Level { get; set; }
        public string Text { get; set; }
    }

    public enum SystemWarningLevel
    {
        Pass,
        Warning,
        Fail
    }
}