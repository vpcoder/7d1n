using System;
using System.Collections.Generic;

namespace com.baensi.sdon.db.dao.repository
{

    public interface ICache<T> where T : class, new()
    {

        IEnumerable<T> Data { get; }
        int Count { get; }

        void SetCache(IEnumerable<T> data);
        void Clear();

        T Get(int key);

        bool Contains(int key);
        bool Contains(T item);

        T GetFirst(Func<T, bool> filter);

        IEnumerable<T> InsertOrUpdate(IEnumerable<T> items);
        T InsertOrUpdate(T item);

        int Remove(IEnumerable<int> keys);
        int Remove(IEnumerable<T> items);
        int Remove(int key);
        int Remove(T item);

    }

}
