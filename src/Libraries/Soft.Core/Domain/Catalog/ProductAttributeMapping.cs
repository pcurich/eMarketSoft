using System.Collections.Generic;
using Soft.Core.Domain.Localization;

namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa un mapeo de los atributos de un producto
    /// </summary>
    public partial class ProductAttributeMapping : BaseEntity, ILocalizedEntity
    {
        private ICollection<ProductAttributeValue> _productAttributeValues;

        /// <summary>
        /// Identificador del producto 
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Identificador del atributo del producto 
        /// </summary>
        public int ProductAttributeId { get; set; }

        /// <summary>
        /// Texto del prompt
        /// </summary>
        public string TextPrompt { get; set; }

        /// <summary>
        /// Si el entity es requerido 
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Identificadoor del typo de control
        /// </summary>
        public int AttributeControlTypeId { get; set; }

        /// <summary>
        /// Orden de aparicion 
        /// </summary>
        public int DisplayOrder { get; set; }

        //Campos de validacion

        /// <summary>
        /// Longitud minima (para textb y multiline textbox)
        /// </summary>
        public int? ValidationMinLength { get; set; }

        /// <summary>
        /// Longitud maxima (para textb y multiline textbox)
        /// </summary>
        public int? ValidationMaxLength { get; set; }

        /// <summary>
        /// Si se permite la extencion de un archivo (para file upload)
        /// </summary>
        public string ValidationFileAllowedExtensions { get; set; }

        /// <summary>
        /// Tamaño maximo de un archivo en kilobytes in kilobytes (for file upload)
        /// </summary>
        public int? ValidationFileMaximumSize { get; set; }

        /// <summary>
        /// Valor por default  (para textbox y multiline textbox)
        /// </summary>
        public string DefaultValue { get; set; }


        /// <summary>
        /// Control de tipo de atributo
        /// </summary>
        public AttributeControlType AttributeControlType
        {
            get { return (AttributeControlType) AttributeControlTypeId; }
            set { AttributeControlTypeId = (int) value; }
        }

        /// <summary>
        /// Atributo del producto
        /// </summary>
        public virtual ProductAttribute ProductAttribute { get; set; }

        /// <summary>
        /// Producto
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Atributos del producto
        /// </summary>
        public virtual ICollection<ProductAttributeValue> ProductAttributeValues
        {
            get { return _productAttributeValues ?? (_productAttributeValues = new List<ProductAttributeValue>()); }
            protected set { _productAttributeValues = value; }
        }
    }
}