using System;
using System.Collections.Generic;
using System.Linq;

namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Extension para productos
    /// </summary>
    public static class ProductExtensions
    {
        /// <summary>
        /// Para parsear se requiere las Ids de los productos
        /// </summary>
        /// <param name="product">Producto</param>
        /// <returns>Lista de los Ids de los productos requeridos</returns>
        public static int[] ParseRequiredProductIds(this Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            if (String.IsNullOrEmpty(product.RequiredProductIds))
                return new int[0];

            var ids = new List<int>();

            foreach (var idStr in product.RequiredProductIds
                .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim()))
            {
                int id;
                if (int.TryParse(idStr, out id))
                    ids.Add(id);
            }

            return ids.ToArray();
        }

        /// <summary>
        ///  Si el producto esta disponible ahora (fechas)
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>Result</returns>
        public static bool IsAvailable(this Product product)
        {
            return IsAvailable(product, DateTime.UtcNow);
        }

        /// <summary>
        /// Si el producto esta disponibles (fechas)
        /// </summary>
        /// <param name="product">Producto</param>
        /// <param name="dateTime">Fecha a revisar</param>
        /// <returns>Result</returns>
        public static bool IsAvailable(this Product product, DateTime dateTime)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            if (product.AvailableStartDateTimeUtc.HasValue && product.AvailableStartDateTimeUtc.Value > dateTime)
            {
                return false;
            }

            if (product.AvailableEndDateTimeUtc.HasValue && product.AvailableEndDateTimeUtc.Value < dateTime)
            {
                return false;
            }

            return true;
        }
    }
}