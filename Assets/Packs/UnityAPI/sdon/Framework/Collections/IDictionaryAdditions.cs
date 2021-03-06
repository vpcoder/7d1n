using System.Collections.Generic;

namespace UnityEngine
{

    public static class IDictionaryAdditions
    {

        public static void AddInToDictionary<K, T, V>(this IDictionary<K, IDictionary<T, V>> dictionary, K key, T otherKey, V value)
        {
            if (!dictionary.TryGetValue(key, out IDictionary<T, V> list))
            {
                list = new Dictionary<T, V>();
                dictionary.Add(key, list);
            }
            list.Add(otherKey, value);
        }

        public static void AddInToDictionary<K, T, V>(this IDictionary<K, Dictionary<T, V>> dictionary, K key, T otherKey, V value)
        {
            if (!dictionary.TryGetValue(key, out Dictionary<T, V> list))
            {
                list = new Dictionary<T, V>();
                dictionary.Add(key, list);
            }
            list.Add(otherKey, value);
        }

        public static void AddInToList<K,V>(this IDictionary<K, IList<V>> dictionary, K key, V value)
        {
            if (!dictionary.TryGetValue(key, out IList<V> list))
            {
                list = new List<V>();
                dictionary.Add(key, list);
            }
            list.Add(value);
        }

        public static void AddInToList<K, V>(this IDictionary<K, List<V>> dictionary, K key, V value)
        {
            if (!dictionary.TryGetValue(key, out List<V> list))
            {
                list = new List<V>();
                dictionary.Add(key, list);
            }
            list.Add(value);
        }

        public static void AddInToSet<K, V>(this IDictionary<K, ISet<V>> dictionary, K key, V value)
        {
            if (!dictionary.TryGetValue(key, out ISet<V> set))
            {
                set = new HashSet<V>();
                dictionary.Add(key, set);
            }
            set.Add(value);
        }

        public static void AddInToSet<K, V>(this IDictionary<K, HashSet<V>> dictionary, K key, V value)
        {
            if (!dictionary.TryGetValue(key, out HashSet<V> set))
            {
                set = new HashSet<V>();
                dictionary.Add(key, set);
            }
            set.Add(value);
        }

    }

}
