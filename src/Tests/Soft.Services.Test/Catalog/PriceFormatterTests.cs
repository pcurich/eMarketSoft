﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using Rhino.Mocks;
using Soft.Core;
using Soft.Core.Caching;
using Soft.Core.Data;
using Soft.Core.Domain.Directory;
using Soft.Core.Domain.Localization;
using Soft.Core.Domain.Tax;
using Soft.Core.Plugins;
using Soft.Services.Catalog;
using Soft.Services.Directory;
using Soft.Services.Localization;
using Soft.Services.Stores;
using Soft.Test;

namespace Soft.Services.Test.Catalog
{
    [TestFixture]
    public class PriceFormatterTests : ServiceTest
    {
        private IRepository<Currency> _currencyRepo;
        private IStoreMappingService _storeMappingService;
        private ICurrencyService _currencyService;
        private CurrencySettings _currencySettings;
        private IWorkContext _workContext;
        private ILocalizationService _localizationService;
        private TaxSettings _taxSettings;
        private IPriceFormatter _priceFormatter;

        [SetUp]
        public new void SetUp()
        {
            var cacheManager = new SoftNullCache();

            _workContext = null;

            _currencySettings = new CurrencySettings();

            var currency1 = new Currency
            {
                Id = 1,
                Name = "Euro",
                CurrencyCode = "EUR",
                DisplayLocale = "",
                CustomFormatting = "€0.00",
                DisplayOrder = 1,
                Published = true,
                CreatedOnUtc = DateTime.UtcNow,
                UpdatedOnUtc = DateTime.UtcNow
            };

            var currency2 = new Currency
            {
                Id = 1,
                Name = "US Dollar",
                CurrencyCode = "USD",
                DisplayLocale = "en-US",
                CustomFormatting = "",
                DisplayOrder = 2,
                Published = true,
                CreatedOnUtc = DateTime.UtcNow,
                UpdatedOnUtc = DateTime.UtcNow
            };

            _currencyRepo = MockRepository.GenerateMock<IRepository<Currency>>();
            _currencyRepo.Expect(x => x.Table).Return(new List<Currency> { currency1, currency2 }.AsQueryable());

            _storeMappingService = MockRepository.GenerateMock<IStoreMappingService>();

            var pluginFinder = new PluginFinder();
            _currencyService = new CurrencyService(cacheManager, _currencyRepo, _storeMappingService,
                _currencySettings, pluginFinder, null);

            _taxSettings = new TaxSettings();

            _localizationService = MockRepository.GenerateMock<ILocalizationService>();
            _localizationService.Expect(x => x.GetResource("Products.InclTaxSuffix", 1, false)).Return("{0} incl tax");
            _localizationService.Expect(x => x.GetResource("Products.ExclTaxSuffix", 1, false)).Return("{0} excl tax");

            _priceFormatter = new PriceFormatter(_workContext, _currencyService, _localizationService,
                _taxSettings, _currencySettings);
        }

        [Test]
        public void Can_formatPrice_with_custom_currencyFormatting()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            var currency = new Currency
            {
                Id = 1,
                Name = "Euro",
                CurrencyCode = "EUR",
                DisplayLocale = "",
                CustomFormatting = "€0.00"
            };

            var language = new Language
            {
                Id = 1,
                Name = "English",
                LanguageCulture = "en-US"
            };
            _priceFormatter.FormatPrice(1234.5M, false, currency, language, false, false).ShouldEqual("€1234.50");
        }

        [Test]
        public void Can_formatPrice_with_distinct_currencyDisplayLocale()
        {
            var usdCurrency = new Currency
            {
                Id = 1,
                Name = "US Dollar",
                CurrencyCode = "USD",
                DisplayLocale = "en-US",
            };
            var gbpCurrency = new Currency
            {
                Id = 2,
                Name = "great british pound",
                CurrencyCode = "GBP",
                DisplayLocale = "en-GB",
            };

            var language = new Language
            {
                Id = 1,
                Name = "English",
                LanguageCulture = "en-US"
            };
            _priceFormatter.FormatPrice(1234.5M, false, usdCurrency, language, false, false).ShouldEqual("$1,234.50");
            _priceFormatter.FormatPrice(1234.5M, false, gbpCurrency, language, false, false).ShouldEqual("£1,234.50");
        }

        [Test]
        public void Can_formatPrice_with_showTax()
        {
            var currency = new Currency
            {
                Id = 1,
                Name = "US Dollar",
                CurrencyCode = "USD",
                DisplayLocale = "en-US",
            };
            var language = new Language
            {
                Id = 1,
                Name = "English",
                LanguageCulture = "en-US"
            };
            _priceFormatter.FormatPrice(1234.5M, false, currency, language, true, true).ShouldEqual("$1,234.50 incl tax");
            _priceFormatter.FormatPrice(1234.5M, false, currency, language, false, true).ShouldEqual("$1,234.50 excl tax");

        }


        [Test]
        public void Can_formatPrice_with_showCurrencyCode()
        {
            var currency = new Currency
            {
                Id = 1,
                Name = "US Dollar",
                CurrencyCode = "USD",
                DisplayLocale = "en-US",
            };
            var language = new Language
            {
                Id = 1,
                Name = "English",
                LanguageCulture = "en-US"
            };

            _currencySettings.DisplayCurrencyLabel = true;
            _priceFormatter.FormatPrice(1234.5M, true, currency, language, false, false).ShouldEqual("$1,234.50 (USD)");
            _priceFormatter.FormatPrice(1234.5M, false, currency, language, false, false).ShouldEqual("$1,234.50");

            _currencySettings.DisplayCurrencyLabel = false;
            _priceFormatter.FormatPrice(1234.5M, true, currency, language, false, false).ShouldEqual("$1,234.50");
        }
    }
}