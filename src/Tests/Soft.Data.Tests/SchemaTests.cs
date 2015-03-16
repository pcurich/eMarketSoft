using System;
using System.Data.Entity;
using NUnit.Framework;
using Soft.Data.Entities;
using Soft.Test;

namespace Soft.Data.Test
{
    [TestFixture]
    public class SchemaTest
    {
        [Test]
        public void Can_generate_schema()
        {
            Database.SetInitializer<SoftContext>(null);
            var ctx = new SoftContext("Test");
            string result = ctx.CreateDatabaseScript();
            result.ShouldNotBeNull();
            Console.Write(result);
        }
    }
}
