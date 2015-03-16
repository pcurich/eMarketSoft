using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using NUnit.Framework;
using Soft.Core;
using Soft.Data.Entities;

namespace Soft.Data.Test
{
    [TestFixture]
    public abstract class PersistenceTest
    {
        protected SoftContext Context;

        [SetUp]
        public void SetUp()
        {
            //TODO fix compilation warning (below)
            #pragma warning disable 0618
            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
            Context = new SoftContext(GetTestDbName());
            Context.Database.Delete();
            Context.Database.Create();
        }

        protected string GetTestDbName()
        {
            var testDbName = "Data Source=" +
                             (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)) +
                             @"\\Soft.Data.Tests.Db.sdf;Persist Security Info=False";
            return testDbName;
        }

        protected T SaveAndLoadEntity<T>(T entity, bool disposeContext = true) where T : BaseEntity
        {
            Context.Set<T>().Add(entity);
            Context.SaveChanges();

            Object id = entity.Id;

            if (disposeContext)
            {
                Context.Dispose();
                Context = new SoftContext(GetTestDbName());
            }

            var fromDb = Context.Set<T>().Find(id);
            return fromDb;
        }
    }
}