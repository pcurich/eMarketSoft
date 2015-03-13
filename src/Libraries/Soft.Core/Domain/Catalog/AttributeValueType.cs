namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa los tipos de atributos
    /// </summary>
    public enum AttributeValueType
    {
        /// <summary>
        /// Valor de atributo simple
        /// </summary>
        Simple = 0,

        /// <summary>
        /// Asociado a un producto
        /// Usado cuando se configura un paquete de producto 
        /// </summary>
        AssociatedToProduct = 10,
    }
}