using System.Collections.Generic;

namespace UnityEngine {

    /// <summary>
    /// Класс-расширение **IListAddition** для IList коллекций
    /// Авторы: sdon
    /// Дата: 17.08.2018
    /// Версия: 1.0.0
    /// </summary>
    public static class IListAddition {

		public static bool DropFirst<T>(this IList<T> list) {
			if(list.Count==0){
				return false;
			}
			list.RemoveAt(0);
			return true;
		}
		
		public static bool DropLast<T>(this IList<T> list) {
			if(list.Count==0){
				return false;
			}
			list.RemoveAt(list.Count-1);
			return true;
		}
		
		public static T First<T>(this IList<T> list) where T : class {
			if(list.Count==0){
				return null;
			}
			return list.UnsafeFirst();
		}
				
		public static T Last<T>(this IList<T> list) where T : class {
			if(list.Count==0){
				return null;
			}
			return list.UnsafeLast();
		}

		#region Hidden Methods

		private static T UnsafeFirst<T>(this IList<T> list) {
			return list[0];
		}
		
		private static T UnsafeLast<T>(this IList<T> list) {
			return list[list.Count-1];
		}

		#endregion

	}

}
