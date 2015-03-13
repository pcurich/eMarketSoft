using System;
using System.Linq;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace Soft.Core.Caching
{
    /// <summary>
    ///     Representa un manejador para cache entre HTTP request (largo tiempo de almacenamiento de cache)
    /// </summary>
    public class MemoryCacheManager : ICacheManager
    {
        protected ObjectCache Cache
        {
            get { return MemoryCache.Default; }
        }

        /// <summary>
        /// Retorna un elemento almacenado en la cache con una llave determinada
        /// </summary>
        /// <typeparam name="T">Tipo</typeparam>
        /// <param name="key">Lave para obtener el valor</param>
        /// <returns>
        /// Valor asociado con la llave especifica
        /// </returns>
        public virtual T Get<T>(string key)
        {
            return (T) Cache[key];
        }

        /// <summary>
        /// Permite agregar una data a la cache con un tiempo de permanencia
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Tiempo de duracion en cache</param>
        public virtual void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime)
            };
            Cache.Add(new CacheItem(key, data), policy);
        }

        /// <summary>
        /// Determina si objeto con una llave dada esta almacenado en cache
        /// </summary>
        /// <param name="key">Llave</param>
        /// <returns>
        /// Si esta asociado<c>true</c> de lo contrario <c>false</c>
        /// </returns>
        public virtual bool IsSet(string key)
        {
            return (Cache.Contains(key));
        }

        /// <summary>
        /// Remueve un valor especifico de la cache que
        /// esta asociado a una llave
        /// </summary>
        /// <param name="key">/Llave</param>
        public virtual void Remove(string key)
        {
            Cache.Remove(key);
        }

        /// <summary>
        ///     Remueve un item por un patron
        /// </summary>
        /// <param name="pattern">patron</param>
        public void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove =
                (from item
                    in Cache
                    where regex.IsMatch(item.Key)
                    select item.Key).ToList();

            foreach (var key in keysToRemove)
                Remove(key);
        }

        /// <summary>
        ///     Borra toda la data de la cache
        /// </summary>
        public void Clear()
        {
            foreach (var item in Cache)
                Remove(item.Key);
        }
    }
}