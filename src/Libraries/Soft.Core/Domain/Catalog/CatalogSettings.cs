using Soft.Core.Configuration;

namespace Soft.Core.Domain.Catalog
{
    public class CatalogSettings : ISettings
    {
        /// <summary>
        /// Muestra Sku de los productos
        /// </summary>
        public bool ShowProductSku { get; set; }

        /// <summary>
        /// Indica si si se va a mostrar el numero de parte del proveedor 
        /// </summary>
        public bool ShowManufacturerPartNumber { get; set; }

        /// <summary>
        /// Si se muestra el GTIN del pructo
        /// </summary>
        public bool ShowGtin { get; set; }

        /// <summary>
        /// si se debe mostrar el icono de Free shipping en los productos
        /// </summary>
        public bool ShowFreeShippingNotification { get; set; }

        /// <summary>
        /// Permite ordenar losproductos
        /// </summary>
        public bool AllowProductSorting { get; set; }

        /// <summary>
        /// Si se puede cambiar la vista de los Productos
        /// </summary>
        public bool AllowProductViewModeChanging { get; set; }

        /// <summary>
        /// Si los clientes pueden cambiar el modo de vista de los productos 
        /// </summary>
        public string DefaultViewMode { get; set; }

        /// <summary>
        /// Si la pagina de los detalles de la categoria deberian incluir productos de las subcategorias
        /// </summary>
        public bool ShowProductsFromSubcategories { get; set; }

        /// <summary>
        /// Si el numero de productos se deberia mostrar enncima de cada categoria 
        /// </summary>
        public bool ShowCategoryProductNumber { get; set; }

        /// <summary>
        /// Si se debe incluir subcategorias (si es true)
        /// </summary>
        public bool ShowCategoryProductNumberIncludingSubcategories { get; set; }

        /// <summary>
        /// Si se debe activar el BreadCrumb
        /// </summary>
        public bool CategoryBreadcrumbEnabled { get; set; }

        /// <summary>
        /// Si el boton de compartir esta activo
        /// </summary>
        public bool ShowShareButton { get; set; }

        /// <summary>
        /// Compartir el codigo (AddThis button code)
        /// </summary>
        public string PageShareCode { get; set; }

        /// <summary>
        /// Productos que deben ser aprobados para ser publicados
        /// </summary>
        public bool ProductReviewsMustBeApproved { get; set; }

        /// <summary>
        /// Valor por defecto de las estrellitas
        /// </summary>
        public int DefaultProductRatingValue { get; set; }

        /// <summary>
        /// Usuarios anonimos deberias hacer una opinion del producto
        /// </summary>
        public bool AllowAnonymousUsersToReviewProduct { get; set; }

        /// <summary>
        /// SI las notificaciones del dueño de una tienda hacerca de la revision de un nuevo producto
        /// </summary>
        public bool NotifyStoreOwnerAboutNewProductReviews { get; set; }

        /// <summary>
        /// Indica si se envia un email a un amigo
        /// </summary>
        public bool EmailAFriendEnabled { get; set; }

        public bool AllowAnonymousUsersToEmailAFriend { get; set; }

        /// <summary>
        /// Numero de productos recientemente vistos
        /// </summary>
        public int RecentlyViewedProductsNumber { get; set; }

        public bool RecentlyViewedProductsEnabled { get; set; }
        
        /// <summary>
        /// Numero de productos recientemente agregados
        /// </summary>
        public int RecentlyAddedProductsNumber { get; set; }

        public bool RecentlyAddedProductsEnabled { get; set; }

        public bool CompareProductsEnabled { get; set; }
        public int CompareProductsNumber { get; set; }

        /// <summary>
        /// Autocompletar en la busqueda de un producto
        /// </summary>
        public bool ProductSearchAutoCompleteEnabled { get; set; }

        /// <summary>
        /// Numero de productos devueltos usando autocomplete
        /// </summary>
        public int ProductSearchAutoCompleteNumberOfProducts { get; set; }

        public bool ShowProductImagesInSearchAutoComplete { get; set; }

        /// <summary>
        /// Minima longitud para buscar
        /// </summary>
        public int ProductSearchTermMinimumLength { get; set; }

        public bool ShowBestsellersOnHomepage { get; set; }
        public int NumberOfBestsellersOnHomepage { get; set; }

        /// <summary>
        /// Numero de productos por pagina
        /// </summary>
        public int SearchPageProductsPerPage { get; set; }

        public bool SearchPageAllowCustomersToSelectPageSize { get; set; }

        public string SearchPagePageSizeOptions { get; set; }

        /// <summary>
        /// Lista de productos comprados por otros usuarios quienes compraron 
        /// </summary>
        public bool ProductsAlsoPurchasedEnabled { get; set; }

        /// <summary>
        /// Numero de productos comprados por otros usuarios
        /// </summary>
        public int ProductsAlsoPurchasedNumber { get; set; }

        public bool EnableDynamicPriceUpdate { get; set; }

        public bool DynamicPriceUpdateAjax { get; set; }

        /// <summary>
        /// Numeros de tags de productos que aparecera en la nube de tags
        /// </summary>
        public int NumberOfProductTags { get; set; }

        public int ProductsByTagPageSize { get; set; }

        /// <summary>
        /// Indica a los clientes seleccionar el tamaño de la pagina en products by tag
        /// </summary>
        public bool ProductsByTagAllowCustomersToSelectPageSize { get; set; }

        public string ProductsByTagPageSizeOptions { get; set; }

        public bool IncludeShortDescriptionInCompareProducts { get; set; }

        public bool IncludeFullDescriptionInCompareProducts { get; set; }

        /// <summary>
        /// Una opcion que indica si los productos en las categorias y proveedores deberian incluir sus caracteristicas
        /// </summary>
        public bool IncludeFeaturedProductsInNormalLists { get; set; }

        /// <summary>
        /// Indica si la precio es mostrado con descuento
        /// </summary>
        public bool DisplayTierPricesWithDiscounts { get; set; }

        public bool IgnoreDiscounts { get; set; }

        /// <summary>
        /// Ignora las caracteristicas de los productos 
        /// </summary>
        public bool IgnoreFeaturedProducts { get; set; }

        public bool IgnoreAcl { get; set; }

        /// <summary>
        /// Indica si se ignora limitado a tiendas
        /// </summary>
        public bool IgnoreStoreLimitations { get; set; }

        /// <summary>
        /// si se pone los precios de los productos en cache
        /// </summary>
        public bool CacheProductPrices { get; set; }

        public string DefaultCategoryPageSizeOptions { get; set; }

        public string DefaultManufacturerPageSizeOptions { get; set; }

        /// <summary>
        /// Indica el numero maximo de back in store subscripcion 
        /// </summary>
        public int MaximumBackInStockSubscriptions { get; set; }

        /// <summary>
        /// Indica cuantas niveles de subcategorias   se muestra en el menu de arriba
        /// </summary>
        public int TopCategoryMenuSubcategoryLevelsToDisplay { get; set; }

        public bool LoadAllSideCategoryMenuSubcategories { get; set; }

        /// <summary>
        /// Cuantos proveedores se muestran 
        /// </summary>
        public int ManufacturersBlockItemsToDisplay { get; set; }

        public bool DisplayTaxShippingInfoFooter { get; set; }

        public bool DisplayTaxShippingInfoProductDetailsPage { get; set; }

        public bool DisplayTaxShippingInfoProductBoxes { get; set; }

        public bool DisplayTaxShippingInfoWishlist { get; set; }

        public bool DisplayTaxShippingInfoOrderDetailsPage { get; set; }
    }
}