namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa una actividad baja de stock
    /// </summary>
    public enum LowStockActivity
    {
        /// <summary>
        /// Nada
        /// </summary>
        Nothing = 0,

        /// <summary>
        /// Desabilitar el boton de compra
        /// </summary>
        DisableBuyButton = 1,

        /// <summary>
        /// No mostrar
        /// </summary>
        Unpublish = 2,
    }
}