using System;
using System.Collections.Generic;
using System.Linq;
using Soft.Core.Domain.Catalog;
using Soft.Core.Domain.Customers;

namespace Soft.Services.Catalog
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class TierPriceExtensions
    {
        /// <summary>
        /// Filtra los precios de nivel por tienda
        /// </summary>
        /// <param name="source">Lista de precios de nivel</param>
        /// <param name="storeId">Identificador de tienda</param>
        /// <returns>Precios de nivel filtrados por tienda</returns>
        public static IList<TierPrice> FilterByStore(this IList<TierPrice> source,int storeId)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var result = new List<TierPrice>();
            foreach (var tierPrice in source)
            {
                //Revisa los requerimientos de la tienda. Si tierPrice.StoreId==0 aplica a todas las tiendas
                if (tierPrice.StoreId > 0 && tierPrice.StoreId != storeId)
                    continue;

                //Se aplica a todos
                result.Add(tierPrice);
            }

            return result;
        }

        /// <summary>
        /// Filtro de precio de nivel para los clientes
        /// </summary>
        /// <param name="source">Lista de precio de nivel</param>
        /// <param name="customer">Cliente</param>
        /// <returns>Precios de nivel filtrados</returns>
        public static IList<TierPrice> FilterForCustomer(this IList<TierPrice> source,
            Customer customer)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var result = new List<TierPrice>();
            foreach (var tierPrice in source)
            {
                //Revisa el rol del cliente 
                if (tierPrice.CustomerRole != null)
                {
                    if (customer == null)
                        continue;

                    var customerRoles = customer.CustomerRoles.Where(cr => cr.Active).ToList();
                    if (customerRoles.Count == 0)
                        continue;

                    var roleIsFound = false;
                    foreach (var customerRole in customerRoles)
                        if (customerRole == tierPrice.CustomerRole)
                            roleIsFound = true;

                    if (!roleIsFound)
                        continue;

                }

                result.Add(tierPrice);
            }

            return result;
        }

        /// <summary>
        /// Remueve las cantidades duplicadas( deja solo las de minimo precio)
        /// </summary>
        /// <param name="source">Lista de precion de nivel</param>
        /// <returns>Precios de nivel filtrados</returns>
        public static IList<TierPrice> RemoveDuplicatedQuantities(this IList<TierPrice> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            
            //Encuentra duplicados
            var query = from tierPrice in source
                        group tierPrice by tierPrice.Quantity into g
                        where g.Count() > 1
                        select new { Quantity = g.Key, TierPrices = g.ToList() };
            foreach (var item in query)
            {
                //Encuentra el registro de precio de nivel minimo (no se removera)
                var minTierPrice = item.TierPrices.Aggregate((tp1, tp2) => (tp1.Price < tp2.Price ? tp1 : tp2));
                //remover todos los items
                item.TierPrices.Remove(minTierPrice);
                item.TierPrices.ForEach(x=> source.Remove(x));
            }

            return source;
        }
    }
}
