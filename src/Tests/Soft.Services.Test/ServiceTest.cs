using System.Collections.Generic;
using NUnit.Framework;
using Soft.Core.Plugins;

namespace Soft.Services.Test
{
    [TestFixture]
    public abstract class ServiceTest
    {
        [SetUp]
        public void SetUp()
        {
            InitPlugins();
        }

        private static void InitPlugins()
        {
 
        }
    }
}