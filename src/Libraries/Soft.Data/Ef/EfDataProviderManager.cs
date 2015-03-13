using System;
using Soft.Core;
using Soft.Core.Data;
using Soft.Data.Sql;

namespace Soft.Data.Ef
{
    public class EfDataProviderManager : BaseDataProviderManager
    {
        public EfDataProviderManager(DataSettings settings) : base(settings)
        {
        }

        public override IDataProvider LoadDataProvider()
        {
            var providerName = Settings.DataProvider;
            if (String.IsNullOrWhiteSpace(providerName))
                throw new SoftException("Data Settings no contiene un providerName");

            switch (providerName.ToLowerInvariant())
            {
                case "sqlserver":
                    return new SqlServerDataProvider();
                case "sqlce":
                    return new SqlCeDataProvider();
                default:
                    throw new SoftException(string.Format("No soporta dataprovider nombre: {0}", providerName));
            }
        }
    }
}