namespace Soft.Core.Domain.Tax
{
    /// <summary>
    /// Representa el numero de IGV
    /// Represents the VAT number status enumeration
    /// </summary>
    public enum VatNumberStatus
    {
        /// <summary>
        /// Desconocido
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Vacio
        /// </summary>
        Empty = 10,

        /// <summary>
        /// Valido
        /// </summary>
        Valid = 20,

        /// <summary>
        /// Invalido 
        /// </summary>
        Invalid = 30
    }
}