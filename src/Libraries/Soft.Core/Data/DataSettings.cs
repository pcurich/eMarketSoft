using System;
using System.Collections.Generic;

namespace Soft.Core.Data
{
    public partial class DataSettings
    {
        public string DataProvider { get; set; }
        public string DataConnectionString { get; set; }
        public IDictionary<string, string> RawDataSettings { get; private set; }

        public DataSettings()
        {
            RawDataSettings = new Dictionary<string, string>();
        }

        public bool IsValid()
        {
            return !String.IsNullOrEmpty(DataProvider) && !String.IsNullOrEmpty(DataConnectionString);
        }
    }
}