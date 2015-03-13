using System.Collections.Generic;

namespace Soft.Core.Plugins.Official
{
    public interface IOfficialFeedManager
    {
        IList<OfficialFeedCategory> GetCategories();
        IList<OfficialFeedVersion> GetVersions();

        /// <summary>
        /// Retorna todos los plugins
        /// </summary>
        /// <param name="categoryId">Identificador de la categoria</param>
        /// <param name="versionId">Identificador de la version</param>
        /// <param name="price">0 - Todos, 10 - gratis, 20 - paga</param 
        /// <param name="searchTerm">Termino de busqueda</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedList<OfficialFeedPlugin> GetAllPlugins(
            int categoryId = 0,
            int versionId = 0,
            int price = 0,
            string searchTerm = "",
            int pageIndex = 0,
            int pageSize = int.MaxValue);
    }
}