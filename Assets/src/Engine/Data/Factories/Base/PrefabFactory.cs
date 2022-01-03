using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data.Factories
{

    /// <summary>
    /// Базовый класс фабрики префабов
    /// </summary>
    /// <typeparam name="T">Тип хранимых объектов-префабов в фабрике</typeparam>
    public abstract class PrefabFactory<T> : IPrefabFactory<T> where T : Object
    {

        /// <summary>
        /// Коллекция кешированных префабов
        /// </summary>
        private IDictionary<string, T> data = new Dictionary<string, T>();

        /// <summary>
        /// Адрес дирректории, где находятся префабы данной фабрики
        /// </summary>
        public abstract string Directory { get; }

        /// <summary>
        /// Возвращает Экземпляр префаба по его текстовому идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Загруженный (кешированный) экземпляр префаба</returns>
        public T Get(string id)
        {
            T result = null;
            if (!data.TryGetValue(id, out result))
            {
                result = Resources.Load<T>("Data/Effects/" + Directory + "/" + id);
                if (result == null)
                {
                    Debug.LogError("prefab '" + GetType().Name + "' with id '" + id + "' - not founded!");
                }
                data.Add(id, result);
            }
            return result;
        }

    }

}
