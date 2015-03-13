using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace Soft.Core.Caching
{
    /// <summary>
    ///     Representa un manejador de cache durante un request de Http (largo tiempo de almacenamiento de cache)
    /// </summary>
    public class PerRequestCacheManager : ICacheManager
    {
        private readonly HttpContextBase _context;

        public PerRequestCacheManager(HttpContextBase context)
        {
            _context = context;
        }

        /// <summary>
        ///     Retorna un elemento almacenado en la cache con una llave determinada
        /// </summary>
        /// <typeparam name="T">Tipo</typeparam>
        /// <param name="key">Lave para obtener el valor</param>
        /// <returns>
        ///     Valor asociado con la llave especifica
        /// </returns>
        public virtual T Get<T>(string key)
        {
            var items = GetItems();
            return items == null ? default(T) : (T) items[key];
        }

        /// <summary>
        ///     Permite agregar una data a la cache con un tiempo de permanencia
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Tiempo de duracion en cache</param>
        public virtual void Set(string key, object data, int cacheTime)
        {
            var items = GetItems();
            if (items == null)
                return;

            if (data == null)
                return;

            if (items.Contains(key))
                items[key] = data; // se remplaza su valor
            else
                items.Add(key, data); // se agrega al cache
        }

        /// <summary>
        ///     Determina si objeto con una llave dada esta almacenado en cache
        /// </summary>
        /// <param name="key">Llave</param>
        /// <returns>
        ///     Si esta asociado<c>true</c> de lo contrario <c>false</c>
        /// </returns>
        public virtual bool IsSet(string key)
        {
            var items = GetItems();
            return items != null && items[key] != null;
        }

        /// <summary>
        ///     Remueve un valor especifico de la cache que
        ///     esta asociado a una llave
        /// </summary>
        /// <param name="key">/Llave</param>
        public virtual void Remove(string key)
        {
            var items = GetItems();
            if (items != null)
                items.Remove(key);
        }

        /// <summary>
        ///     Remueve un item por un patron
        /// </summary>
        /// <param name="pattern">patron</param>
        public virtual void RemoveByPattern(string pattern)
        {
            var items = GetItems();
            if (items == null)
                return;

            var enumerator = items.GetEnumerator();
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = new List<String>();

            while (enumerator.MoveNext())
            {
                if (regex.IsMatch(enumerator.Key.ToString()))
                    keysToRemove.Add(enumerator.Key.ToString());
            }


            foreach (var key in keysToRemove)
                items.Remove(key);
            
        }

        /// <summary>
        /// Borra toda la data de la cache
        /// </summary>
        public virtual void Clear()
        {
            var items = GetItems();
            if (items == null) 
                return;

            var enumerator = items.GetEnumerator();
            var keysToRemove = new List<String>();
                
            while (enumerator.MoveNext())
                keysToRemove.Add(enumerator.Key.ToString());
            
            foreach (var key in keysToRemove)
                items.Remove(key);
        }

        /// <summary>
        ///     Crea una nueva instancia de la clase SoftRequestCache
        /// </summary>
        protected virtual IDictionary GetItems()
        {
            return _context != null ? _context.Items : null;
        }
    }
}