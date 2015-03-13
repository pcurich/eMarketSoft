using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Soft.Web.Framework.Events
{
    /// <summary>
    ///     Admin tabstrip evento creado
    /// </summary>
    public class AdminTabStripCreated
    {
        public HtmlHelper Helper { get; private set; }
        public string TabStripName { get; private set; }
        public IList<MvcHtmlString> BlocksToRender { get; set; }

        public AdminTabStripCreated(HtmlHelper helper, string tabStripName)
        {
            Helper = helper;
            TabStripName = tabStripName;
            BlocksToRender = new List<MvcHtmlString>();
        }
    }
}