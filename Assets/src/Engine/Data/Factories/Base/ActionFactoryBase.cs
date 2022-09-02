using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data.Factories
{

    public abstract class ActionFactoryBase<T> : IActionFactory<T> where T : class, IActionItem
    {

        private readonly IDictionary<string, T> actions = new Dictionary<string, T>();

        public ICollection<T> Load()
        {
            return AssembliesHandler.CreateImplementations<T>();
        }

        public ActionFactoryBase()
        {
            foreach(var action in Load())
                actions.Add(action.ID, action);
        }

        public T Get(string id)
        {
            if (actions.TryGetValue(id, out T result))
                return result;
            throw new KeyNotFoundException("id: " + id);
        }

    }

}
