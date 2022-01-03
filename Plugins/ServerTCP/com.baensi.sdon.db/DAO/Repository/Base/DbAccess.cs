using com.baensi.sdon.db.dao.generator;
using com.baensi.sdon.db.entity;
using System.Collections.Generic;

namespace com.baensi.sdon.db.dao.repository
{

    public abstract class DbAccess<T> : IDbAccess<T> where T : class, IEntity, new()
    {

        #region Hidden Fields

        /// <summary>
        /// Генератор уникальных идентификаторов для таблицы
        /// </summary>
        protected IGenerator _generator;

        #endregion

        #region Ctors

        public DbAccess(string table)
        {
            _generator = new DefaultIdGenerator(table);
        }

        #endregion

        public virtual void CreateTable() { }

        public abstract void Delete(params int[] ids);
        public abstract T Get(int id);
        public abstract IEnumerable<T> GetAll();
        public abstract int[] Insert(IEnumerable<T> items);
        public abstract int Insert(T item);
        public abstract void Update(IEnumerable<T> items);
        public abstract void Update(T item);

    }

}
