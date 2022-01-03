using com.baensi.sdon.db.entity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace com.baensi.sdon.db.dao.repository
{

    public class CacheData<T> : ICache<T> where T : class, IEntity, new()
    {

        #region Hidden Fields

        /// <summary>
        /// Коллекция для кэша
        /// </summary>
        private IDictionary<int, T> _data = new Dictionary<int, T>();

        #endregion

        #region Ctors

        public CacheData() : base()
        { }

        #endregion

        #region Properties

        public IEnumerable<T> Data
        {
            get
            {
                return _data.Values;
            }
        }

        public int Count
        {
            get
            {
                return _data.Count;
            }
        }

        #endregion

        public T GetFirst(Func<T, bool> filter)
        {
            foreach (var item in _data.Values)
            {
                if (filter(item))
                    return item;
            }
            return null;
        }

        public bool Contains(int key)
        {
            return _data.ContainsKey(key);
        }

        public bool Contains(T item)
        {
            return _data.ContainsKey(item.Id);
        }

        public T Get(int key)
        {
            T item = null;
            _data.TryGetValue(key, out item);
            return item;
        }

        public IEnumerable<T> InsertOrUpdate(IEnumerable<T> items)
        {
            var _result = new List<T>();

            foreach (var item in items)
            {
                var tmp = Get(item.Id);

                if (tmp == null)
                    tmp = item;
                else
                    EntityHandler.Copy<T>(item, tmp);

                _result.Add(tmp);
                _data.Add(tmp.Id, tmp);
            }

            return _result;
        }

        public int Remove(IEnumerable<int> keys)
        {
            int count = 0;

            foreach (int key in keys)
                count += this._data.Remove(key) ? 1 : 0;

            return count;
        }

        public int Remove(IEnumerable<T> items)
        {
            return Remove(items.Select(o => o.Id));
        }

        public void SetCache(IEnumerable<T> data)
        {
            Clear();

            foreach(var item in data)
                this._data.Add(item.Id, item);
        }

        public void Clear()
        {
            _data.Clear();
        }

        public T InsertOrUpdate(T item)
        {
            return InsertOrUpdate(new T[] { item }).First();
        }

        public int Remove(int key)
        {
            return this._data.Remove(key) ? 1 : 0;
        }

        public int Remove(T item)
        {
            return Remove(item.Id);
        }
    }

}
