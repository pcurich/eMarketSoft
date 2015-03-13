using Soft.Core.Configuration;

namespace Soft.Core.Domain.Vendors
{
    /// <summary>
    /// Configuracion de vendedor
    /// </summary>
    public class VendorSettings : ISettings
    {
        /// <summary>
        /// Establece el valor x defecto para usar como tamaño de las opciones de la pagina (nuevos vendedores)
        /// </summary>
        public string DefaultVendorPageSizeOptions { get; set; }

        /// <summary>
        /// Cuantos vendedores para mostrar en el bloque de vendedores
        /// </summary>
        public int VendorsBlockItemsToDisplay { get; set; }

        /// <summary>
        /// Indica si se muestra el nombre del vendedor en la pagina de los detalles del producto 
        /// </summary>
        public bool ShowVendorOnProductDetailsPage { get; set; }
    }
}