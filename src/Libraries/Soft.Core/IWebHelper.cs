using System.Web;

namespace Soft.Core
{
    /// <summary>
    /// Representa ayudas comunes
    /// </summary>
    public partial interface IWebHelper
    {
        /// <summary>
        /// Obtener URL de referencia
        /// </summary>
        /// <returns>Url de referencia</returns>
        string GetUrlReferrer();

        /// <summary>
        /// Optiene la Direccion Ip del contexto
        /// </summary>
        /// <returns>Url de referencia</returns>
        string GetCurrentIpAddress();

        /// <summary>
        /// Obtiene el nombre de esta pagina
        /// </summary>
        /// <param name="includeQueryString">Valor que indica si se debe incluir las cadenas de consulta</param>
        /// <returns>Nombre de la paguina</returns>
        string GetThisPageUrl(bool includeQueryString);


        /// <summary>
        /// Obtiene el nombre de esta pagina
        /// </summary>
        /// <param name="includeQueryString">Valor que indica si se debe incluir las cadenas de consulta</param>
        /// <param name="useSsl">Valor que indica si la pagina esta protegida por SSL</param>
        /// <returns>Nombre de la paguina</returns>
        string GetThisPageUrl(bool includeQueryString, bool useSsl);

        /// <summary>
        /// Optiene un valor indicando que la conexion actual es segura
        /// </summary>
        /// <returns>true - segura, false - no segura</returns>
        bool IsCurrentConnectionSecured();

        /// <summary>
        /// Obtiene las variables del servidor por nombre
        /// </summary>
        /// <param name="name">Nombre</param>
        /// <returns>Lista de variables</returns>
        string ServerVariables(string name);

        /// <summary>
        /// Obtiene la localizacion del host
        /// </summary>
        /// <param name="useSsl">Usa SSL</param>
        /// <returns>Localizacion del host</returns>
        string GetStoreHost(bool useSsl);

        /// <summary>
        /// Obtiene la localizacion de la tienda
        /// </summary>
        /// <returns>locacion de la tienda</returns>
        string GetStoreLocation();

        /// <summary>
        /// Obtiene la localizacion de la tienda
        /// </summary>
        /// <param name="useSsl">Usa SSL</param>
        /// <returns>locacion de la tienda</returns>
        string GetStoreLocation(bool useSsl);

        /// <summary>
        /// Retorna true si el recurso del request es uno de los recursos tipicos que no necesitan ser procesador por el cms
        /// </summary>
        /// <param name="request">HTTP Request</param>
        /// <returns>True si la etiqueta del recurso es un archivo de recurso estatico.</returns>
        /// <remarks>
        /// Extensiones consideradas recursos estaticos
        /// .css
        ///	.gif
        /// .png 
        /// .jpg
        /// .jpeg
        /// .js
        /// .axd
        /// .ashx
        /// </remarks>
        bool IsStaticResource(HttpRequest request);

        /// <summary>
        /// Mapa virual del path hacia el del disco fisico 
        /// </summary>
        /// <param name="path">El path para el mapa. E.g. "~/bin"</param>
        /// <returns>El path. fisico E.g. "c:\inetpub\wwwroot\bin"</returns>
        string MapPath(string path);


        /// <summary>
        /// Modifica la cadena de query
        /// </summary>
        /// <param name="url">Url a modificar</param>
        /// <param name="queryStringModification">Cadena de Querycon modificaciones</param>
        /// <param name="anchor">Anchor</param>
        /// <returns>Nueva url</returns>
        string ModifyQueryString(string url, string queryStringModification, string anchor);

        /// <summary>
        /// Remueve una cadena de query de la url
        /// </summary>
        /// <param name="url">Url a modificar</param>
        /// <param name="queryString">Cadena de query a removerse</param>
        /// <returns>Nueva url</returns>
        string RemoveQueryString(string url, string queryString);

        /// <summary>
        /// Retorna el valor de la query por nombre
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">Nombre del parametro</param>
        /// <returns>Query string value</returns>
        T QueryString<T>(string name);

        /// <summary>
        /// Reiniciar el dominio de la aplicaicon 
        /// </summary>
        /// <param name="makeRedirect">Valor que indica cual deberia ser modo de redireccionarnos despues de reiniciar</param>
        /// 
        /// <param name="redirectUrl">Redireccionar URL; Vadena vacia si se quiere redireccionar de la paguina corriente</param>
        void RestartAppDomain(bool makeRedirect = false, string redirectUrl = "");

        /// <summary>
        /// Establece un valor que indica si el cliente esta comenzado a redirecciona a una nueva aplicacion 
        /// </summary>
        bool IsRequestBeingRedirected { get; }

        /// <summary>
        /// Obtiene o establece un valor que indica si el cliente  esta comenzado a redirecciona a una nueva localiacin usando un POST 
        /// </summary>
        bool IsPostBeingDone { get; set; }
    }
}