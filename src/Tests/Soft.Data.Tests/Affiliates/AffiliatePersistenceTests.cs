using System;
using NUnit.Framework;
using Soft.Core.Domain.Affiliates;
using Soft.Core.Domain.Common;
using Soft.Core.Domain.Directory;
using Soft.Test;

namespace Soft.Data.Test.Affiliates
{
    [TestFixture]
    public class AffiliatePersistenceTests : PersistenceTest
    {
        [Test]
        public void Can_save_and_load_affiliate()
        {
            var affiliate = new Affiliate()
            {
                Deleted = true,
                Active = true,
                Address = GetTestAddress(),
            };

            var fromDb = SaveAndLoadEntity(affiliate);
            fromDb.ShouldNotBeNull();
            fromDb.Deleted.ShouldBeTrue();
            fromDb.Active.ShouldBeTrue();
            fromDb.Address.FirstName.ShouldEqual("FirstName 1");

        }

        protected Address GetTestAddress()
        {
            return new Address
            {
                FirstName = "FirstName 1",
                LastName = "LastName 1",
                Email = "Email 1",
                Company = "Company 1",
                City = "City 1",
                Address1 = "Address1a",
                Address2 = "Address1a",
                ZipPostalCode = "ZipPostalCode 1",
                PhoneNumber = "PhoneNumber 1",
                FaxNumber = "FaxNumber 1",
                CreatedOnUtc = new DateTime(2010, 01, 01),
                Country = new Country
                {
                    Name = "United States",
                    TwoLetterIsoCode = "US",
                    ThreeLetterIsoCode = "USA",
                }
            };
        }
    }
}