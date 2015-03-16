using Soft.Core;
using Soft.Core.Domain.Affiliates;

namespace Soft.Services.Affiliates
{
    /// <summary>
    /// Interfaz para los servicios de afiliados
    /// </summary>
    public partial interface IAffiliateService
    {
        /// <summary>
        /// Obtiene un afiliado desde un id
        /// </summary>
        /// <param name="affiliateId">Identificador del afiliado</param>
        /// <returns>Afiliado</returns>
        Affiliate GetAffiliateById(int affiliateId);

        /// <summary>
        /// Marca a un afiliado para ser borrado
        /// </summary>
        /// <param name="affiliate">Afiliado</param>
        void DeleteAffiliate(Affiliate affiliate);

        /// <summary>
        /// Optiene todos los afiliados
        /// </summary>
        /// <param name="showHidden">Si se debe mostrar los registros activos</param>
        /// <param name="pageIndex">Index de la pagina</param>
        /// <param name="pageSize">Tamaño de la pagina</param>
        /// <returns>Coleccion de afiliados</returns>
        IPagedList<Affiliate> GetAllAffiliates(int pageIndex, int pageSize, bool showHidden = false);

        /// <summary>
        /// Inserta un afiliado
        /// </summary>
        /// <param name="affiliate">Afiliado</param>
        void InsertAffiliate(Affiliate affiliate);

        /// <summary>
        /// Actualiza un afiliado
        /// </summary>
        /// <param name="affiliate">Afiliado</param>
        void UpdateAffiliate(Affiliate affiliate);
        
    }
}