using Engine.DB;
using System.Collections.Generic;

namespace Engine.Collections
{

    public abstract class DbCollection<T> : IDbCollection<T> where T : Dto, new()
    {

        public DbCollection()
        {
            PreConstruct();
            PostConstruct();
        }

        public virtual void PreConstruct() { }
        public virtual void PostConstruct() { }

        public List<T> GetAll()
        {
            List<T> items = null;
            Db.Instance.Do(connection =>
            {
                items = connection.QueryAll<T>();
            });
            return items;
        }

        public T Get(long id)
        {
            T item = null;

            Db.Instance.Do(connection =>
            {
                item = connection.QueryFirst<T>(id);
            });

            return item;
        }

        public int Save(T item)
        {
            int result = 0;
            Db.Instance.Do(connection =>
            {
                result = connection.InsertOrReplace(item);
            });
            return result;
        }

        public int Delete(long id)
        {
            int result = 0;
            Db.Instance.Do(connection =>
            {
                result = connection.Delete<T>(id);
            });
            return result;
        }

        public int DeleteQuery(string whereQuery, params object[] args)
        {
            int result = 0;
            Db.Instance.Do(connection =>
            {
                var tableInfo = connection.GetMapping<T>();
                var query = "delete from " + tableInfo.TableName + " where " + whereQuery;
                result = connection.Execute(query, args);
            });
            return result;
        }

        public List<T> Query(string whereQuery, params object[] args)
        {
            List<T> result = null;
            Db.Instance.Do(connection =>
            {
                var tableInfo = connection.GetMapping<T>();
                var query = "select * from " + tableInfo.TableName + " where " + whereQuery;
                result = connection.Query<T>(query, args);
            });
            return result;
        }

        public long NextID
        {
            get
            {
                long result = 0;
                Db.Instance.Do(connection =>
                {
                    result = connection.NextID<T>();
                });
                return result;
            }
        }

        public bool IsExists(long id)
        {
            bool exists = false;
            Db.Instance.Do(connection =>
            {
                exists = connection.QueryFirst<T>(id) != null;
            });
            return exists;
        }

    }

}
