using System;
using System.Collections.Generic;
using Soft.Core.Domain.Discounts;
using Soft.Core.Domain.Localization;
using Soft.Core.Domain.Security;
using Soft.Core.Domain.Seo;
using Soft.Core.Domain.Stores;

namespace Soft.Core.Domain.Catalog
{
    public partial class Product : BaseEntity, ILocalizedEntity, ISlugSupported, IAclSupported, IStoreMappingSupported
    {
        private ICollection<ProductCategory> _productCategories;
        private ICollection<ProductManufacturer> _productManufacturers;
        private ICollection<ProductPicture> _productPictures;
        private ICollection<ProductReview> _productReviews;
        private ICollection<ProductSpecificationAttribute> _productSpecificationAttributes;
        private ICollection<ProductTag> _productTags;
        private ICollection<ProductAttributeMapping> _productAttributeMappings;
        private ICollection<ProductAttributeCombination> _productAttributeCombinations;
        private ICollection<TierPrice> _tierPrices;
        private ICollection<Discount> _appliedDiscounts;
        private ICollection<ProductWarehouseInventory> _productWarehouseInventory;

        /// <summary>
        /// Identificador del tipo de producto
        /// </summary>
        public int ProductTypeId { get; set; }

        /// <summary>
        /// Identificador del padre del producto 
        /// Es usado para identificar productos asociados (solo con "grouped" productos)
        /// </summary>
        public int ParentGroupedProductId { get; set; }

        /// <summary>
        /// Si el producto es visible en el catalogo o en los resultados de la busqueda 
        /// Esto es usado cuando el producto es asociado a algun "grupo" 
        /// Esta manera de asociacion de productos se puede acceder/agregar/etc solo desde los detalles del producto
        /// </summary>
        public bool VisibleIndividually { get; set; }

        /// <summary>
        /// Nombre del producto
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Corta descripcion del producto
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Larga descripcion del producto
        /// </summary>
        public string FullDescription { get; set; }

        /// <summary>
        /// Comentarios del adminitraodr
        /// </summary>
        public string AdminComment { get; set; }

        /// <summary>
        /// Identiticador del template de productos
        /// </summary>
        public int ProductTemplateId { get; set; }

        /// <summary>
        /// Identificador del vendedor
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        /// Si se muestra en la pagina de inicio
        /// </summary>
        public bool ShowOnHomePage { get; set; }

        /// <summary>
        /// Meta Keywords
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Meta descripcion
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Meta Titulo
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// Si permite revisiones de los clientes
        /// </summary>
        public bool AllowCustomerReviews { get; set; }

        /// <summary>
        /// Suma del rating (revisiones aprobadas)
        /// </summary>
        public int ApprovedRatingSum { get; set; }

        /// <summary>
        /// Suma del rating (revisiones no aprobadas)
        /// </summary>
        public int NotApprovedRatingSum { get; set; }

        /// <summary>
        /// Total de votos del reating (aprobados)
        /// </summary>
        public int ApprovedTotalReviews { get; set; }

        /// <summary>
        /// Total de votos del reating (no aprobados)
        /// </summary>
        public int NotApprovedTotalReviews { get; set; }

        /// <summary>
        /// Si se esta sugeto a ACL
        /// </summary>
        public bool SubjectToAcl { get; set; }

        /// <summary>
        /// Si se esta limitado a tiendas
        /// </summary>
        public bool LimitedToStores { get; set; }

        /// <summary>
        /// El SKU
        /// </summary>
        public string Sku { get; set; }

        /// <summary>
        /// Parte del numero del proveedor
        /// </summary>
        public string ManufacturerPartNumber { get; set; }

        /// <summary>
        /// Global Trade Item Number (GTIN).
        /// include UPC (en North America), 
        /// EAN (en Europe), 
        /// JAN (en Japan), y ISBN (para books).
        /// </summary>
        public string Gtin { get; set; }

        /// <summary>
        /// Si el producto es un gift card
        /// </summary>
        public bool IsGiftCard { get; set; }

        /// <summary>
        /// Identificador del gift card
        /// </summary>
        public int GiftCardTypeId { get; set; }

        /// <summary>
        /// Si requiere que otros productos se añadan al carrito
        /// (Producto X requiere Producto Y)
        /// </summary>
        public bool RequireOtherProducts { get; set; }

        /// <summary>
        /// Identificadores de productos requeridos separados por comas
        /// </summary>
        public string RequiredProductIds { get; set; }

        /// <summary>
        /// Si el producto es automaticamente añadido al carrito
        /// </summary>
        public bool AutomaticallyAddRequiredProducts { get; set; }

        /// <summary>
        /// Si se puede descargar el producto
        /// </summary>
        public bool IsDownload { get; set; }

        /// <summary>
        /// Identificador de la descarga
        /// </summary>
        public int DownloadId { get; set; }

        /// <summary>
        /// Si puede ser descargable ilimitadamente
        /// </summary>
        public bool UnlimitedDownloads { get; set; }

        /// <summary>
        /// Maximo numero de descargas
        /// </summary>
        public int MaxNumberOfDownloads { get; set; }

        /// <summary>
        /// Numero de dias en que se tiene acceso al archivo
        /// </summary>
        public int? DownloadExpirationDays { get; set; }

        /// <summary>
        /// Tipo de activacion de la descarga
        /// </summary>
        public int DownloadActivationTypeId { get; set; }

        /// <summary>
        /// Si el producto tiene un ejemplo de descarga
        /// </summary>
        public bool HasSampleDownload { get; set; }

        /// <summary>
        /// Identificador del ejemplo de la descarga
        /// </summary>
        public int SampleDownloadId { get; set; }

        /// <summary>
        /// Si se tiene usuario de agradecimiento
        /// </summary>
        public bool HasUserAgreement { get; set; }

        /// <summary>
        /// Licencia de agradecimiento
        /// </summary>
        public string UserAgreementText { get; set; }

        /// <summary>
        /// Si es un producto recurrente
        /// </summary>
        public bool IsRecurring { get; set; }

        /// <summary>
        /// Longitud del ciclo
        /// </summary>
        public int RecurringCycleLength { get; set; }

        /// <summary>
        /// Periodo del ciclo
        /// </summary>
        public int RecurringCyclePeriodId { get; set; }

        /// <summary>
        /// Total de ciclos
        /// </summary>
        public int RecurringTotalCycles { get; set; }

        /// <summary>
        /// Si el producto se alquila
        /// </summary>
        public bool IsRental { get; set; }

        /// <summary>
        ///Longitud del arrendamiento para algunos periodos (Precio para este periodo)
        /// </summary>
        public int RentalPriceLength { get; set; }

        /// <summary>
        /// Periodo de alquiler (Precio para este periodo)
        /// </summary>
        public int RentalPricePeriodId { get; set; }

        /// <summary>
        /// Si se activa el metodo de envio
        /// </summary>
        public bool IsShipEnabled { get; set; }

        /// <summary>
        /// Si se activa el metodo de envio gratis
        /// </summary>
        public bool IsFreeShipping { get; set; }

        /// <summary>
        /// Si este producto podria ser enviado separadamente (cada item)
        /// </summary>
        public bool ShipSeparately { get; set; }

        /// <summary>
        /// Cargos adicionales de envio
        /// </summary>
        public decimal AdditionalShippingCharge { get; set; }

        /// <summary>
        /// IIdentificador de la fecha de entrega
        /// </summary>
        public int DeliveryDateId { get; set; }

        /// <summary>
        /// Si el producto esta libre de impuestos
        /// </summary>
        public bool IsTaxExempt { get; set; }

        /// <summary>
        /// Identificador de la categoria de impuestos
        /// </summary>
        public int TaxCategoryId { get; set; }

        /// <summary>
        /// Si el producto es de telecomunicaciones or broadcasting o servicios electronicos
        /// </summary>
        public bool IsTelecommunicationsOrBroadcastingOrElectronicServices { get; set; }

        /// <summary>
        /// Como se maneja el inventario
        /// </summary>
        public int ManageInventoryMethodId { get; set; }

        /// <summary>
        /// Si multiples almacenes son usados para este producto
        /// </summary>
        public bool UseMultipleWarehouses { get; set; }

        /// <summary>
        /// Identificador del almacen
        /// </summary>
        public int WarehouseId { get; set; }

        /// <summary>
        /// Cantidad en stock
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// Si se muestra el stock disponible
        /// </summary>
        public bool DisplayStockAvailability { get; set; }

        /// <summary>
        /// Si se muestra cantidad de stock
        /// </summary>
        public bool DisplayStockQuantity { get; set; }

        /// <summary>
        /// MInima cantidad de stock
        /// </summary>
        public int MinStockQuantity { get; set; }

        /// <summary>
        /// Identificador de cantidad baja
        /// </summary>
        public int LowStockActivityId { get; set; }

        /// <summary>
        /// Cantidad en el que el admin debe ser informado
        /// </summary>
        public int NotifyAdminForQuantityBelow { get; set; }

        /// <summary>
        /// Identificador de ordenes de back
        /// </summary>
        public int BackorderModeId { get; set; }

        /// <summary>
        /// Si se puede suscribir al back de la tienda
        /// </summary>
        public bool AllowBackInStockSubscriptions { get; set; }

        /// <summary>
        /// Cantidad de ordenes minimas
        /// </summary>
        public int OrderMinimumQuantity { get; set; }

        /// <summary>
        /// Cantidad de ordenes maximas
        /// </summary>
        public int OrderMaximumQuantity { get; set; }

        /// <summary>
        /// lista de cantidades permitidas separadas por comas, null o vcio si ninguna cantidad es permitida
        /// </summary>
        public string AllowedQuantities { get; set; }

        /// <summary>
        /// Si se permite añador al carrito o lista de deseos con solo atributos combinados 
        /// Si existe y si tiene stock mayor a cero
        /// Esta opcion es usada solo cuando se gestiona el inventario para hacer el track del inventario por los atributos del producto
        /// </summary>
        public bool AllowAddingOnlyExistingAttributeCombinations { get; set; }

        /// <summary>
        /// Si se desabiita el boton de comprar
        /// </summary>
        public bool DisableBuyButton { get; set; }

        /// <summary>
        /// Si se desabilita el boton de agregar a lista
        /// </summary>
        public bool DisableWishlistButton { get; set; }

        /// <summary>
        /// Si esta habilitado para pre ordenes
        /// </summary>
        public bool AvailableForPreOrder { get; set; }

        /// <summary>
        /// Fecha de inicio para productos de inicio de pre ordenes
        /// </summary>
        public DateTime? PreOrderAvailabilityStartDateTimeUtc { get; set; }

        /// <summary>
        /// Si se va a llamar para el precio en lugar del precio 
        /// </summary>
        public bool CallForPrice { get; set; }

        /// <summary>
        /// Precio
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Precio anterior
        /// </summary>
        public decimal OldPrice { get; set; }

        /// <summary>
        /// Costo del producto
        /// </summary>
        public decimal ProductCost { get; set; }

        /// <summary>
        /// Precio especial 
        /// </summary>
        public decimal? SpecialPrice { get; set; }

        /// <summary>
        /// Inicio del precio especial 
        /// </summary>
        public DateTime? SpecialPriceStartDateTimeUtc { get; set; }

        /// <summary>
        /// Fin del precio especial
        /// </summary>
        public DateTime? SpecialPriceEndDateTimeUtc { get; set; }

        /// <summary>
        /// So el cliente ingresa el precio
        /// </summary>
        public bool CustomerEntersPrice { get; set; }

        /// <summary>
        /// Precio minimo ingresado por el cliente
        /// </summary>
        public decimal MinimumCustomerEnteredPrice { get; set; }

        /// <summary>
        /// Precio maximo ingresado por el cliente
        /// </summary>
        public decimal MaximumCustomerEnteredPrice { get; set; }

        /// <summary>
        /// Si se tiene nivel del precio configurado
        /// <remarks>T
        /// El mismo si se tiene TierPrices.Count > 0
        /// Se usa para optimizar 
        /// Si es falso no se necesita cargar los rangos de precios
        /// </remarks>
        /// </summary>
        public bool HasTierPrices { get; set; }

        /// <summary>
        /// Si el producto tiene un descuento asignado
        /// <remarks>
        /// El mismo si se tiene AppliedDiscounts.Count > 0
        /// Se usa para optimizar 
        /// Si es falso no se necesita cargar la aplicacion del descuento
        /// </remarks>
        /// </summary>
        public bool HasDiscountsApplied { get; set; }

        /// <summary>
        /// El peso
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// El largo
        /// </summary>
        public decimal Length { get; set; }

        /// <summary>
        /// El ancho
        /// </summary>
        public decimal Width { get; set; }

        /// <summary>
        /// El alto
        /// </summary>
        public decimal Height { get; set; }

        /// <summary>
        /// Desponible desde
        /// </summary>
        public DateTime? AvailableStartDateTimeUtc { get; set; }

        /// <summary>
        /// Disponible hasta
        /// </summary>
        public DateTime? AvailableEndDateTimeUtc { get; set; }

        /// <summary>
        /// Orden para mostrar
        /// Es usado para  cuanso se ordena con asociacion de productos (usado con grouped )
        /// Es usado para  cuanso se ordena en la pagina de inicio
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Si se publica
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Borrado logico
        /// </summary>
        public bool Deleted { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime UpdatedOnUtc { get; set; }


        /// <summary>
        /// Tipo de producto
        /// </summary>
        public ProductType ProductType
        {
            get { return (ProductType) ProductTypeId; }
            set { ProductTypeId = (int) value; }
        }

        /// <summary>
        /// Modo de back ordenes
        /// </summary>
        public BackorderMode BackorderMode
        {
            get { return (BackorderMode) BackorderModeId; }
            set { BackorderModeId = (int) value; }
        }

        /// <summary>
        /// Descarga el tipo de activacion
        /// </summary>
        public DownloadActivationType DownloadActivationType
        {
            get { return (DownloadActivationType) DownloadActivationTypeId; }
            set { DownloadActivationTypeId = (int) value; }
        }

        /// <summary>
        /// Tipo de gift card
        /// </summary>
        public GiftCardType GiftCardType
        {
            get { return (GiftCardType) GiftCardTypeId; }
            set { GiftCardTypeId = (int) value; }
        }

        /// <summary>
        /// Actividad de stock bajo
        /// </summary>
        public LowStockActivity LowStockActivity
        {
            get { return (LowStockActivity) LowStockActivityId; }
            set { LowStockActivityId = (int) value; }
        }

        /// <summary>
        /// Como gestionar el inventario
        /// </summary>
        public ManageInventoryMethod ManageInventoryMethod
        {
            get { return (ManageInventoryMethod) ManageInventoryMethodId; }
            set { ManageInventoryMethodId = (int) value; }
        }

        /// <summary>
        /// Ciclo del periodo para productos recurrentes
        /// </summary>
        public RecurringProductCyclePeriod RecurringCyclePeriod
        {
            get { return (RecurringProductCyclePeriod) RecurringCyclePeriodId; }
            set { RecurringCyclePeriodId = (int) value; }
        }

        /// <summary>
        /// Gets or sets the period for rental products
        /// </summary>
        public RentalPricePeriod RentalPricePeriod
        {
            get { return (RentalPricePeriod) RentalPricePeriodId; }
            set { RentalPricePeriodId = (int) value; }
        }


        /// <summary>
        /// Productos por categoria
        /// </summary>
        public virtual ICollection<ProductCategory> ProductCategories
        {
            get { return _productCategories ?? (_productCategories = new List<ProductCategory>()); }
            protected set { _productCategories = value; }
        }

        /// <summary>
        /// Productos por proveedor
        /// </summary>
        public virtual ICollection<ProductManufacturer> ProductManufacturers
        {
            get { return _productManufacturers ?? (_productManufacturers = new List<ProductManufacturer>()); }
            protected set { _productManufacturers = value; }
        }

        /// <summary>
        /// Productos por imagenes
        /// </summary>
        public virtual ICollection<ProductPicture> ProductPictures
        {
            get { return _productPictures ?? (_productPictures = new List<ProductPicture>()); }
            protected set { _productPictures = value; }
        }

        /// <summary>
        /// Revision de productos
        /// </summary>
        public virtual ICollection<ProductReview> ProductReviews
        {
            get { return _productReviews ?? (_productReviews = new List<ProductReview>()); }
            protected set { _productReviews = value; }
        }

        /// <summary>
        /// Especificacion de atributos por producto
        /// </summary>
        public virtual ICollection<ProductSpecificationAttribute> ProductSpecificationAttributes
        {
            get
            {
                return _productSpecificationAttributes ??
                       (_productSpecificationAttributes = new List<ProductSpecificationAttribute>());
            }
            protected set { _productSpecificationAttributes = value; }
        }

        /// <summary>
        /// Tags de productos
        /// </summary>
        public virtual ICollection<ProductTag> ProductTags
        {
            get { return _productTags ?? (_productTags = new List<ProductTag>()); }
            protected set { _productTags = value; }
        }

        /// <summary>
        /// Mapeos de atributos de productos
        /// </summary>
        public virtual ICollection<ProductAttributeMapping> ProductAttributeMappings
        {
            get
            {
                return _productAttributeMappings ?? (_productAttributeMappings = new List<ProductAttributeMapping>());
            }
            protected set { _productAttributeMappings = value; }
        }

        /// <summary>
        /// Atributos de productos combinados
        /// </summary>
        public virtual ICollection<ProductAttributeCombination> ProductAttributeCombinations
        {
            get
            {
                return _productAttributeCombinations ??
                       (_productAttributeCombinations = new List<ProductAttributeCombination>());
            }
            protected set { _productAttributeCombinations = value; }
        }

        /// <summary>
        /// Raango de precios
        /// </summary>
        public virtual ICollection<TierPrice> TierPrices
        {
            get { return _tierPrices ?? (_tierPrices = new List<TierPrice>()); }
            protected set { _tierPrices = value; }
        }

        /// <summary>
        /// Descuentos
        /// </summary>
        public virtual ICollection<Discount> AppliedDiscounts
        {
            get { return _appliedDiscounts ?? (_appliedDiscounts = new List<Discount>()); }
            protected set { _appliedDiscounts = value; }
        }

        /// <summary>
        /// Inventartio de productos por almacen
        /// Gets or sets the collection of "ProductWarehouseInventory" records. 
        /// Se usa cuando UseMultipleWarehouses es verdadero y ManageInventoryMethod = ManageStock
        /// </summary>
        public virtual ICollection<ProductWarehouseInventory> ProductWarehouseInventory
        {
            get
            {
                return _productWarehouseInventory ?? (_productWarehouseInventory = new List<ProductWarehouseInventory>());
            }
            protected set { _productWarehouseInventory = value; }
        }
    }
}