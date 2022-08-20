
namespace System.Collections.Generic
{
    
    public static class Sets
    {

        public static bool HasIntersect<T>(ISet<T> set0, ISet<T> set1)
        {
            return IsNotEmpty(Intersect(set0, set1));
        }
        
        public static ISet<T> Intersect<T>(ISet<T> set0, ISet<T> set1)
        {
            if (set0 == set1)
                return set0;
            if (set0 == null || set1 == null)
                return null;
            ISet<T> result = new HashSet<T>(set0);
            result.IntersectWith(set1);
            return result;
        }
        
        public static bool IsEmpty<T>(ISet<T> set)
        {
            return set == null || set.Count == 0;
        }
        
        public static bool IsNotEmpty<T>(ISet<T> set)
        {
            return !IsEmpty(set);
        }
        
    }
    
}