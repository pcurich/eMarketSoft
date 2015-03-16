using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Soft.Core.Plugins
{
    /// <summary>
    /// Parseador de archivos a plugins
    /// </summary>
    public static class PluginFileParser
    {
        public static IList<string> ParseInstalledPluginsFile(string filePath)
        {
            var lines = new List<string>();

            //Lee y parsea el archivo
            if (!File.Exists(filePath))
                return lines;

            var text = File.ReadAllText(filePath);
            if (String.IsNullOrEmpty(text))
                return lines;

            //Antigua manera de leer un archivo Esto conduce a un inesperado comportamiento cuando x ftp se transfiere
            //los archivos. Estos archivos estan en ASCII  (\r\n cambian a \n).
            //var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            using (var reader = new StringReader(text))
            {
                string str;
                while ((str = reader.ReadLine()) != null)
                {
                    if (String.IsNullOrWhiteSpace(str))
                        continue;
                    lines.Add(str.Trim());
                }
            }
            return lines;
        }

        public static void SaveInstalledPluginsFile(IList<String> pluginSystemNames, string filePath)
        {
            var result = pluginSystemNames.Aggregate("",
                (current, sn) => current + string.Format("{0}{1}", sn, Environment.NewLine));
            File.WriteAllText(filePath, result);
        }

        public static PluginDescriptor ParsePluginDescriptionFile(string filePath)
        {
            var descriptor = new PluginDescriptor();
            var text = File.ReadAllText(filePath);
            if (String.IsNullOrEmpty(text))
                return descriptor;

            var settings = new List<string>();
            using (var reader = new StringReader(text))
            {
                string str;
                while ((str = reader.ReadLine()) != null)
                {
                    if (String.IsNullOrWhiteSpace(str))
                        continue;
                    settings.Add(str.Trim());
                }
            }

            //Antigua manera de leer un archivo Esto conduce a un inesperado comportamiento cuando x ftp se transfiere
            //los archivos. Estos archivos estan en ASCII  (\r\n cambian a \n).
            //var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var setting in settings)
            {
                var separatorIndex = setting.IndexOf(':');
                if (separatorIndex == -1)
                {
                    continue;
                }
                var key = setting.Substring(0, separatorIndex).Trim();
                var value = setting.Substring(separatorIndex + 1).Trim();

                switch (key)
                {
                    case "Group":
                        descriptor.Group = value;
                        break;
                    case "FriendlyName":
                        descriptor.FriendlyName = value;
                        break;
                    case "SystemName":
                        descriptor.SystemName = value;
                        break;
                    case "Version":
                        descriptor.Version = value;
                        break;
                    case "SupportedVersions":
                    {
                        //parse supported versions
                        descriptor.SupportedVersions = value.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                            .Select(x => x.Trim())
                            .ToList();
                    }
                        break;
                    case "Author":
                        descriptor.Author = value;
                        break;
                    case "DisplayOrder":
                    {
                        int displayOrder;
                        int.TryParse(value, out displayOrder);
                        descriptor.DisplayOrder = displayOrder;
                    }
                        break;
                    case "FileName":
                        descriptor.PluginFileName = value;
                        break;
                    case "LimitedToStores":
                    {
                        //parse list of store IDs
                        foreach (var str1 in value.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                            .Select(x => x.Trim()))
                        {
                            int storeId;
                            if (int.TryParse(str1, out storeId))
                            {
                                descriptor.LimitedToStores.Add(storeId);
                            }
                        }
                    }
                        break;
                }
            }
            return descriptor;
        }

        public static void SavePluginDescriptionFile(PluginDescriptor plugin)
        {
            if (plugin == null)
                throw new ArgumentException("plugin");

            //Obtiene el Description.txt file path
            if (plugin.OriginalAssemblyFile == null)
                throw new Exception(string.Format("No se puede leer el ensamblado original para el {0} plugin.",
                    plugin.SystemName));


            if (plugin.OriginalAssemblyFile.Directory == null) 
                return;

            var filePath = Path.Combine(plugin.OriginalAssemblyFile.Directory.FullName, "Description.txt");
            if (!File.Exists(filePath))
                throw new Exception(string.Format("La descripcion para el plugin {0} con archivo {1} no existe",
                    plugin.SystemName, filePath));


            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("Group", plugin.Group));
            keyValues.Add(new KeyValuePair<string, string>("FriendlyName", plugin.FriendlyName));
            keyValues.Add(new KeyValuePair<string, string>("SystemName", plugin.SystemName));
            keyValues.Add(new KeyValuePair<string, string>("Version", plugin.Version));
            keyValues.Add(new KeyValuePair<string, string>("SupportedVersions",string.Join(",", plugin.SupportedVersions)));
            keyValues.Add(new KeyValuePair<string, string>("Author", plugin.Author));
            keyValues.Add(new KeyValuePair<string, string>("DisplayOrder",plugin.DisplayOrder.ToString(CultureInfo.InvariantCulture)));
            keyValues.Add(new KeyValuePair<string, string>("FileName", plugin.PluginFileName));
            if (plugin.LimitedToStores.Count > 0)
            {
                var storeList = "";
                for (int i = 0; i < plugin.LimitedToStores.Count; i++)
                {
                    storeList += plugin.LimitedToStores[i];
                    if (i != plugin.LimitedToStores.Count - 1)
                        storeList += ",";
                }
                keyValues.Add(new KeyValuePair<string, string>("Limitado a tiendas", storeList));
            }

            var sb = new StringBuilder();
            for (int i = 0; i < keyValues.Count; i++)
            {
                var key = keyValues[i].Key;
                var value = keyValues[i].Value;
                sb.AppendFormat("{0}: {1}", key, value);
                if (i != keyValues.Count - 1)
                    sb.Append(Environment.NewLine);
            }
            //Guarda el archivo
            File.WriteAllText(filePath, sb.ToString());
        }
    }
}