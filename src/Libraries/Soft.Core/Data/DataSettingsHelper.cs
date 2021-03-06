﻿using System;

namespace Soft.Core.Data
{
    public partial class DataSettingsHelper
    {
        private static bool? _databaseIsInstalled;

        public static bool DatabaseIsInstalled()
        {
            if (_databaseIsInstalled.HasValue)
                return _databaseIsInstalled.Value;

            var manager = new DataSettingsManager();
            var settings = manager.LoadSettings();
            _databaseIsInstalled = settings != null && !String.IsNullOrEmpty(settings.DataConnectionString);

            return _databaseIsInstalled.Value;
        }

        /// <summary>
        /// Reinicia el cache para que vuelva a crear un nuevo objeto <see cref="DataSettingsManager"/>
        /// </summary>
        public static void ResetCache()
        {
            _databaseIsInstalled = null;
        }
    }
}