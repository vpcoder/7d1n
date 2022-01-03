using com.baensi.sdon.db.entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.baensi.sdon.db.dao.repository
{

    public abstract class RepositoryBase<T> : IRepository<T> where T : class, IEntity, new()
    {

        #region Hidden Fields

        protected IDbAccess<T> _dbAccess;
        protected ICache<T> _cache = new CacheData<T>();

        #endregion

        #region Ctors

        public RepositoryBase(IDbAccess<T> dbAccess) : base()
        {
            this._dbAccess = dbAccess;
            this._dbAccess.CreateTable();
            Reload();
        }

        #endregion

        public ICache<T> Cache
        {
            get
            {
                return this._cache;
            }
        }

        public void Reload()
        {
            _cache.SetCache(_dbAccess.GetAll());
        }

        public T Get(int id)
        {
            var tmp = _cache.Get(id);

            if (tmp == null)
            {
                tmp = _dbAccess.Get(id);

                if (tmp == null)
                    return null;

                _cache.InsertOrUpdate(tmp);
            }

            return tmp;
        }

        public T GetFirst(Func<T, bool> predicate)
        {
            foreach(var item in GetAll())
            {
                if (predicate(item))
                    return item;
            }
            return null;
        }

        public IEnumerable<T> GetWhere(Func<T, bool> predicate)
        {
            var list = new List<T>();
            foreach (var item in GetAll())
            {
                if (predicate(item))
                    list.Add(item);
            }
            return list;
        }

        public IEnumerable<T> GetAll()
        {
            return _cache.Data;
        }

        public int[] Insert(IEnumerable<T> items)
        {
            var list = items.ToList();
            var ids = _dbAccess.Insert(list);

            for (int i = list.Count - 1; i >= 0; i--)
                list[i].Id = ids[i];

            _cache.InsertOrUpdate(list);

            return ids;
        }

        public int Insert(T item)
        {
            var id = _dbAccess.Insert(item);
            item.Id = id;
            _cache.InsertOrUpdate(item);
            return id;
        }

        public void Update(IEnumerable<T> items)
        {
            var list = items.ToList();
            _dbAccess.Update(list);
            _cache.InsertOrUpdate(list);
        }

        public void Update(T item)
        {
            _dbAccess.Update(item);
            _cache.InsertOrUpdate(item);
        }

        public void Delete(params int[] ids)
        {
            _dbAccess.Delete(ids);
            _cache.Remove(ids);
        }

    }

}
