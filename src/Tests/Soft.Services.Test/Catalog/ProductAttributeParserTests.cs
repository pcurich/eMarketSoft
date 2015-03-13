using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Soft.Core;
using Soft.Core.Caching;
using Soft.Core.Data;
using Soft.Core.Domain.Catalog;
using Soft.Core.Domain.Customers;
using Soft.Core.Domain.Localization;
using Soft.Core.Domain.Orders;
using Soft.Services.Catalog;
using Soft.Services.Directory;
using Soft.Services.Events;
using Soft.Services.Localization;
using Soft.Services.Media;
using Soft.Services.Tax;
using Soft.Test;

namespace Soft.Services.Test.Catalog
{
    [TestFixture]
    public class ProductAttributeParserTests : ServiceTest
    {
        private IRepository<ProductAttribute> _productAttributeRepo;
        private IRepository<ProductAttributeMapping> _productAttributeMappingRepo;
        private IRepository<ProductAttributeCombination> _productAttributeCombinationRepo;
        private IRepository<ProductAttributeValue> _productAttributeValueRepo;
        private IProductAttributeService _productAttributeService;
        private IProductAttributeParser _productAttributeParser;
        private IEventPublisher _eventPublisher;

        private IWorkContext _workContext;
        private ICurrencyService _currencyService;
        private ILocalizationService _localizationService;
        private ITaxService _taxService;
        private IPriceFormatter _priceFormatter;
        private IPriceCalculationService _priceCalculationService;
        private IDownloadService _downloadService;
        private IWebHelper _webHelper;
        private ShoppingCartSettings _shoppingCartSettings;
        private IProductAttributeFormatter _productAttributeFormatter;

        private ProductAttribute _pa1, _pa2, _pa3;
        private ProductAttributeMapping _pam11, _pam21, _pam31;
        private ProductAttributeValue _pav11, _pav12, _pav21, _pav22;

        [SetUp]
        public new void SetUp()
        {
            #region Test data

            //color (dropdownlist)
            _pa1 = new ProductAttribute
            {
                Id = 1,
                Name = "Color",
            };
            _pam11 = new ProductAttributeMapping
            {
                Id = 11,
                ProductId = 1,
                TextPrompt = "Select color:",
                IsRequired = true,
                AttributeControlType = AttributeControlType.DropdownList,
                DisplayOrder = 1,
                ProductAttribute = _pa1,
                ProductAttributeId = _pa1.Id
            };
            _pav11 = new ProductAttributeValue
            {
                Id = 11,
                Name = "Green",
                DisplayOrder = 1,
                ProductAttributeMapping = _pam11,
                ProductAttributeMappingId = _pam11.Id
            };
            _pav12 = new ProductAttributeValue
            {
                Id = 12,
                Name = "Red",
                DisplayOrder = 2,
                ProductAttributeMapping = _pam11,
                ProductAttributeMappingId = _pam11.Id
            };
            _pam11.ProductAttributeValues.Add(_pav11);
            _pam11.ProductAttributeValues.Add(_pav12);

            //custom option (checkboxes)
            _pa2 = new ProductAttribute
            {
                Id = 2,
                Name = "Some custom option",
            };
            _pam21 = new ProductAttributeMapping
            {
                Id = 21,
                ProductId = 1,
                TextPrompt = "Select at least one option:",
                IsRequired = true,
                AttributeControlType = AttributeControlType.Checkboxes,
                DisplayOrder = 2,
                ProductAttribute = _pa2,
                ProductAttributeId = _pa2.Id
            };
            _pav21 = new ProductAttributeValue
            {
                Id = 21,
                Name = "Option 1",
                DisplayOrder = 1,
                ProductAttributeMapping = _pam21,
                ProductAttributeMappingId = _pam21.Id
            };
            _pav22 = new ProductAttributeValue
            {
                Id = 22,
                Name = "Option 2",
                DisplayOrder = 2,
                ProductAttributeMapping = _pam21,
                ProductAttributeMappingId = _pam21.Id
            };
            _pam21.ProductAttributeValues.Add(_pav21);
            _pam21.ProductAttributeValues.Add(_pav22);

            //custom text
            _pa3 = new ProductAttribute
            {
                Id = 3,
                Name = "Custom text",
            };
            _pam31 = new ProductAttributeMapping
            {
                Id = 31,
                ProductId = 1,
                TextPrompt = "Enter custom text:",
                IsRequired = true,
                AttributeControlType = AttributeControlType.TextBox,
                DisplayOrder = 1,
                ProductAttribute = _pa3,
                ProductAttributeId = _pa3.Id
            };


            #endregion

            _productAttributeRepo = MockRepository.GenerateMock<IRepository<ProductAttribute>>();
            _productAttributeRepo.Expect(x => x.Table).Return(new List<ProductAttribute> { _pa1, _pa2, _pa3 }.AsQueryable());
            _productAttributeRepo.Expect(x => x.GetById(_pa1.Id)).Return(_pa1);
            _productAttributeRepo.Expect(x => x.GetById(_pa2.Id)).Return(_pa2);
            _productAttributeRepo.Expect(x => x.GetById(_pa3.Id)).Return(_pa3);

            _productAttributeMappingRepo = MockRepository.GenerateMock<IRepository<ProductAttributeMapping>>();
            _productAttributeMappingRepo.Expect(x => x.Table).Return(new List<ProductAttributeMapping> { _pam11, _pam21, _pam31 }.AsQueryable());
            _productAttributeMappingRepo.Expect(x => x.GetById(_pam11.Id)).Return(_pam11);
            _productAttributeMappingRepo.Expect(x => x.GetById(_pam21.Id)).Return(_pam21);
            _productAttributeMappingRepo.Expect(x => x.GetById(_pam31.Id)).Return(_pam31);

            _productAttributeCombinationRepo = MockRepository.GenerateMock<IRepository<ProductAttributeCombination>>();
            _productAttributeCombinationRepo.Expect(x => x.Table).Return(new List<ProductAttributeCombination>().AsQueryable());

            _productAttributeValueRepo = MockRepository.GenerateMock<IRepository<ProductAttributeValue>>();
            _productAttributeValueRepo.Expect(x => x.Table).Return(new List<ProductAttributeValue> { _pav11, _pav12, _pav21, _pav22 }.AsQueryable());
            _productAttributeValueRepo.Expect(x => x.GetById(_pav11.Id)).Return(_pav11);
            _productAttributeValueRepo.Expect(x => x.GetById(_pav12.Id)).Return(_pav12);
            _productAttributeValueRepo.Expect(x => x.GetById(_pav21.Id)).Return(_pav21);
            _productAttributeValueRepo.Expect(x => x.GetById(_pav22.Id)).Return(_pav22);

            _eventPublisher = MockRepository.GenerateMock<IEventPublisher>();
            _eventPublisher.Expect(x => x.Publish(Arg<object>.Is.Anything));

            var cacheManager = new SoftNullCache();

            _productAttributeService = new ProductAttributeService(cacheManager,
                _productAttributeRepo,
                _productAttributeMappingRepo,
                _productAttributeCombinationRepo,
                _productAttributeValueRepo,
                _eventPublisher);

            _productAttributeParser = new ProductAttributeParser(_productAttributeService);
            _priceCalculationService = MockRepository.GenerateMock<IPriceCalculationService>();

            var workingLanguage = new Language();
            _workContext = MockRepository.GenerateMock<IWorkContext>();
            _workContext.Expect(x => x.WorkingLanguage).Return(workingLanguage);

            _currencyService = MockRepository.GenerateMock<ICurrencyService>();
            _localizationService = MockRepository.GenerateMock<ILocalizationService>();
            _localizationService.Expect(x => x.GetResource("GiftCardAttribute.For.Virtual")).Return("For: {0} <{1}>");
            _localizationService.Expect(x => x.GetResource("GiftCardAttribute.From.Virtual")).Return("From: {0} <{1}>");
            _localizationService.Expect(x => x.GetResource("GiftCardAttribute.For.Physical")).Return("For: {0}");
            _localizationService.Expect(x => x.GetResource("GiftCardAttribute.From.Physical")).Return("From: {0}");
            
            _taxService = MockRepository.GenerateMock<ITaxService>();
            _priceFormatter = MockRepository.GenerateMock<IPriceFormatter>();
            _downloadService = MockRepository.GenerateMock<IDownloadService>();
            _webHelper = MockRepository.GenerateMock<IWebHelper>();
            _shoppingCartSettings = MockRepository.GenerateMock<ShoppingCartSettings>();

            _productAttributeFormatter = new ProductAttributeFormatter(_workContext,
                _productAttributeService,
                _productAttributeParser,
                _currencyService,
                _localizationService,
                _taxService,
                _priceFormatter,
                _downloadService,
                _webHelper,
                _priceCalculationService,
                _shoppingCartSettings);

        }

        [Test]
        public void Can_add_and_parse_productAttributes()
        {
            var attributes = "";
            //color: green
            attributes = _productAttributeParser.AddProductAttribute(attributes, _pam11, _pav11.Id.ToString());
            //custom option: option 1, option 2
            attributes = _productAttributeParser.AddProductAttribute(attributes, _pam21, _pav21.Id.ToString());
            attributes = _productAttributeParser.AddProductAttribute(attributes, _pam21, _pav22.Id.ToString());
            //custom text
            attributes = _productAttributeParser.AddProductAttribute(attributes, _pam31, "Some custom text goes here");

            var parsedAttributeValues = _productAttributeParser.ParseProductAttributeValues(attributes);
            parsedAttributeValues.Contains(_pav11).ShouldEqual(true);
            parsedAttributeValues.Contains(_pav12).ShouldEqual(false);
            parsedAttributeValues.Contains(_pav21).ShouldEqual(true);
            parsedAttributeValues.Contains(_pav22).ShouldEqual(true);
            parsedAttributeValues.Contains(_pav22).ShouldEqual(true);

            var parsedValues = _productAttributeParser.ParseValues(attributes, _pam31.Id);
            parsedValues.Count.ShouldEqual(1);
            parsedValues.Contains("Some custom text goes here").ShouldEqual(true);
            parsedValues.Contains("Some other custom text").ShouldEqual(false);
        }

        [Test]
        public void Can_add_and_parse_giftCardAttributes()
        {
            string attributes = "";
            attributes = _productAttributeParser.AddGiftCardAttribute(attributes,
                "recipientName 1", "recipientEmail@gmail.com",
                "senderName 1", "senderEmail@gmail.com", "custom message");

            string recipientName, recipientEmail, senderName, senderEmail, giftCardMessage;
            _productAttributeParser.GetGiftCardAttribute(attributes,
                out recipientName,
                out recipientEmail,
                out senderName,
                out senderEmail,
                out giftCardMessage);
            recipientName.ShouldEqual("recipientName 1");
            recipientEmail.ShouldEqual("recipientEmail@gmail.com");
            senderName.ShouldEqual("senderName 1");
            senderEmail.ShouldEqual("senderEmail@gmail.com");
            giftCardMessage.ShouldEqual("custom message");
        }

        [Test]
        public void Can_render_virtual_gift_cart()
        {
            string attributes = _productAttributeParser.AddGiftCardAttribute("",
                "recipientName 1", "recipientEmail@gmail.com",
                "senderName 1", "senderEmail@gmail.com", "custom message");

            var product = new Product
            {
                IsGiftCard = true,
                GiftCardType = GiftCardType.Virtual,
            };
            var customer = new Customer();
            var formattedAttributes = _productAttributeFormatter.FormatAttributes(product,
                attributes, customer, "<br />", false, false, true, true);
            formattedAttributes.ShouldEqual("From: senderName 1 <senderEmail@gmail.com><br />For: recipientName 1 <recipientEmail@gmail.com>");
        }

        [Test]
        public void Can_render_physical_gift_cart()
        {
            string attributes = _productAttributeParser.AddGiftCardAttribute("",
                "recipientName 1", "recipientEmail@gmail.com",
                "senderName 1", "senderEmail@gmail.com", "custom message");

            var product = new Product
            {
                IsGiftCard = true,
                GiftCardType = GiftCardType.Physical,
            };
            var customer = new Customer();
            string formattedAttributes = _productAttributeFormatter.FormatAttributes(product,
                attributes, customer, "<br />", false, false, true, true);
            formattedAttributes.ShouldEqual("From: senderName 1<br />For: recipientName 1");
        }

        [Test]
        public void Can_render_attributes_withoutPrices()
        {
            string attributes = "";
            //color: green
            attributes = _productAttributeParser.AddProductAttribute(attributes, _pam11, _pav11.Id.ToString());
            //custom option: option 1, option 2
            attributes = _productAttributeParser.AddProductAttribute(attributes, _pam21, _pav21.Id.ToString());
            attributes = _productAttributeParser.AddProductAttribute(attributes, _pam21, _pav22.Id.ToString());
            //custom text
            attributes = _productAttributeParser.AddProductAttribute(attributes, _pam31, "Some custom text goes here");

            //gift card attributes
            attributes = _productAttributeParser.AddGiftCardAttribute(attributes,
                "recipientName 1", "recipientEmail@gmail.com",
                "senderName 1", "senderEmail@gmail.com", "custom message");

            var product = new Product
            {
                IsGiftCard = true,
                GiftCardType = GiftCardType.Virtual,
            };
            var customer = new Customer();
            string formattedAttributes = _productAttributeFormatter.FormatAttributes(product,
                attributes, customer, "<br />", false, false, true, true);
            formattedAttributes.ShouldEqual("Color: Green<br />Some custom option: Option 1<br />Some custom option: Option 2<br />Color: Some custom text goes here<br />From: senderName 1 <senderEmail@gmail.com><br />For: recipientName 1 <recipientEmail@gmail.com>");
        }
    }
}
