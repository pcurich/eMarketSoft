﻿using NUnit.Framework;
using Soft.Core.Domain.Catalog;
using Soft.Core.Domain.Common;
using Soft.Test;

namespace Soft.Data.Test.Common
{
    [TestFixture]
    public class AttributeValuePersistenceTests : PersistenceTest
    {
        [Test]
        public void Can_save_and_load_addressAttributeValue()
        {
            var cav = new AddressAttributeValue
            {
                Name = "Name 2",
                IsPreSelected = true,
                DisplayOrder = 1,
                AddressAttribute = new AddressAttribute
                {
                    Name = "Name 1",
                    IsRequired = true,
                    AttributeControlType = AttributeControlType.DropdownList,
                    DisplayOrder = 2
                }
            };

            var fromDb = SaveAndLoadEntity(cav);
            fromDb.ShouldNotBeNull();
            fromDb.Name.ShouldEqual("Name 2");
            fromDb.IsPreSelected.ShouldEqual(true);
            fromDb.DisplayOrder.ShouldEqual(1);

            fromDb.AddressAttribute.ShouldNotBeNull();
            fromDb.AddressAttribute.Name.ShouldEqual("Name 1");
        }
    }
}
