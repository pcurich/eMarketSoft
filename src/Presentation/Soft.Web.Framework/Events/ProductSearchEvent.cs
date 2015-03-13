using System.Collections.Generic;

namespace Soft.Web.Framework.Events
{
    /// <summary>
    /// Evento de busqueda de producto
    /// </summary>
    public class ProductSearchEvent
    {
        public string SearchTerm { get; set; }
        public bool SearchInDescriptions { get; set; }
        public IList<int> CategoryIds { get; set; }
        public int ManufacturerId { get; set; }
        public int WorkingLanguageId { get; set; } 
    }
}