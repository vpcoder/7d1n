using com.baensi.sdon.db.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.baensi.sdon.server.cache
{

    public interface IDataDictionary
    { }

    public interface IDataDictionary<T> : IDataDictionary where T : class, new() 
    {

        int Count { get; }

        T Get(int id);

        T GetFirst(Func<T, bool> predicate);

        IEnumerable<T> GetWhere(Func<T, bool> predicate);

        T AutoUpdate(int id);

        T EagerUpdate(int id);

    }

}
