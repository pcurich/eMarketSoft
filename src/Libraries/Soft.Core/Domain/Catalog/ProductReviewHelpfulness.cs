namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa una revision util para el produto
    /// </summary>
    public partial class ProductReviewHelpfulness : BaseEntity
    {
        /// <summary>
        /// Identificador de revision de producto
        /// </summary>
        public int ProductReviewId { get; set; }

        /// <summary>
        /// Si tiene revision util
        /// </summary>
        public bool WasHelpful { get; set; }

        /// <summary>
        /// Identificador de un cliente
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Revision de un producto
        /// </summary>
        public virtual ProductReview ProductReview { get; set; }
    }
}