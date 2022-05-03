using System.Collections.Generic;

namespace UnityEngine
{
    
    public static class Sets
    {

        public static bool IsEmpty<T>(ISet<T> set)
        {
            return set == null || set.Count == 0;
        }
        
    }
    
}