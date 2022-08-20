using System.Collections.Generic;

namespace Engine.Data.Factories
{

    public interface IActionFactory<T> where T : class, IActionItem
    {

        ICollection<T> Load();

        T Get(string id);

    }

}
