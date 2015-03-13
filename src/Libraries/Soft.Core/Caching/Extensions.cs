using System;

namespace Soft.Core.Caching
{
    /// <summary>
    /// Extensiones
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// variable (lock) para el soporte del Hilo seguro 
        /// </summary>
        private static readonly object SyncObject = new object();

        /// <summary>
        /// Obtiene un item de la cache. Si no se encuentra en la cache lo carga
        /// </summary>
        /// <typeparam name="T">Tipo</typeparam>
        /// <param name="cacheManager">Gestion de cache</param>
        /// <param name="key">Llave de la cache</param>
        /// <param name="acquire">Funcion para cargar los items. Si no esta e la cache</param>
        /// <returns>Item de cache</returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> acquire)
        {
            return Get(cacheManager, key, 60, acquire);
        }

        /// <summary>
        /// Obtiene un item de la cache. Si no se encuentra en la cache lo carga
        /// </summary>
        /// <typeparam name="T">Tipo </typeparam>
        /// <param name="cacheManager">Gestion de cache</param>
        /// <param name="key">Llave de la cache</param>
        /// <param name="cacheTime">Tiempo de duracion de la cache</param>
        /// <param name="acquire">Funcion para cargar los items. Si no esta e la cache</param>
        /// <returns>Item de cache</returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<T> acquire)
        {
            lock (SyncObject)
            {
                if (cacheManager.IsSet(key))
                {
                    return cacheManager.Get<T>(key);
                }

                var result = acquire();
                if (cacheTime > 0)
                    cacheManager.Set(key, result, cacheTime);
                return result;
            }
        }
    }
}