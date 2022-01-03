using com.baensi.sdon.db.entity;
using System.Collections.Generic;

namespace com.baensi.sdon.db.dao.repository
{

    public interface IDbAccess<T> where T : class, IEntity, new()
    {
        void CreateTable();

        void Delete(params int[] ids);

        int[] Insert(IEnumerable<T> items);

        int Insert(T item);

        void Update(IEnumerable<T> items);

        void Update(T item);

        T Get(int id);

        IEnumerable<T> GetAll();
    }

}
