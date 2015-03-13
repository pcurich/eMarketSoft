using NUnit.Framework;
using Soft.Core.Data;

namespace Soft.Core.Test.Data
{
    [TestFixture]
    public class DataSettingsManagerTests
    {
        [Test]
        public void Can_Read_Directory()
        {
            var manager = new DataSettingsManager();
            manager.LoadSettings();
        }
    }
}