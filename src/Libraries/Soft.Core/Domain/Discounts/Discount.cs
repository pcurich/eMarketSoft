using System;
using System.Collections.Generic;
using Soft.Core.Domain.Catalog;

namespace Soft.Core.Domain.Discounts
{
    /// <summary>
    /// Representa un descuento
    /// </summary>
    public partial class Discount : BaseEntity
    {
        private ICollection<DiscountRequirement> _discountRequirements;
        private ICollection<Category> _appliedToCategories;
        private ICollection<Product> _appliedToProducts;

        /// <summary>
        /// Nomnre del decuento
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Identificador del tipo de descuento 
        /// </summary>
        public int DiscountTypeId { get; set; }

        /// <summary>
        /// Si se va a usar porcentajes
        /// </summary>
        public bool UsePercentage { get; set; }

        /// <summary>
        /// Valor del descuento en porcentaje
        /// </summary>
        public decimal DiscountPercentage { get; set; }

        /// <summary>
        /// Monto del descuento
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// Inicio del descuento
        /// </summary>
        public DateTime? StartDateUtc { get; set; }

        /// <summary>
        /// Fin del descuento
        /// </summary>
        public DateTime? EndDateUtc { get; set; }

        /// <summary>
        /// Si se requiere cupones de descuentos
        /// </summary>
        public bool RequiresCouponCode { get; set; }

        /// <summary>
        /// Codigo del cupon
        /// </summary>
        public string CouponCode { get; set; }

        /// <summary>
        /// Identificador de las limitaciones del descuento
        /// </summary>
        public int DiscountLimitationId { get; set; }

        /// <summary>
        /// Tiempo de limites del descuento 
        /// (usado cuando la limitacion es "N Times Only" or "N Timespor cliente")
        /// </summary>
        public int LimitationTimes { get; set; }

        /// <summary>
        /// Maxima cantidad de producto a la que aplica el descuento 
        /// Usado con "Asignado a producto" o "Asignado a categorias"
        /// </summary>
        public int? MaximumDiscountedQuantity { get; set; }

        /// <summary>
        /// Tipo de desciuento
        /// </summary>
        public DiscountType DiscountType
        {
            get { return (DiscountType) DiscountTypeId; }
            set { DiscountTypeId = (int) value; }
        }

        /// <summary>
        /// Limitaciones del descuento
        /// </summary>
        public DiscountLimitationType DiscountLimitation
        {
            get { return (DiscountLimitationType) DiscountLimitationId; }
            set { DiscountLimitationId = (int) value; }
        }

        /// <summary>
        /// Requisitos del descuento
        /// </summary>
        public virtual ICollection<DiscountRequirement> DiscountRequirements
        {
            get { return _discountRequirements ?? (_discountRequirements = new List<DiscountRequirement>()); }
            protected set { _discountRequirements = value; }
        }

        /// <summary>
        /// Categorias
        /// </summary>
        public virtual ICollection<Category> AppliedToCategories
        {
            get { return _appliedToCategories ?? (_appliedToCategories = new List<Category>()); }
            protected set { _appliedToCategories = value; }
        }

        /// <summary>
        /// Productos
        /// </summary>
        public virtual ICollection<Product> AppliedToProducts
        {
            get { return _appliedToProducts ?? (_appliedToProducts = new List<Product>()); }
            protected set { _appliedToProducts = value; }
        }
    }
}