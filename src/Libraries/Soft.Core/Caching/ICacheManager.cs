namespace Soft.Core.Caching
{
    /// <summary>
    /// Interfaz para la gestion de la cache
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Retorna un elemento almacenado en la cache con una llave determinada
        /// </summary>
        /// <typeparam name="T">Tipo</typeparam>
        /// <param name="key">Lave para obtener el valor</param>
        /// <returns>Valor asociado con la llave especifica</returns>
        T Get<T>(string key);

        /// <summary>
        /// Permite agregar una data a la cache con un tiempo de permanencia
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Tiempo de duracion en cache</param>
        void Set(string key, object data, int cacheTime);

        /// <summary>
        /// Determina si objeto con una llave dada esta almacenado en cache
        /// </summary>
        /// <param name="key">Llave</param>
        /// <returns>
        /// Si esta asociado<c>true</c> de lo contrario <c>false</c>
        /// </returns>
        bool IsSet(string key);

        /// <summary>
        /// Remueve un valor especifico de la cache que 
        /// esta asociado a una llave
        /// </summary>
        /// <param name="key">/Llave</param>
        void Remove(string key);

        /// <summary>
        /// Remueve un item por un patron
        /// </summary>
        /// <param name="pattern">patron</param>
        void RemoveByPattern(string pattern);

        /// <summary>
        /// Borra toda la data de la cache
        /// </summary>
        void Clear();
    }
}