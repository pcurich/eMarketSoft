using System;
using System.Collections.Generic;
using Soft.Core.Domain.Customers;

namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa una vista previa del producto
    /// </summary>
    public partial class ProductReview : BaseEntity
    {
        private ICollection<ProductReviewHelpfulness> _productReviewHelpfulnessEntries;

        /// <summary>
        /// Identificador del cliente
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Identificador del producto 
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Si el contenido es aprobado
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Titulo del producto
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Texto para revision 
        /// </summary>
        public string ReviewText { get; set; }

        /// <summary>
        /// Rating de la revision
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Vista pravia del total de votos utiles
        /// </summary>
        public int HelpfulYesTotal { get; set; }

        /// <summary>
        /// Vista pravia del total de votos no utiles
        /// </summary>
        public int HelpfulNoTotal { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Cliente
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Producto
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Revision de productos utiles
        /// </summary>
        public virtual ICollection<ProductReviewHelpfulness> ProductReviewHelpfulnessEntries
        {
            get
            {
                return _productReviewHelpfulnessEntries ??
                       (_productReviewHelpfulnessEntries = new List<ProductReviewHelpfulness>());
            }
            protected set { _productReviewHelpfulnessEntries = value; }
        }
    }
}