using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;

namespace Soft.Core.Data
{
    public partial class DataSettingsManager
    {
        protected const char Separator = ':';
        protected const string Filename = "Settings.txt";

        /// <summary>
        /// Mapas de una ruta virtual a una fisica de un directorio
        /// </summary>
        /// <param name="path">La ruta por ejemplo "~/bin"</param>
        /// <returns>La ruta fisica, por ejemplo "c:\inetpub\wwwroot\bin"</returns>
        protected virtual string MapPath(string path)
        {
            if (HostingEnvironment.IsHosted)
            {
                //hosted
                return HostingEnvironment.MapPath(path);
            }
            //No host por ejemplo un test 
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            return Path.Combine(baseDirectory, path);
        }

        protected virtual DataSettings ParseSettings(string text)
        {
            var shellSettings = new DataSettings();
            if (String.IsNullOrEmpty(text))
                return shellSettings;

            //Manera antigua de lectura. Esto permite un inesperado comportamiento cuando los usuarios de FTP transfieren archivos como ASCII (\r\n becomes \n)
            //var settings = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var settings = new List<string>();
            using (var reader = new StringReader(text))
            {
                string str;
                while ((str = reader.ReadLine()) != null)
                    settings.Add(str);
            }

            foreach (var setting in settings)
            {
                var separatorIndex = setting.IndexOf(Separator);
                if (separatorIndex == -1)
                {
                    continue;
                }
                var key = setting.Substring(0, separatorIndex).Trim();
                var value = setting.Substring(separatorIndex + 1).Trim();

                switch (key)
                {
                    case "DataProvider":
                        shellSettings.DataProvider = value;
                        break;
                    case "DataConnectionString":
                        shellSettings.DataConnectionString = value;
                        break;
                    default:
                        shellSettings.RawDataSettings.Add(key, value);
                        break;
                }
            }

            return shellSettings;
        }

        protected virtual string ComposeSettings(DataSettings settings)
        {
            if (settings == null)
                return "";

            return string.Format("DataProvider: {0}{2}DataConnectionString: {1}{2}",
                settings.DataProvider,
                settings.DataConnectionString,
                Environment.NewLine
                );
        }

        /// <summary>
        /// Carga Configuracion
        /// </summary>
        /// <param name="filePath">si se pasa null para utilizar el default del archivo de configuracion</param>
        /// <returns></returns>
        public virtual DataSettings LoadSettings(string filePath = null)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                //use webHelper.MapPath instead of HostingEnvironment.MapPath which is not available in unit tests
                filePath = Path.Combine(MapPath("~/App_Data/"), Filename);
            }
            if (!File.Exists(filePath))
                return new DataSettings();

            var text = File.ReadAllText(filePath);
            return ParseSettings(text);
        }

        public virtual void SaveSettings(DataSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");

            //use webHelper.MapPath instead of HostingEnvironment.MapPath which is not available in unit tests
            var filePath = Path.Combine(MapPath("~/App_Data/"), Filename);
            if (!File.Exists(filePath))
            {
                using (File.Create(filePath))
                {
                    //Se usa 'using' para cerrar el archivo despues de haberse creado
                }
            }

            var text = ComposeSettings(settings);
            File.WriteAllText(filePath, text);
        }
    }
}