using Soft.Core.Domain.Localization;

namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa un valor de un atributo de un producto 
    /// </summary>
    public partial class ProductAttributeValue : BaseEntity, ILocalizedEntity
    {
        /// <summary>
        /// Identificador del mapeo de un atributo a un producto 
        /// </summary>
        public int ProductAttributeMappingId { get; set; }

        /// <summary>
        /// Identificador del tipo de valor de un atributo 
        /// </summary>
        public int AttributeValueTypeId { get; set; }

        /// <summary>
        /// Identificador de la asociacion de un producto 
        /// (usado solo con AttributeValueType.AssociatedToProduct)
        /// </summary>
        public int AssociatedProductId { get; set; }

        /// <summary>
        /// Nombre del atributo de un producto 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Valor del color en RGB (usado con "Color squares" attribute type)
        /// </summary>
        public string ColorSquaresRgb { get; set; }

        /// <summary>
        /// Ajusto del precio 
        /// (usado con  AttributeValueType.Simple)
        /// </summary>
        public decimal PriceAdjustment { get; set; }

        /// <summary>
        /// Ajuste del peso
        /// (usado solo con AttributeValueType.Simple)
        /// </summary>
        public decimal WeightAdjustment { get; set; }

        /// <summary>
        /// Costo del atributo
        /// (usado solo con AttributeValueType.Simple)
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Cantidad de productos asociados
        /// (usado solo con AttributeValueType.AssociatedToProduct)
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Di el valor debe estar pre selecionado
        /// </summary>
        public bool IsPreSelected { get; set; }

        /// <summary>
        /// Orden de aparicion 
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Imagen asociado con el valor (identifier)
        /// </summary>
        public int PictureId { get; set; }

        /// <summary>
        /// Mappeo de atributo
        /// </summary>
        public virtual ProductAttributeMapping ProductAttributeMapping { get; set; }

        /// <summary>
        /// Tipo de atributo
        /// </summary>
        public AttributeValueType AttributeValueType
        {
            get { return (AttributeValueType) AttributeValueTypeId; }
            set { AttributeValueTypeId = (int) value; }
        }
    }
}