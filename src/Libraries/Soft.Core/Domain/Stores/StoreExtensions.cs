﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Soft.Core.Domain.Stores
{
    public static class StoreExtensions
    {
        /// <summary>
        /// Parsea los separadores de comas del host
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        public static string[] ParseHostValues(this Store store)
        {
            if (store == null)
                throw new ArgumentNullException("store");

            var parsedValues = new List<string>();
            if (!String.IsNullOrEmpty(store.Hosts))
            {
                string[] hosts = store.Hosts.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
                foreach (string host in hosts)
                {
                    var tmp = host.Trim();
                    if (!String.IsNullOrEmpty(tmp))
                        parsedValues.Add(tmp);
                }
            }
            return parsedValues.ToArray();
        }

        /// <summary>
        /// Indica si un host contiene un especifico host
        /// </summary>
        /// <param name="store"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        public static bool ContainsHostValue(this Store store, string host)
        {
            if (store == null)
                throw new ArgumentNullException("store");

            if (String.IsNullOrEmpty(host))
                return false;

            var contains = store.ParseHostValues()
                .FirstOrDefault(x => x.Equals(host, StringComparison.InvariantCultureIgnoreCase)) != null;
            return contains;
        }
    }
}