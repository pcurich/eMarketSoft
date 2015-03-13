namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa un metodo de control de inventario
    /// </summary>
    public enum ManageInventoryMethod
    {
        /// <summary>
        /// No seguir los productos en el inventario
        /// </summary>
        DontManageStock = 0,

        /// <summary>
        /// Seguir el inventario del producto
        /// </summary>
        ManageStock = 1,

        /// <summary>
        /// Sigue el inventario del producto por los atributos del producto
        /// </summary>
        ManageStockByAttributes = 2,
    }
}