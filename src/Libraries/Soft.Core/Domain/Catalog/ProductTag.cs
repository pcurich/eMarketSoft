using System.Collections.Generic;
using Soft.Core.Domain.Localization;

namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa el tag de un roducto
    /// </summary>
    public partial class ProductTag : BaseEntity, ILocalizedEntity
    {
        private ICollection<Product> _products;

        /// <summary>
        /// Nombre del tag
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// lista de productos 
        /// </summary>
        public virtual ICollection<Product> Products
        {
            get { return _products ?? (_products = new List<Product>()); }
            protected set { _products = value; }
        }
    }
}