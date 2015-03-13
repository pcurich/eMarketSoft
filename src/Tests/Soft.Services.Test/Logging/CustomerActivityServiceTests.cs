using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Soft.Core;
using Soft.Core.Caching;
using Soft.Core.Data;
using Soft.Core.Domain.Customers;
using Soft.Core.Domain.Logging;
using Soft.Services.Logging;
using Soft.Test;

namespace Soft.Services.Test.Logging
{
     [TestFixture]
    public class CustomerActivityServiceTests : ServiceTest
    {
        private ICacheManager _cacheManager;
        private IRepository<ActivityLog> _activityLogRepository;
        private IRepository<ActivityLogType> _activityLogTypeRepository;
        private IWorkContext _workContext;
        private ICustomerActivityService _customerActivityService;
        private ActivityLogType _newCustomerType, _newCategoryType;
        private ActivityLog _activityNewCustomer, _activityNewCategory;
        private Customer _newPcurichCustomer, _newAdminCustomer;

        [SetUp]
        public new void SetUp()
        {
            _newCustomerType = new ActivityLogType
            {
                Id = 1,
                SystemKeyword = "NuevoCliente",
                Enabled = true,
                Name = "Se agrego un nuevo cliente"
            };

            _newCategoryType = new ActivityLogType
            {
                Id = 2,
                SystemKeyword = "NuevaCategoria",
                Enabled = true,
                Name = "Agrego una nueva categoria"
            };

            _newPcurichCustomer = new Customer
            {
                Id = 1,
                Email = "pcurich@localhost.com",
                Username = "pcurich",
                Deleted = false,
            };

            _newAdminCustomer = new Customer
            {
                Id = 2,
                Email = "admin@localhost.com",
                Username = "admin",
                Deleted = false,
            };

            _activityNewCustomer = new ActivityLog
            {
                Id = 1,
                ActivityLogType = _newCustomerType,
                CustomerId = _newPcurichCustomer.Id,
                Customer = _newPcurichCustomer
            };
            _activityNewCategory = new ActivityLog
            {
                Id = 2,
                ActivityLogType = _newCustomerType,
                CustomerId = _newAdminCustomer.Id,
                Customer = _newAdminCustomer
            };
            _cacheManager = new SoftNullCache();
            _workContext = MockRepository.GenerateMock<IWorkContext>();
            _activityLogRepository = MockRepository.GenerateMock<IRepository<ActivityLog>>();
            _activityLogTypeRepository = MockRepository.GenerateMock<IRepository<ActivityLogType>>();
            _activityLogTypeRepository.Expect(x => x.Table).Return(new List<ActivityLogType> { _newCategoryType, _newCustomerType }.AsQueryable());
            _activityLogRepository.Expect(x => x.Table).Return(new List<ActivityLog> { _activityNewCustomer, _activityNewCategory }.AsQueryable());
            _customerActivityService = new CustomerActivityService(_cacheManager,
                                        _activityLogRepository, 
                                        _activityLogTypeRepository, 
                                        _workContext, null, null, null);
        }

        [Test]
        public void Can_Find_Activities()
        {
            var activities = _customerActivityService.GetAllActivities(null, null, 1, 0, 0, 10);
            activities.Contains(_activityNewCustomer).ShouldBeTrue();
            activities = _customerActivityService.GetAllActivities(null, null, 2, 0, 0, 10);
            activities.Contains(_activityNewCustomer).ShouldBeFalse();
            activities = _customerActivityService.GetAllActivities(null, null, 2, 0, 0, 10);
            activities.Contains(_activityNewCategory).ShouldBeTrue();
        }

    }
}
