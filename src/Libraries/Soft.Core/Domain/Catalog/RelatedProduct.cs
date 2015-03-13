namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa una relacion entre 2 productos
    /// </summary>
    public partial class RelatedProduct : BaseEntity
    {
        /// <summary>
        /// Identificador del primer producto 
        /// </summary>
        public int ProductId1 { get; set; }

        /// <summary>
        /// Identificador del segundo producto 
        /// </summary>
        public int ProductId2 { get; set; }

        /// <summary>
        /// Orden de aparicion
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}