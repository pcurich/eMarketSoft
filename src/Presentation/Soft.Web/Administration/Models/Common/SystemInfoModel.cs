using System;
using System.Collections.Generic;
using Soft.Web.Framework;
using Soft.Web.Framework.Mvc;

namespace Soft.Admin.Models.Common
{
    public partial class SystemInfoModel : BaseSoftModel
    {
        public SystemInfoModel()
        {
            ServerVariables = new List<ServerVariableModel>();
            LoadedAssemblies = new List<LoadedAssembly>();
        }

        [SoftResourceDisplayName("Admin.System.SystemInfo.ASPNETInfo")]
        public string AspNetInfo { get; set; }

        [SoftResourceDisplayName("Admin.System.SystemInfo.IsFullTrust")]
        public string IsFullTrust { get; set; }

        [SoftResourceDisplayName("Admin.System.SystemInfo.SoftVersion")]
        public string SoftVersion { get; set; }

        [SoftResourceDisplayName("Admin.System.SystemInfo.OperatingSystem")]
        public string OperatingSystem { get; set; }

        [SoftResourceDisplayName("Admin.System.SystemInfo.ServerLocalTime")]
        public DateTime ServerLocalTime { get; set; }

        [SoftResourceDisplayName("Admin.System.SystemInfo.ServerTimeZone")]
        public string ServerTimeZone { get; set; }

        [SoftResourceDisplayName("Admin.System.SystemInfo.UTCTime")]
        public DateTime UtcTime { get; set; }

        [SoftResourceDisplayName("Admin.System.SystemInfo.HTTPHOST")]
        public string HttpHost { get; set; }

        [SoftResourceDisplayName("Admin.System.SystemInfo.ServerVariables")]
        public IList<ServerVariableModel> ServerVariables { get; set; }

        [SoftResourceDisplayName("Admin.System.SystemInfo.LoadedAssemblies")]
        public IList<LoadedAssembly> LoadedAssemblies { get; set; }

        public partial class ServerVariableModel : BaseSoftModel
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        public partial class LoadedAssembly : BaseSoftModel
        {
            public string FullName { get; set; }
            public string Location { get; set; }
        }
    }
}