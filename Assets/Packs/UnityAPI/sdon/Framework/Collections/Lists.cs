using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace UnityEngine
{
    
    public static class Lists
    {

        public static List<T> newArrayList<T>(params T[] items)
        {
            return new List<T>(items);
        }

        public static bool IsEmpty<T>(ICollection<T> list)
        {
            return list == null || list.Count == 0;
        }
        
        public static bool DropFirst<T>(IList<T> list) {
            if(IsEmpty(list)){
                return false;
            }
            list.RemoveAt(0);
            return true;
        }
		
        public static bool DropLast<T>(IList<T> list) {
            if(IsEmpty(list)) {
                return false;
            }
            list.RemoveAt(list.Count-1);
            return true;
        }
		
        public static T First<T>(IList<T> list, T defaultValue) where T : struct {
            return IsEmpty(list) ? defaultValue : list[0];
        }
				
        public static T Last<T>(IList<T> list, T defaultValue) where T : struct {
            return IsEmpty(list) ? defaultValue : list[list.Count - 1];
        }
        
        public static T First<T>(IList<T> list) where T : class {
            return IsEmpty(list) ? null : list[0];
        }
				
        public static T Last<T>(IList<T> list) where T : class {
            return IsEmpty(list) ? null : list[list.Count - 1];
        }
        
    }
    
}