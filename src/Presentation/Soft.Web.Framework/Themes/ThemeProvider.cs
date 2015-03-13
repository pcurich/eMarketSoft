using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Soft.Core;
using Soft.Core.Configuration;

namespace Soft.Web.Framework.Themes
{
    public partial class ThemeProvider : IThemeProvider
    {
        #region Campos

        private readonly IList<ThemeConfiguration> _themeConfigurations;
        private readonly string _basePath;

        #endregion

        #region Ctr

        public ThemeProvider(SoftConfig nopConfig, IWebHelper webHelper)
        {
            _themeConfigurations = new List<ThemeConfiguration>();
            _basePath = webHelper.MapPath(nopConfig.ThemeBasePath);
            LoadConfigurations();
        }

        #endregion

        #region IThemeProvider

        public ThemeConfiguration GetThemeConfiguration(string themeName)
        {
            return _themeConfigurations
                .SingleOrDefault(x => x.ThemeName.Equals(themeName, StringComparison.InvariantCultureIgnoreCase));
        }

        public IList<ThemeConfiguration> GetThemeConfigurations()
        {
            return _themeConfigurations;
        }

        public bool ThemeConfigurationExists(string themeName)
        {
            return GetThemeConfigurations()
                .Any(configuration => configuration.ThemeName.Equals(themeName, StringComparison.InvariantCultureIgnoreCase));
        }

        #endregion

        #region Util

        private void LoadConfigurations()
        {
            //TODO:Use IFileStorage?
            foreach (var themeName in Directory.GetDirectories(_basePath))
            {
                var configuration = CreateThemeConfiguration(themeName);
                if (configuration != null)
                {
                    _themeConfigurations.Add(configuration);
                }
            }
        }

        private ThemeConfiguration CreateThemeConfiguration(string themePath)
        {
            var themeDirectory = new DirectoryInfo(themePath);
            var themeConfigFile = new FileInfo(Path.Combine(themeDirectory.FullName, "theme.config"));

            if (themeConfigFile.Exists)
            {
                var doc = new XmlDocument();
                doc.Load(themeConfigFile.FullName);
                return new ThemeConfiguration(themeDirectory.Name, themeDirectory.FullName, doc);
            }

            return null;
        }

        #endregion
    }
}