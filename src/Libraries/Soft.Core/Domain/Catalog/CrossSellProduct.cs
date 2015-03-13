namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Ventas cruzadas de productos
    /// </summary>
    public partial class CrossSellProduct : BaseEntity
    {
        /// <summary>
        /// Primier producto 
        /// </summary>
        public int ProductId1 { get; set; }

        /// <summary>
        /// Segundo producto
        /// </summary>
        public int ProductId2 { get; set; }
    }
}