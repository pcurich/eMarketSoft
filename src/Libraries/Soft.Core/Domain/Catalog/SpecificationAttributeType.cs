namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa un tipo de atributo para una especificacion
    /// </summary>
    public enum SpecificationAttributeType
    {
        /// <summary>
        /// Option
        /// </summary>
        Option = 0,

        /// <summary>
        /// Texto personnalizado
        /// </summary>
        CustomText = 10,

        /// <summary>
        /// Texto personnalizado en HTML
        /// </summary>
        CustomHtmlText = 20,

        /// <summary>
        /// Hyperlink
        /// </summary>
        Hyperlink = 30
    }
}