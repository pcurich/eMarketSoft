namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa una descarga de tipo de activacion 
    /// </summary>
    public enum DownloadActivationType
    {
        /// <summary>
        /// Cuando la orden a sido pagada
        /// </summary>
        WhenOrderIsPaid = 1,

        /// <summary>
        /// Manualmente
        /// </summary>
        Manually = 10,
    }
}