using System;
using System.Collections.Generic;
using System.Linq;
using Soft.Core.Caching;
using Soft.Core.Data;
using Soft.Core.Domain.Customers;
using Soft.Services.Events;

namespace Soft.Services.Customers
{
    /// <summary>
    /// Customer attribute service
    /// </summary>
    public partial class CustomerAttributeService : ICustomerAttributeService
    {
        #region Constantes

        /// <summary>
        /// Llave para el cache
        /// </summary>
        private const string CustomerattributesAllKey = "Soft.customerattribute.all";
        /// <summary>
        /// Llave para el cache
        /// </summary>
        /// <remarks>
        /// {0} : Id del atributo del cliente
        /// </remarks>
        private const string CustomerattributesByIdKey = "Soft.customerattribute.id-{0}";
        /// <summary>
        /// Llave para el cache
        /// </summary>
        /// <remarks>
        /// {0} : Id del valor del atributo del cliente
        /// </remarks>
        private const string CustomerattributevaluesAllKey = "Soft.customerattributevalue.all-{0}";
        /// <summary>
        /// Llave para el cache
        /// </summary>
        /// <remarks>
        /// {0} : Id del valor del atributo del cliente
        /// </remarks>
        private const string CustomerattributevaluesByIdKey = "Soft.customerattributevalue.id-{0}";
        /// <summary>
        /// Llave del patron para borrar el cache
        /// </summary>
        private const string CustomerattributesPatternKey = "Soft.customerattribute.";
        /// <summary>
        /// Llave del patron para borrar el cache
        /// </summary>
        private const string CustomerattributevaluesPatternKey = "Soft.customerattributevalue.";
        #endregion
        
        #region Campos

        private readonly IRepository<CustomerAttribute> _customerAttributeRepository;
        private readonly IRepository<CustomerAttributeValue> _customerAttributeValueRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheManager _cacheManager;
        
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="customerAttributeRepository">Customer attribute repository</param>
        /// <param name="customerAttributeValueRepository">Customer attribute value repository</param>
        /// <param name="eventPublisher">Event published</param>
        public CustomerAttributeService(ICacheManager cacheManager,
            IRepository<CustomerAttribute> customerAttributeRepository,
            IRepository<CustomerAttributeValue> customerAttributeValueRepository,
            IEventPublisher eventPublisher)
        {
            _cacheManager = cacheManager;
            _customerAttributeRepository = customerAttributeRepository;
            _customerAttributeValueRepository = customerAttributeValueRepository;
            _eventPublisher = eventPublisher;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Deletes a customer attribute
        /// </summary>
        /// <param name="customerAttribute">Customer attribute</param>
        /// <exception cref="System.ArgumentNullException">customerAttribute</exception>
        public virtual void DeleteCustomerAttribute(CustomerAttribute customerAttribute)
        {
            if (customerAttribute == null)
                throw new ArgumentNullException("customerAttribute");

            _customerAttributeRepository.Delete(customerAttribute);

            _cacheManager.RemoveByPattern(CustomerattributesPatternKey);
            _cacheManager.RemoveByPattern(CustomerattributevaluesPatternKey);

            //event notification
            _eventPublisher.EntityDeleted(customerAttribute);
        }

        /// <summary>
        /// Gets all customer attributes
        /// </summary>
        /// <returns>Customer attributes</returns>
        public virtual IList<CustomerAttribute> GetAllCustomerAttributes()
        {
            const string key = CustomerattributesAllKey;
            return _cacheManager.Get(key, () =>
            {
                var query = from ca in _customerAttributeRepository.Table
                            orderby ca.DisplayOrder
                            select ca;
                return query.ToList();
            });
        }

        /// <summary>
        /// Gets a customer attribute 
        /// </summary>
        /// <param name="customerAttributeId">Customer attribute identifier</param>
        /// <returns>Customer attribute</returns>
        public virtual CustomerAttribute GetCustomerAttributeById(int customerAttributeId)
        {
            if (customerAttributeId == 0)
                return null;

            string key = string.Format(CustomerattributesByIdKey, customerAttributeId);
            return _cacheManager.Get(key, () => _customerAttributeRepository.GetById(customerAttributeId));
        }

        /// <summary>
        /// Inserts a customer attribute
        /// </summary>
        /// <param name="customerAttribute">Customer attribute</param>
        public virtual void InsertCustomerAttribute(CustomerAttribute customerAttribute)
        {
            if (customerAttribute == null)
                throw new ArgumentNullException("customerAttribute");

            _customerAttributeRepository.Insert(customerAttribute);

            _cacheManager.RemoveByPattern(CustomerattributesPatternKey);
            _cacheManager.RemoveByPattern(CustomerattributevaluesPatternKey);

            //event notification
            _eventPublisher.EntityInserted(customerAttribute);
        }

        /// <summary>
        /// Updates the customer attribute
        /// </summary>
        /// <param name="customerAttribute">Customer attribute</param>
        public virtual void UpdateCustomerAttribute(CustomerAttribute customerAttribute)
        {
            if (customerAttribute == null)
                throw new ArgumentNullException("customerAttribute");

            _customerAttributeRepository.Update(customerAttribute);

            _cacheManager.RemoveByPattern(CustomerattributesPatternKey);
            _cacheManager.RemoveByPattern(CustomerattributevaluesPatternKey);

            //event notification
            _eventPublisher.EntityUpdated(customerAttribute);
        }

        /// <summary>
        /// Deletes a customer attribute value
        /// </summary>
        /// <param name="customerAttributeValue">Customer attribute value</param>
        public virtual void DeleteCustomerAttributeValue(CustomerAttributeValue customerAttributeValue)
        {
            if (customerAttributeValue == null)
                throw new ArgumentNullException("customerAttributeValue");

            _customerAttributeValueRepository.Delete(customerAttributeValue);

            _cacheManager.RemoveByPattern(CustomerattributesPatternKey);
            _cacheManager.RemoveByPattern(CustomerattributevaluesPatternKey);

            //event notification
            _eventPublisher.EntityDeleted(customerAttributeValue);
        }

        /// <summary>
        /// Gets customer attribute values by customer attribute identifier
        /// </summary>
        /// <param name="customerAttributeId">The customer attribute identifier</param>
        /// <returns>Customer attribute values</returns>
        public virtual IList<CustomerAttributeValue> GetCustomerAttributeValues(int customerAttributeId)
        {
            string key = string.Format(CustomerattributevaluesAllKey, customerAttributeId);
            return _cacheManager.Get(key, () =>
            {
                var query = from cav in _customerAttributeValueRepository.Table
                            orderby cav.DisplayOrder
                            where cav.CustomerAttributeId == customerAttributeId
                            select cav;
                var customerAttributeValues = query.ToList();
                return customerAttributeValues;
            });
        }
        
        /// <summary>
        /// Gets a customer attribute value
        /// </summary>
        /// <param name="customerAttributeValueId">Customer attribute value identifier</param>
        /// <returns>Customer attribute value</returns>
        public virtual CustomerAttributeValue GetCustomerAttributeValueById(int customerAttributeValueId)
        {
            if (customerAttributeValueId == 0)
                return null;

            string key = string.Format(CustomerattributevaluesByIdKey, customerAttributeValueId);
            return _cacheManager.Get(key, () => _customerAttributeValueRepository.GetById(customerAttributeValueId));
        }

        /// <summary>
        /// Inserts a customer attribute value
        /// </summary>
        /// <param name="customerAttributeValue">Customer attribute value</param>
        public virtual void InsertCustomerAttributeValue(CustomerAttributeValue customerAttributeValue)
        {
            if (customerAttributeValue == null)
                throw new ArgumentNullException("customerAttributeValue");

            _customerAttributeValueRepository.Insert(customerAttributeValue);

            _cacheManager.RemoveByPattern(CustomerattributesPatternKey);
            _cacheManager.RemoveByPattern(CustomerattributevaluesPatternKey);

            //event notification
            _eventPublisher.EntityInserted(customerAttributeValue);
        }

        /// <summary>
        /// Updates the customer attribute value
        /// </summary>
        /// <param name="customerAttributeValue">Customer attribute value</param>
        public virtual void UpdateCustomerAttributeValue(CustomerAttributeValue customerAttributeValue)
        {
            if (customerAttributeValue == null)
                throw new ArgumentNullException("customerAttributeValue");

            _customerAttributeValueRepository.Update(customerAttributeValue);

            _cacheManager.RemoveByPattern(CustomerattributesPatternKey);
            _cacheManager.RemoveByPattern(CustomerattributevaluesPatternKey);

            //event notification
            _eventPublisher.EntityUpdated(customerAttributeValue);
        }
        
        #endregion
    }
}
