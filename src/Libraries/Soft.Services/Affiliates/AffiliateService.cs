using System;
using System.Linq;
using Soft.Core;
using Soft.Core.Data;
using Soft.Core.Domain.Affiliates;
using Soft.Services.Events;

namespace Soft.Services.Affiliates
{
    /// <summary>
    ///  Implementacion de los servicios de afiliados
    /// </summary>
    public partial class AffiliateService : IAffiliateService
    {
        #region Campos

        private readonly IRepository<Affiliate> _affiliateRepository;
        private readonly IEventPublisher _eventPublisher;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="affiliateRepository">Repositorio de un afiliado</param>
        /// <param name="eventPublisher">Evento publicado</param>
        public AffiliateService(IRepository<Affiliate> affiliateRepository,
            IEventPublisher eventPublisher)
        {
            _affiliateRepository = affiliateRepository;
            _eventPublisher = eventPublisher;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Obtiene un afiliado desde un id
        /// </summary>
        /// <param name="affiliateId">Identificador del afiliado</param>
        /// <returns>Afiliado</returns>
        public virtual Affiliate GetAffiliateById(int affiliateId)
        {
            return affiliateId == 0 ? null : _affiliateRepository.GetById(affiliateId);
        }

        /// <summary>
        /// Marca a un afiliado para ser borrado
        /// </summary>
        /// <param name="affiliate">Afiliado</param>
        public virtual void DeleteAffiliate(Affiliate affiliate)
        {
            if (affiliate == null)
                throw new ArgumentNullException("affiliate");

            affiliate.Deleted = true;
            UpdateAffiliate(affiliate);
        }

        /// <summary>
        /// Optiene todos los afiliados
        /// </summary>
        /// <param name="showHidden">Si se debe mostrar los registros activos</param>
        /// <param name="pageIndex">Index de la pagina</param>
        /// <param name="pageSize">Tamaño de la pagina</param>
        /// <returns>Coleccion de afiliados</returns>
        public virtual IPagedList<Affiliate> GetAllAffiliates(int pageIndex, int pageSize, bool showHidden = false)
        {
            var query = _affiliateRepository.Table;
            if (!showHidden)
                query = query.Where(a => a.Active);
            query = query.Where(a => !a.Deleted);
            query = query.OrderByDescending(a => a.Id);

            var affiliates = new PagedList<Affiliate>(query, pageIndex, pageSize);
            return affiliates;
        }

        /// <summary>
        /// Inserta un afiliado
        /// </summary>
        /// <param name="affiliate">Afiliado</param>
        public virtual void InsertAffiliate(Affiliate affiliate)
        {
            if (affiliate == null)
                throw new ArgumentNullException("affiliate");

            _affiliateRepository.Insert(affiliate);

            //notifica el evento
            _eventPublisher.EntityInserted(affiliate);
        }

        /// <summary>
        /// Actualiza un afiliado
        /// </summary>
        /// <param name="affiliate">Afiliado</param>
        public virtual void UpdateAffiliate(Affiliate affiliate)
        {
            if (affiliate == null)
                throw new ArgumentNullException("affiliate");

            _affiliateRepository.Update(affiliate);

            //notifica el evento
            _eventPublisher.EntityUpdated(affiliate);
        }

        #endregion
        
    }
}