using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Soft.Core;

namespace Soft.Web.Framework.Mvc
{
    /// <summary>
    ///     Este MetadataProvider agrega acgunas funcionalidades arriba del default DataAnnotationsModelMetadataProvider
    ///     Este agrega atributos personalizados (implementando IModelAttribute) a las propiedades de AdditionalValues en
    ///     el modelo de metadata de modo que puede ser recuperada más tarde
    /// </summary>
    public class SoftMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(
            IEnumerable<Attribute> attributes,
            Type containerType,
            Func<object> modelAccessor,
            Type modelType,
            string propertyName)
        {
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            var additionalValues = attributes.OfType<IModelAttribute>().ToList();
            foreach (var additionalValue in additionalValues)
            {
                if (metadata.AdditionalValues.ContainsKey(additionalValue.Name))
                    throw new SoftException("Ya existe un atributo con el nombre de \"" + additionalValue.Name +
                                            "\" en este modelo.");
                metadata.AdditionalValues.Add(additionalValue.Name, additionalValue);
            }
            return metadata;
        }
    }
}