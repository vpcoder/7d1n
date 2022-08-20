namespace UnityEngine
{
    
    public static class Arrays
    {

        public static bool IsEmpty<T>(T[] array)
        {
            return array == null || array.Length == 0;
        }
        
    }
    
}