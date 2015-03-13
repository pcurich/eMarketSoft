using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace Soft.Core.Plugins.Official
{
    public class OfficialFeedManager : IOfficialFeedManager
    {
        /// <summary>
        /// Retorna las categorias
        /// </summary>
        /// <returns>Resultado</returns>
        public IList<OfficialFeedCategory> GetCategories()
        {
            var result = new List<OfficialFeedCategory>();
            const string feedUrl = "http://www.nopcommerce.com/extensionsxml.aspx?getCategories=1";

            //Espero 5 segundos
            var request = WebRequest.Create(feedUrl);
            request.Timeout = 5000;
            using (var response = request.GetResponse())
            {
                var dataStream = response.GetResponseStream();
                var reader = new StreamReader(dataStream);
                var responseFromServer = reader.ReadToEnd();
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseFromServer);

                foreach (XmlNode node in xmlDoc.SelectNodes(@"//categories/category"))
                {
                    var id = node.SelectNodes(@"id")[0].InnerText;
                    var parentCategoryId = node.SelectNodes(@"parentCategoryId")[0].InnerText;
                    var name = node.SelectNodes(@"name")[0].InnerText;
                    result.Add(new OfficialFeedCategory
                    {
                        Id = int.Parse(id),
                        ParentCategoryId = int.Parse(parentCategoryId),
                        Name = name,
                    });
                }
            }
            return result;
        }

        public virtual IList<OfficialFeedVersion> GetVersions()
        {
            var result = new List<OfficialFeedVersion>();

            const string feedUrl = "http://www.nopcommerce.com/extensionsxml.aspx?getVersions=1";

            //specify timeout (5 secs)
            var request = WebRequest.Create(feedUrl);
            request.Timeout = 5000;
            using (var response = request.GetResponse())
            {
                var dataStream = response.GetResponseStream();
                var reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseFromServer);

                foreach (XmlNode node in xmlDoc.SelectNodes(@"//versions/version"))
                {
                    var id = node.SelectNodes(@"id")[0].InnerText;
                    var name = node.SelectNodes(@"name")[0].InnerText;
                    result.Add(new OfficialFeedVersion
                    {
                        Id = int.Parse(id),
                        Name = name,
                    });
                }
            }
            return result;
        }

        public IPagedList<OfficialFeedPlugin> GetAllPlugins(int categoryId = 0, int versionId = 0, int price = 0,
            string searchTerm = "", int pageIndex = 0,
            int pageSize = Int32.MaxValue)
        {
            var list = new List<OfficialFeedPlugin>();

            //pageSize parameter is currently ignored by official site (set to 15)
            var feedUrl =
                string.Format(
                    "http://www.nopcommerce.com/extensionsxml.aspx?category={0}&version={1}&price={2}&pageIndex={3}&pageSize={4}&searchTerm={5}",
                    categoryId, versionId, price, pageIndex, pageSize, HttpUtility.UrlEncode(searchTerm));

            //specify timeout (5 secs)
            var request = WebRequest.Create(feedUrl);
            request.Timeout = 5000;
            int totalRecords = 0;
            using (var response = request.GetResponse())
            {
                var dataStream = response.GetResponseStream();
                var reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseFromServer);

                foreach (XmlNode node in xmlDoc.SelectNodes(@"//extensions/extension"))
                {
                    var name = node.SelectNodes(@"name")[0].InnerText;
                    var url = node.SelectNodes(@"url")[0].InnerText;
                    var pictureUrl = node.SelectNodes(@"picture")[0].InnerText;
                    var category = node.SelectNodes(@"category")[0].InnerText;
                    var versions = node.SelectNodes(@"versions")[0].InnerText;
                    var priceValue = node.SelectNodes(@"price")[0].InnerText;
                    list.Add(new OfficialFeedPlugin
                    {
                        Name = name,
                        Url = url,
                        PictureUrl = pictureUrl,
                        Category = category,
                        SupportedVersions = versions,
                        Price = priceValue,
                    });
                }

                //total records
                foreach (XmlNode node in xmlDoc.SelectNodes(@"//totalRecords"))
                {
                    totalRecords = int.Parse(node.SelectNodes(@"value")[0].InnerText);
                }
            }

            return new PagedList<OfficialFeedPlugin>(list, pageIndex, pageSize, totalRecords);
        }
    }
}