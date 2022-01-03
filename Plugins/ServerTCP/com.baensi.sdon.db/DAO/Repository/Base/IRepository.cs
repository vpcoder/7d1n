using com.baensi.sdon.db.entity;
using System;
using System.Collections.Generic;

namespace com.baensi.sdon.db.dao.repository
{

    public interface IRepository<T> where T : class, IEntity, new()
    {
        void Reload();

        int[] Insert(IEnumerable<T> items);

        void Update(IEnumerable<T> items);

        void Delete(params int[] ids);

        int Insert(T item);

        void Update(T item);

        T Get(int id);

        T GetFirst(Func<T, bool> predicate);

        IEnumerable<T> GetWhere(Func<T, bool> predicate);

        IEnumerable<T> GetAll();
    }

}
