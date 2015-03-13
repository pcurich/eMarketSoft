﻿using NUnit.Framework;
using Soft.Core.Domain.Common;
using Soft.Test;

namespace Soft.Data.Test.Common
{
    [TestFixture]
    public class SearchTermPeristenceTests : PersistenceTest
    {
        [Test]
        public void Can_save_and_load_searchTerm()
        {
            var searchTerm = new SearchTerm
            {
                Keyword = "Keyword 1",
                StoreId = 1,
                Count = 2,
            };

            var fromDb = SaveAndLoadEntity(searchTerm);
            fromDb.ShouldNotBeNull();

            fromDb.Keyword.ShouldEqual("Keyword 1");
            fromDb.StoreId.ShouldEqual(1);
            fromDb.Count.ShouldEqual(2);
        }
    }
}
