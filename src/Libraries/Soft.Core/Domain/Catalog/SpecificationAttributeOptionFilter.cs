namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa unas opciones de especificacion de atributos para filtros
    /// </summary>
    public class SpecificationAttributeOptionFilter
    {
        /// <summary>
        /// Identificador del atributo
        /// </summary>
        public int SpecificationAttributeId { get; set; }

        /// <summary>
        /// Nombre de la especificacion del atributo
        /// </summary>
        public string SpecificationAttributeName { get; set; }

        /// <summary>
        /// Orden de aparicion de las especificaciones de los productos
        /// </summary>
        public int SpecificationAttributeDisplayOrder { get; set; }

        /// <summary>
        /// Identificador de la especificacion de los opciones de los atributos
        /// </summary>
        public int SpecificationAttributeOptionId { get; set; }

        /// <summary>
        /// Nombre de la especificacion de los opciones de un atributo
        /// </summary>
        public string SpecificationAttributeOptionName { get; set; }

        /// <summary>
        /// Orden para mostrar de la especificacion de los opciones de un atributo
        /// </summary>
        public int SpecificationAttributeOptionDisplayOrder { get; set; }
    }
}