using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data.Factories
{

    public abstract class ActionFactoryBase<T> : IActionFactory<T> where T : class, IActionItem
    {

        private readonly IDictionary<string, T> actions = new Dictionary<string, T>();

        public IList<T> Load()
        {
            var list = new List<T>();
            try
            {
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (!typeof(T).IsAssignableFrom(type) || type.IsAbstract || type.IsNotPublic)
                            continue;
                        list.Add((T)Activator.CreateInstance(type));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
            return list;
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
