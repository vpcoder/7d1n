using Engine.DB;
using System.Collections.Generic;

namespace Engine.Collections
{

    public interface IDbCollection<T> where T : IDto, new()
    {

        void PreConstruct();

        void PostConstruct();

        List<T> GetAll();

        T Get(long id);

        int Save(T item);

        int Delete(long id);

        int DeleteQuery(string whereQuery, params object[] args);

        List<T> Query(string whereQuery, params object[] args);

        long NextID { get; }

        bool IsExists(long id);

    }

}
