namespace Soft.Core.Caching
{
    /// <summary>
    /// Representa una implemetacion nula de la interfaz
    /// </summary>
    public partial class SoftNullCache : ICacheManager
    {
        /// <summary>
        /// Obtiene o establece el valor asociado con una llave especifica
        /// </summary>
        /// <typeparam name="T">Tipo</typeparam>
        /// <param name="key">Lave para obtener el valor</param>
        /// <returns>
        /// Valor asociado con la llave especifica
        /// </returns>
        public T Get<T>(string key)
        {
            return default(T);
        }

        /// <summary>
        /// Agrega una llave especifica con un objeto a la cache
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Tiempo en que ocurre</param>
        public void Set(string key, object data, int cacheTime)
        {
        }

        /// <summary>
        /// Si el valor asociado con una  llave especifica esta en cache
        /// </summary>
        /// <param name="key">Llave</param>
        /// <returns>
        /// Si esta asociado<c>true</c>
        /// </returns>
        public bool IsSet(string key)
        {
            return false;
        }

        /// <summary>
        /// Remueve un valor espacifico asociado a una llave
        /// </summary>
        /// <param name="key">/Llave</param>
        public void Remove(string key)
        {
        }

        /// <summary>
        /// Remueve un item por un patron
        /// </summary>
        /// <param name="pattern">patron</param>
        public void RemoveByPattern(string pattern)
        {
        }

        /// <summary>
        /// Borra toda la data de la cache
        /// </summary>
        public void Clear()
        {
        }
    }
}