using System;
using System.Collections.Generic;

namespace Soft.Core.Infrastructure.Patterns
{
    /// <summary>
    /// Un singleton estaticvo que se usa para almacenar objetos a traves del lifetime
    /// para el dominio app 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> : Singleton
    {
        private static T _instance;

        /// <summary>
        /// La instancia de singleton para el tipo especifico T. 
        /// Solo una instancia (al momento) de este objeto para cada tipo 
        /// </summary>
        public static T Instance
        {
            get { return _instance; }
            set
            {
                _instance = value;
                AllSingletons[typeof (T)] = value;
            }
        }
    }

    /// <summary>
    /// Provee una lista de singleton para cada tipo 
    /// </summary>
    /// <typeparam name="T">El tipo de listaa almacenar</typeparam>
    public class SingletonList<T> : Singleton<IList<T>>
    {
        static SingletonList()
        {
            Singleton<IList<T>>.Instance = new List<T>();
        }

        /// <summary>
        /// La instancia de singleton para un tipo especifico.
        /// Solo una instancia (al momento) de este objeto para cada tipo 
        /// </summary>
        public new static IList<T> Instance
        {
            get { return Singleton<IList<T>>.Instance; }
        }
    }

    public class SingletonDictionary<TKey, TValue> : Singleton<IDictionary<TKey, TValue>>
    {
        static SingletonDictionary()
        {
            Singleton<Dictionary<TKey, TValue>>.Instance = new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// La instancia de singleton para un tipo especifico.
        /// Solo una instancia (al momento) de este objeto para cada tipo 
        /// </summary>
        public new static IDictionary<TKey, TValue> Instance
        {
            get { return Singleton<Dictionary<TKey, TValue>>.Instance; }
        }
    }


    public class Singleton
    {
        private static readonly IDictionary<Type, object> _allSingletons;

        static Singleton()
        {
            _allSingletons = new Dictionary<Type, object>();
        }

        public static IDictionary<Type, object> AllSingletons
        {
            get { return _allSingletons; }
        }
    }
}