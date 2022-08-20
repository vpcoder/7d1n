using System.Collections.Generic;

namespace UnityEngine
{
    
    public static class Maps
    {

        public static bool IsEmpty<K,V>(IDictionary<K,V> dictionary)
        {
            return dictionary == null || dictionary.Count == 0;
        }
        
    }
    
}