namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa una especificacion de un atributo de un producto
    /// </summary>
    public partial class ProductSpecificationAttribute : BaseEntity
    {
        /// <summary>
        /// Identificador de producto
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Identificador del tipo de atributo
        /// </summary>
        public int AttributeTypeId { get; set; }

        /// <summary>
        /// Identificador del tipo de la opcion de un atributo
        /// </summary>
        public int SpecificationAttributeOptionId { get; set; }

        /// <summary>
        /// Valores personalizados
        /// </summary>
        public string CustomValue { get; set; }

        /// <summary>
        /// Si se puede filtrar el atributo
        /// </summary>
        public bool AllowFiltering { get; set; }

        /// <summary>
        /// Si se muestra en la pagina del producto
        /// </summary>
        public bool ShowOnProductPage { get; set; }

        /// <summary>
        /// Orden de aparicion 
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Producto 
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Especificacion de las opciones deun atributo
        /// </summary>
        public virtual SpecificationAttributeOption SpecificationAttributeOption { get; set; }


        /// <summary>
        /// Atributo del contro  de tipoa
        /// </summary>
        public SpecificationAttributeType AttributeType
        {
            get { return (SpecificationAttributeType) AttributeTypeId; }
            set { AttributeTypeId = (int) value; }
        }
    }
}