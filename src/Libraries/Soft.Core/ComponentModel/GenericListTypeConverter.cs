using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Soft.Core.ComponentModel
{
    public class GenericListTypeConverter<T> : TypeConverter
    {
        protected readonly TypeConverter TypeConverter;

        public GenericListTypeConverter()
        {
            TypeConverter = TypeDescriptor.GetConverter(typeof (T));
            if (TypeConverter == null)
                throw new InvalidOperationException("No existe el tipo de conversor para " + typeof (T).FullName);
        }

        protected virtual string[] GetStringArray(string input)
        {
            if (String.IsNullOrEmpty(input))
                return new string[0];

            var result = input
                .Split(',')
                .Select(x => x.Trim())
                .ToArray();

            return result;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType != typeof (string))
                return base.CanConvertFrom(context, sourceType);

            var items = GetStringArray(sourceType.ToString());
            return items.Any();
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var input = value as string;
            if (input == null)
                return base.ConvertFrom(context, culture, value);

            var items = GetStringArray(input);
            var result = new List<T>();
            Array.ForEach(items, s =>
            {
                var item = TypeConverter.ConvertFromInvariantString(s);
                if (item != null)
                {
                    result.Add((T) item);
                }
            });

            return result;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,Type destinationType)
        {
            if (destinationType != typeof (string))
                return base.ConvertTo(context, culture, value, destinationType);

            var result = string.Empty;
            if (value == null)
                return result;

            //no se usa string.Join() porque no soporta invariant culture
            for (var i = 0; i < ((IList<T>) value).Count; i++)
            {
                var str1 = Convert.ToString(((IList<T>) value)[i], culture);
                result += str1;
                //no se agrega coma despues del ultimo elemento
                if (i != ((IList<T>) value).Count - 1)
                    result += ",";
            }
            return result;
        }
    }
}