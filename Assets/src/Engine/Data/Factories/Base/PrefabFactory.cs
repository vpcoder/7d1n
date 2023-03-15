using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data.Factories
{

    /// <summary>
    /// 
    /// Базовый класс фабрики префабов
    /// Префабы храняться в папке Resources/[Directory]
    /// При загрузке префабы кешируются
    /// ---
    /// The base class of the prefab factory
    /// The prefabs are stored in the Resources/[Directory] folder
    /// When loaded, the prefabs are cached
    /// 
    /// </summary>
    /// <typeparam name="T">
    ///     Тип хранимых объектов-префабов в фабрике
    ///     ---
    ///     The type of stored prefab objects in the factory
    /// </typeparam>
    public abstract class PrefabFactory<T> : IPrefabFactory<T> where T : Object
    {

        /// <summary>
        ///     Коллекция кешированных префабов
        ///     ---
        ///     A collection of cached prefabs
        /// </summary>
        private IDictionary<string, T> data = new Dictionary<string, T>();

        /// <summary>
        ///     Адрес дирректории, где находятся префабы данной фабрики
        ///     ---
        ///     The address of the dirrectory where the prefabs of this factory are located
        /// </summary>
        public abstract string Directory { get; }

        /// <summary>
        ///     Возвращает экземпляр префаба по его текстовому идентификатору
        ///     ---
        ///     Returns an instance of a prefab by its text identifier
        /// </summary>
        /// <param name="id">
        ///     Идентификатор префаба
        ///     ---
        ///     Prefab ID
        /// </param>
        /// <returns>
        ///     Загруженный (кешированный) экземпляр префаба
        ///     ---
        ///     Loaded (cached) instance of the prefab
        /// </returns>
        public T Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;
            return GetRaw(Directory + "/" + id);
        }
        
        public T GetRaw(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;
            
            T result = null;
            if (!data.TryGetValue(id, out result))
            {
                result = Resources.Load<T>(id);
                if (result == null)
                {
                    Debug.LogError("prefab '" + GetType().Name + "' with id '" + id + "' - not founded!");
                }
                data.Add(id, result);
            }
            return result;
        }

        /// <summary>
        ///     Создаёт экземпляр объекта в мире
        ///     ---
        ///     Creates an object instance in the world
        /// </summary>
        /// <param name="id">
        ///     Идентификатор префаба
        ///     ---
        ///     Prefab ID
        /// </param>
        /// <param name="position">
        ///     Расположение в мировых координатах
        ///     ---
        ///     Location in world coordinates
        /// </param>
        /// <returns>
        ///     Экземпляр созданного объекта
        ///     ---
        ///     An instance of the created object
        /// </returns>
        public T CreatePrefabInstance(string id, Vector3 position)
        {
            var explode = Get(id);
            if (explode == null)
                return null;
            return GameObject.Instantiate(explode, position, Quaternion.identity);
        }

    }

}
