namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa una combinacion de atributos de un producto
    /// </summary>
    public partial class ProductAttributeCombination : BaseEntity
    {
        /// <summary>
        /// Identificador de producto
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Atributos en xml 
        /// </summary>
        public string AttributesXml { get; set; }

        /// <summary>
        /// Cantidad de stock
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// Si se permite ordenar cuando no hay stock
        /// </summary>
        public bool AllowOutOfStockOrders { get; set; }

        /// <summary>
        /// SKU
        /// </summary>
        public string Sku { get; set; }

        /// <summary>
        /// parte del numero del proveedor
        /// </summary>
        public string ManufacturerPartNumber { get; set; }

        /// <summary>
        /// El Global Trade Item Number (GTIN). 
        /// Este identificador incluye el UPC (in North America), 
        /// EAN (in Europe), JAN (in Japan), and ISBN (for books).
        /// </summary>
        public string Gtin { get; set; }

        /// <summary>
        /// Combinacion de precios. 
        /// De esta manera se sobreescribe el precio por default cuando la 
        /// combinacion de atributos esta agregada al carrito 
        /// Por ejemplo dar un descuento 
        /// </summary>
        public decimal? OverriddenPrice { get; set; }

        /// <summary>
        /// Cantidad cuand el admin deberia ser motificado
        /// </summary>
        public int NotifyAdminForQuantityBelow { get; set; }

        /// <summary>
        /// Producto
        /// </summary>
        public virtual Product Product { get; set; }
    }
}