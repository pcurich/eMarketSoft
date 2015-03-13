using System.Collections.Generic;
using Soft.Core.Configuration;

namespace Soft.Core.Domain.Cms
{
    public class WidgetSettings : ISettings
    {
        public WidgetSettings()
        {
            ActiveWidgetSystemNames = new List<string>();
        }

        /// <summary>
        /// Nombre de los sistemas activados de widgets
        /// </summary>
        public List<string> ActiveWidgetSystemNames { get; set; }
    }
}