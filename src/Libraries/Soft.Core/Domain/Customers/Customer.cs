using System;
using System.Collections.Generic;
using Soft.Core.Domain.Common;
using Soft.Core.Domain.Orders;

namespace Soft.Core.Domain.Customers
{
    /// <summary>
    ///     Representa a un cliente
    /// </summary>
    public class Customer : BaseEntity
    {
        private ICollection<Address> _addresses;
        private ICollection<CustomerRole> _customerRoles;
        private ICollection<ExternalAuthenticationRecord> _externalAuthenticationRecords;
        private ICollection<ReturnRequest> _returnRequests;
        private ICollection<RewardPointsHistory> _rewardPointsHistory;
        private ICollection<ShoppingCartItem> _shoppingCartItems;

        #region Ctr

        public Customer()
        {
            CustomerGuid = Guid.NewGuid();
            PasswordFormat = PasswordFormat.Clear;
        }

        #endregion

        /// <summary>
        ///     Identificador unico de un cliente
        /// </summary>
        /// <value>
        ///     Identificador unico del cliente
        /// </value>
        public Guid CustomerGuid { get; set; }

        /// <summary>
        ///     Nombre de usuario
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     Correo electronico del cliente
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Contraseña del cliente
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Identificador del tipo de formato: <c>Clear = 0</c>,<c>Hashed = 1</c>,<c>Encrypted = 2</c>
        /// </summary>
        public int PasswordFormatId { get; set; }

        public PasswordFormat PasswordFormat
        {
            get { return (PasswordFormat) PasswordFormatId; }
            set { PasswordFormatId = (int) value; }
        }

        public string PasswordSalt { get; set; }
        public string AdminComment { get; set; }
        public bool IsTaxExempt { get; set; }
        public int AffiliateId { get; set; }

        /// <summary>
        ///     Identificador del vendedor con el que este consumidor esta asociado (administrador)
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        ///     Indica si el consumidor tiene productos en el carrito de compras
        /// </summary>
        /// <remarks>
        ///     Se usa para optimizar:
        ///     Si es falso entonces se debe cargar de ShoopingCartItems
        /// </remarks>
        public bool HasShoppingCartItems { get; set; }

        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public bool IsSystemAccount { get; set; }
        public string SystemName { get; set; }
        public string LastIpAddress { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? LastLoginDateUtc { get; set; }
        public DateTime LastActivityDateUtc { get; set; }

        #region Propiedades

        public virtual ICollection<ExternalAuthenticationRecord> ExternalAuthenticationRecords
        {
            get
            {
                return _externalAuthenticationRecords ??
                       (_externalAuthenticationRecords = new List<ExternalAuthenticationRecord>());
            }
            protected set { _externalAuthenticationRecords = value; }
        }

        public virtual ICollection<CustomerRole> CustomerRoles
        {
            get { return _customerRoles ?? (_customerRoles = new List<CustomerRole>()); }
            protected set { _customerRoles = value; }
        }

        public virtual ICollection<ShoppingCartItem> ShoppingCartItems
        {
            get { return _shoppingCartItems ?? (_shoppingCartItems = new List<ShoppingCartItem>()); }
            protected set { _shoppingCartItems = value; }
        }

        /// <summary>
        ///     Historia de puntos
        /// </summary>
        public virtual ICollection<RewardPointsHistory> RewardPointsHistory
        {
            get { return _rewardPointsHistory ?? (_rewardPointsHistory = new List<RewardPointsHistory>()); }
            protected set { _rewardPointsHistory = value; }
        }

        public virtual ICollection<ReturnRequest> ReturnRequests
        {
            get { return _returnRequests ?? (_returnRequests = new List<ReturnRequest>()); }
            protected set { _returnRequests = value; }
        }

        /// <summary>
        ///     Direccion de facturacion
        /// </summary>
        public virtual Address BillingAddress { get; set; }

        /// <summary>
        ///     Direccion de envio
        /// </summary>
        public virtual Address ShippingAddress { get; set; }

        /// <summary>
        ///     Direcciones del cliente
        /// </summary>
        public virtual ICollection<Address> Addresses
        {
            get { return _addresses ?? (_addresses = new List<Address>()); }
            protected set { _addresses = value; }
        }

        #endregion
    }
}