using System.Web.Routing;
using Soft.Services.Seo;

namespace Soft.Web.Framework.Seo
{
    /// <summary>
    /// Evento para manejar nombres de entidad de registro URL desconocidos
    /// </summary>
    public class CustomUrlRecordEntityNameRequested
    {
        public RouteData RouteData { get; private set; }
        public UrlRecordService.UrlRecordForCaching UrlRecord { get; private set; }

        public CustomUrlRecordEntityNameRequested(RouteData routeData, UrlRecordService.UrlRecordForCaching urlRecord)
        {
            RouteData = routeData;
            UrlRecord = urlRecord;
        }
    }
}