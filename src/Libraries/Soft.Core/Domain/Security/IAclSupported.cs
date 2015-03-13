namespace Soft.Core.Domain.Security
{
    /// <summary>
    /// Representa un entity con soporte para ACL
    /// </summary>
    public partial interface IAclSupported
    {
        /// <summary>
        /// Indica si el entity esta sujero a un ACL
        /// </summary>
        bool SubjectToAcl { get; set; }
    }
}