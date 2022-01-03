using com.baensi.sdon.db.entity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.baensi.sdon.server.cache
{

    /// <summary>
    /// Базовый класс словаря-кэша
    /// </summary>
    /// <typeparam name="T">Тип сущностей кэша</typeparam>
    public abstract class DataDictionary<T> : IDataDictionary<T> where T : class, new()
    {

        #region Hidden Fields

        /// <summary>
        /// Потокобезопасный словарь
        /// </summary>
        private IDictionary<int, T> _data = new ConcurrentDictionary<int, T>();

        #endregion

        /// <summary>
        /// Количестве элементо в кэше
        /// </summary>
        public int Count
        {
            get
            {
                return _data.Count;
            }
        }

        /// <summary>
        /// Метод должен сформировать запрашиваемую сущность и закешировать её
        /// </summary>
        /// <param name="id">Идентификатор запрашиваемой сущности</param>
        /// <returns>Формирует и возвращает запрашиваемую сущность</returns>
        public abstract T AutoUpdate(int id);

        /// <summary>
        /// Метод по новой формирует запрашиваемую сущность, и обновляет её в коллекции
        /// </summary>
        /// <param name="id">Идентификатор запрашиваемой сущности</param>
        /// <returns>Формирует, обновляет и возвращает запрошенную сущность</returns>
        public abstract T EagerUpdate(int id);

        /// <summary>
        /// Извлекает ранее закешированную запрашиваемую сущность (если такой не было закешировано, формирует её и кеширует)
        /// </summary>
        /// <param name="id">Идентификатор запрашиваемой сущности</param>
        /// <returns>Возвращает запрашиваемую сущность с соответствующим идентификатором</returns>
        public virtual T Get(int id)
        {
            T tmp = null;

            if (!_data.TryGetValue(id, out tmp))
            {
                tmp = AutoUpdate(id);
                _data.Add(id, tmp);
            }

            return tmp;
        }

        /// <summary>
        /// Выполняет поиск первого попавшего в кэш вложения соответствующего условию отбора (только если оно ранее было добавлено в кэш)
        /// </summary>
        /// <param name="predicate">Условие отбора</param>
        /// <returns>Возвращает отобранную сущность, если ничего нет в кеше, возвращает null</returns>
        public virtual T GetFirst(Func<T,bool> predicate)
        {
            foreach(var item in _data.Values)
            {
                if (predicate(item))
                    return item;
            }

            return null;
        }

        /// <summary>
        /// Выполняет поиск всех попавших в кэш вложений соответствующих условию отбора (только если они были добвалены в кэш ранее)
        /// </summary>
        /// <param name="predicate">Условие отбора</param>
        /// <returns>Возрвщает множество отобранных сущностей, есои ничего нет, вернёт путое множество</returns>
        public virtual IEnumerable<T> GetWhere(Func<T,bool> predicate)
        {
            var list = new List<T>();

            foreach(var item in _data.Values)
            {
                if (predicate(item))
                    list.Add(item);
            }

            return list;
        }

    }

}
