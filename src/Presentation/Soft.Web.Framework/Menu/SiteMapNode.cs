using System.Collections.Generic;
using System.Web.Routing;

//Tomado de Telerick MVC Extensions
namespace Soft.Web.Framework.Menu
{
    public class SiteMapNode
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="SiteMapNode"/>
        /// </summary>
        public SiteMapNode()
        {
            RouteValues = new RouteValueDictionary();
            ChildNodes = new List<SiteMapNode>();
        }

        /// <summary>
        /// Obtiene el titulo
        /// </summary>
        /// <value>
        /// El titulo.
        /// </value>
        public string Title { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public RouteValueDictionary RouteValues { get; set; }
        public string Url { get; set; }
        public IList<SiteMapNode> ChildNodes { get; set; }
        public string ImageUrl { get; set; }
        public bool Visible { get; set; }
    }
}