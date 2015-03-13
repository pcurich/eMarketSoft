namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa un orden de pedidos
    /// </summary>
    public enum BackorderMode
    {
        /// <summary>
        /// No hay pedicos pendientes
        /// </summary>
        NoBackorders = 0,

        /// <summary>
        /// Permite cantidad superior a cero
        /// </summary>
        AllowQtyBelow0 = 1,

        /// <summary>
        /// Cantidades superiores a cero y notificar al cliente
        /// </summary>
        AllowQtyBelow0AndNotifyCustomer = 2,
    }
}