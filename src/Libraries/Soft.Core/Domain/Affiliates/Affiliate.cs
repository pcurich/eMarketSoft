using Soft.Core.Domain.Common;

namespace Soft.Core.Domain.Affiliates
{
    /// <summary>
    /// Representa un afiliado
    /// </summary>
    public partial class Affiliate : BaseEntity
    {
        public int AddressId { get; set; }

        /// <summary>
        /// Comentarios del administrador
        /// </summary>
        public string AdminComment { get; set; }

        /// <summary>
        /// Eliminacion Logica
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Registro activo
        /// </summary>
        public bool Active { get; set; }

        public virtual Address Address { get; set; }
    }
}