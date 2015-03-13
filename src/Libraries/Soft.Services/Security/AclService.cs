using System;
using System.Collections.Generic;
using System.Linq;
using Soft.Core;
using Soft.Core.Caching;
using Soft.Core.Data;
using Soft.Core.Domain.Catalog;
using Soft.Core.Domain.Customers;
using Soft.Core.Domain.Security;

namespace Soft.Services.Security
{
    /// <summary>
    /// Servicio de ACL
    /// </summary>
    public partial class AclService : IAclService
    {
        #region ConstantES

        /// <summary>
        /// Lave del cache
        /// </summary>
        /// <remarks>
        /// {0} : entity ID
        /// {1} : entity name
        /// </remarks>
        private const string AclrecordByEntityidNameKey = "Soft.aclrecord.entityid-name-{0}-{1}";

        /// <summary>
        /// Llave de patron a borrar de la cache
        /// </summary>
        private const string AclrecordPatternKey = "Soft.aclrecord.";

        #endregion

        #region Fields

        private readonly IRepository<AclRecord> _aclRecordRepository;
        private readonly IWorkContext _workContext;
        private readonly ICacheManager _cacheManager;
        private readonly CatalogSettings _catalogSettings;

        #endregion

        #region Ctr

        /// <summary>
        /// Constructos
        /// </summary>
        /// <param name="cacheManager"></param>
        /// <param name="workContext"></param>
        /// <param name="aclRecordRepository"></param>
        /// <param name="catalogSettings"></param>
        public AclService(ICacheManager cacheManager,
            IWorkContext workContext,
            IRepository<AclRecord> aclRecordRepository,
            CatalogSettings catalogSettings)
        {
            _aclRecordRepository = aclRecordRepository;
            _workContext = workContext;
            _cacheManager = cacheManager;
            _catalogSettings = catalogSettings;
        }

        #endregion

        public void DeleteAclRecord(AclRecord aclRecord)
        {
            if (aclRecord == null)
                throw new ArgumentNullException("aclRecord");

            _aclRecordRepository.Delete(aclRecord);
            _cacheManager.RemoveByPattern(AclrecordPatternKey);
        }

        public AclRecord GetAclRecordById(int aclRecordId)
        {
            return aclRecordId == 0 ? null : _aclRecordRepository.GetById(aclRecordId);
        }

        public IList<AclRecord> GetAclRecords<T>(T entity) where T : BaseEntity, IAclSupported
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var entityId = entity.Id;
            var entityName = typeof(T).Name;

            var query = from ur in _aclRecordRepository.Table
                        where ur.EntityId == entityId &&
                        ur.EntityName == entityName
                        select ur;

            var aclRecords = query.ToList();
            return aclRecords;
        }

        public void InsertAclRecord(AclRecord aclRecord)
        {
            if (aclRecord == null)
                throw new ArgumentNullException("aclRecord");

            _aclRecordRepository.Insert(aclRecord);

            //cache
            _cacheManager.RemoveByPattern(AclrecordPatternKey);
        }

        public void InsertAclRecord<T>(T entity, int customerRoleId) where T : BaseEntity, IAclSupported
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (customerRoleId == 0)
                throw new ArgumentOutOfRangeException("customerRoleId");

            int entityId = entity.Id;
            string entityName = typeof(T).Name;

            var aclRecord = new AclRecord
            {
                EntityId = entityId,
                EntityName = entityName,
                CustomerRoleId = customerRoleId
            };

            InsertAclRecord(aclRecord);
        }

        public void UpdateAclRecord(AclRecord aclRecord)
        {
            if (aclRecord == null)
                throw new ArgumentNullException("aclRecord");

            _aclRecordRepository.Update(aclRecord);

            //cache
            _cacheManager.RemoveByPattern(AclrecordPatternKey);
        }

        public int[] GetCustomerRoleIdsWithAccess<T>(T entity) where T : BaseEntity, IAclSupported
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var entityId = entity.Id;
            var entityName = typeof(T).Name;

            var key = string.Format(AclrecordByEntityidNameKey, entityId, entityName);
            return _cacheManager.Get(key, () =>
            {
                var query = from ur in _aclRecordRepository.Table
                            where ur.EntityId == entityId &&
                            ur.EntityName == entityName
                            select ur.CustomerRoleId;
                return query.ToArray();
            });
        }

        public bool Authorize<T>(T entity) where T : BaseEntity, IAclSupported
        {
            return Authorize(entity, _workContext.CurrentCustomer);
        }

        public bool Authorize<T>(T entity, Customer customer) where T : BaseEntity, IAclSupported
        {
            if (entity == null)
                return false;

            if (customer == null)
                return false;

            if (_catalogSettings.IgnoreAcl)
                return true;

            if (!entity.SubjectToAcl)
                return true;

            foreach (var role1 in customer.CustomerRoles.Where(cr => cr.Active))
                foreach (var role2Id in GetCustomerRoleIdsWithAccess(entity))
                    if (role1.Id == role2Id)
                        //yes, we have such permission
                        return true;

            //no permission found
            return false;
        }
    }
}