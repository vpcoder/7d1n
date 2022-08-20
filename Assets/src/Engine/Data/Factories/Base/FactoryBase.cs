using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data.Factories
{

    /// <summary>
    /// 
    /// Базовый класс фабрики
    /// Содержит в себе загрузчик сущностей, кеш из коллекции с ключом long и указанным типом значения
    /// Позволяет получать сущности по ключам
    /// ---
    /// The base class of the factory
    /// Contains an entities loader, cache from the collection with a long key and a specified value type.
    /// Allows to get entities by keys
    /// 
    /// </summary>
    /// <typeparam name="T">
    ///     Тип хранимого объекта в фабрике
    ///     ---
    ///     The type of the stored object in the factory
    /// </typeparam>
    /// <typeparam name="TLoader">
    ///     Загрузчик для этой фабрики
    ///     ---
    ///     Loader for this factory
    /// </typeparam>
    public abstract class FactoryBase<T, TLoader> : IFactory<T> where T : class, IIdentity
                                                                where TLoader : class, IFactoryLoader<T>
    {

        #region Hidden Fields
        
        /// <summary>
        ///     Кеш с загруженными данными
        ///     ---
        ///     Cache with downloaded data
        /// </summary>
        private readonly IDictionary<long, T> dataByID = new Dictionary<long, T>();
        
        /// <summary>
        ///     Загрузчик, который выполняет загрузку данных для фабрики
        ///     ---
        ///     The loader that performs data loading for the factory
        /// </summary>
        private readonly IFactoryLoader<T> loader;
        
        #endregion
        
        #region Properties
        
        /// <summary>
        ///     Прямая ссылка на кеш с загруженными данными
        ///     ! Использовать осторожно !
        ///     ---
        ///     Direct link to cache with downloaded data
        ///     ! Use with care !
        /// </summary>
        public IDictionary<long, T> Data => dataByID;
        
        /// <summary>
        ///     Прямая ссылка на загрузчика данных
        ///     ---
        ///     Direct link to the data loader
        /// </summary>
        public IFactoryLoader<T> Loader => loader;

        #endregion

        #region Ctor
        
        protected FactoryBase()
        {
            loader = Activator.CreateInstance<TLoader>(); // Создаём экземпляр загрузчика дефолтным конструктором
            ReloadFactory(); // Загружаем данные в кеш
        }
        
        #endregion

        /// <summary>
        ///     Выполняет перезагрузку данных в кеш
        ///     ---
        ///     Performs reloading of data in the cache
        /// </summary>
        public void ReloadFactory()
        {
            if (loader == null)
                throw new Exception("Hasn't setup loader!");
            dataByID.Clear();
            foreach (T value in loader.Load())
                dataByID.Add(value.ID, value);
        }
        
        /// <summary>
        ///     Получает прямую ссылку на экземпляры данных в кеше по ключу
        ///     ! Осторожно, ссылка прямая !
        ///     ---
        ///     Gets a direct link to the data instances in the cache by the key
        ///     ! Careful, the link is direct !
        /// </summary>
        /// <param name="ids">
        ///     Идентификаторы искомых объектов
        ///     ---
        ///     The identifiers of the objects you are looking for
        /// </param>
        /// <returns>
        ///     Кешированные экземпляры объектов, найденных по их ключам,
        ///     Объект не попадёт в результирующую коллекцию - если по ключу ничего найти не удалось
        ///     ---
        ///     Cached instances of objects found by their keys,
        ///     The object will not appear in the resulting collection - if nothing could be found using the key
        /// </returns>
        public virtual ICollection<T> GetAll(ICollection<long> ids)
        {
            var result = new LinkedList<T>();
            foreach (var id in ids)
            {
                var item = Get(id);
                if(item != null)
                    result.AddLast(item);
            }
            return result;
        }

        /// <summary>
        ///     Получает прямую ссылку на единственный экземпляр данных в кеше по ключу
        ///     ! Осторожно, ссылка прямая !
        ///     ---
        ///     Gets a direct link to a single instance of the data in the cache by the key
        ///     ! Careful, the link is direct !
        /// </summary>
        /// <param name="id">
        ///     Идентификатор искомого объекта
        ///     ---
        ///     Identifier of the object you are looking for
        /// </param>
        /// <returns>
        ///     Кешированный экземпляр объекта, найденного по ключу,
        ///     null - если объекта по ключу найти не удалось
        ///     ---
        ///     A cached instance of the object found by the key,
        ///     null - if the object could not be found by the key
        /// </returns>
        public virtual T Get(long id)
        {
            if (dataByID.TryGetValue(id, out var value))
                return value;
#if UNITY_EDITOR
            Debug.LogError("T " + typeof(T).Name + " with id " + id + " not founded in factory " + GetType().Name + "!");
#endif
            return null;
        }

        /// <summary>
        ///     Создаёт самостоятельную копию указанного объекта по id
        ///     ---
        ///     Creates an independent copy of the specified object by id
        /// </summary>
        /// <param name="id">
        ///     Идентификатор искомого объекта в кеше
        ///     ---
        ///     Identifier of the searched object in the cache
        /// </param>
        /// <returns>
        ///     Возвращает идентичную копию объекта который находится в кеше
        ///     ---
        ///     Returns an identical copy of the object that is in the cache
        /// </returns>
        public virtual T Create(long id)
        {
            return (T)Get(id).Copy();
        }
        
        /// <summary>
        ///     Проверяет: существует ли указанный объект в кеше?
        ///     ---
        ///     Checks: does the specified object exist in the cache?
        /// </summary>
        /// <param name="id">
        ///     Идентификатор искомого объекта в кеше
        ///     ---
        ///     Identifier of the searched object in the cache
        /// </param>
        /// <returns>
        ///     true - объект существует в кеше,
        ///     false - объекта нет
        ///     ---
        ///     true - the object exists in the cache,
        ///     false - object does not exist
        /// </returns>
        public virtual bool Exists(long id)
        {
            return dataByID.ContainsKey(id);
        }

    }

}
