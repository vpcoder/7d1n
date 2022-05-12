using System;
using System.Collections.Generic;

namespace UnityEngine {

    /// <summary>
    /// Класс-расширение **TransformAddition** для Transform объекта Юнити 
    /// Авторы: sdon
    /// Дата: 16.10.2016
    /// Обновление: 14.02.2017
    /// Версия: 1.00.002
    /// </summary>
    public static class TransformAddition {

		public delegate void AsyncCallback();

		/// <summary>
		/// Асинхронно удаляет все дочерние элементы внутри Transform
		/// </summary>
		/// <example>
		/// Пример использования:
		/// <code>
		/// transform.DestroyAllChilds(); // удаляет все дочерние элементы
		/// </code>
		/// </example>
		public static void DestroyAllChildsEditor(this Transform transform) {
			DestroyAllChildsEditor(transform,null);
		}

		/// <summary>
		/// Асинхронно удаляет все дочерние элементы внутри Transform
		/// </summary>
		/// <param name="callback">По завершению удаления вызывается этот делегат</param>
		/// <example>
		/// Пример использования 1:
		/// <code>
		/// transform.DestroyAllChilds(() => { // удаляет все дочерние элементы
		///		...
		///		DoSomething(); // что-то делает, после того как все элементы удалились
		///		...
		/// };
		/// </code>
		/// Пример использования 2:
		/// <code>
		/// private void DoSomething() { // что-то делает, после того как все элементы удалились
		///		...
		/// }
		/// ...
		/// transform.DestroyAllChilds(DoSomething); // удаляет все дочерние элементы, после чего вызывает DoSomething
		/// </code>
		/// Пример использования 3:
		/// <code>
		/// transform.DestroyAllChilds(null); // удаляет все дочерние элементы (вместо подобного, рекомендуется использовать перегруженный вариант - "transform.DestroyAllChilds();")
		/// </code>
		/// </example>
		public static void DestroyAllChildsEditor(this Transform transform, AsyncCallback callback) {

            if(transform == null)
            {
                Debug.LogError("null!");
                return;
            }

			if (transform.childCount == 0) { // смотрим, есть ли дочерние элементы
				if (callback != null) {
					callback(); // если каллбек был задан, вызывает делегата
				}
				return;
			}

#if UNITY_EDITOR

			UnityEditor.EditorApplication.delayCall += () => { // отложенный деструктор
				GameObject.DestroyImmediate(transform.GetChild(0).gameObject); // удаляем объект в режиме редактора

				if (transform.childCount > 0) { // этот дочерний элемент был не последним
					DestroyAllChilds(transform,callback); // повторяем операцию
					return;
				}

				if (callback != null) {
					callback(); // если каллбек был задан, вызывает делегата
				}

			};

#else

			while(transform.childCount > 0){
				GameObject.Destroy(transform.GetChild(0).gameObject); // удаляем элемент
			}

			if (callback != null) {
				callback();
			}
			
#endif

		}


        /// <summary>
        /// Асинхронно удаляет все дочерние элементы внутри Transform
        /// </summary>
        /// <example>
        /// Пример использования:
        /// <code>
        /// transform.DestroyAllChilds(); // удаляет все дочерние элементы
        /// </code>
        /// </example>
        public static void DestroyAllChilds(this Transform transform)
        {
            DestroyAllChilds(transform, null);
        }

        /// <summary>
        /// Асинхронно удаляет все дочерние элементы внутри Transform
        /// </summary>
        /// <param name="callback">По завершению удаления вызывается этот делегат</param>
        /// <example>
        /// Пример использования 1:
        /// <code>
        /// transform.DestroyAllChilds(() => { // удаляет все дочерние элементы
        ///		...
        ///		DoSomething(); // что-то делает, после того как все элементы удалились
        ///		...
        /// };
        /// </code>
        /// Пример использования 2:
        /// <code>
        /// private void DoSomething() { // что-то делает, после того как все элементы удалились
        ///		...
        /// }
        /// ...
        /// transform.DestroyAllChilds(DoSomething); // удаляет все дочерние элементы, после чего вызывает DoSomething
        /// </code>
        /// Пример использования 3:
        /// <code>
        /// transform.DestroyAllChilds(null); // удаляет все дочерние элементы (вместо подобного, рекомендуется использовать перегруженный вариант - "transform.DestroyAllChilds();")
        /// </code>
        /// </example>
        public static void DestroyAllChilds(this Transform transform, AsyncCallback callback)
        {

            if (transform == null)
            {
                Debug.LogError("null!");
                return;
            }

            if (transform.childCount == 0)
            { // смотрим, есть ли дочерние элементы
                if (callback != null)
                {
                    callback(); // если каллбек был задан, вызывает делегата
                }
                return;
            }

            for(int i = transform.childCount-1; i>=0; i--)
            {
				GameObject.Destroy(transform.GetChild(i).gameObject); // удаляем элемент
			}

			if (callback != null) {
				callback();
			}

        }

        /// <summary>
        /// Удаляет первый дочерний элемент у Transform-а
        /// </summary>
        /// <param name="transform"></param>
        /// <example>
        /// Пример использования:
        /// <code>
        /// transform.DestroyFirstChild(); // удаляет первый дочерний элемент
        /// </code>
        /// </example>
        public static void DestroyFirstChild(this Transform transform) {
			DestroyFirstChild(transform,null);
		}

		/// <summary>
		/// Удаляет первый дочерний элемент у Transform-а
		/// </summary>
		/// <param name="callback">По завершению удаления элемента вызывается этот делегат</param>
		/// <example>
		/// Пример использования 1:
		/// <code>
		/// transform.DestroyFirstChild(() => { // удаляет первый дочерний элемент
		///		...
		///		DoSomething(); // что-то делает, после того как элемент удалился
		///		...
		/// });
		/// </code>
		/// Пример использования 2:
		/// <code>
		/// private void DoSomething() { // что-то делает, после того как первый дочерний элемент удалился
		///		...
		/// }
		/// ...
		/// transform.DestroyFirstChild(DoSomething); // удаляет первый дочерний элемент, после чего вызывает DoSomething
		/// </code>
		/// Пример использования 3:
		/// <code>
		/// transform.DestroyFirstChild(null); // удаляет первый дочерний элемент (вместо подобного, рекомендуется использовать перегруженный вариант - "transform.DestroyFirstChild();")
		/// </code>
		/// </example>
		/// </example>
		public static void DestroyFirstChild(this Transform transform, AsyncCallback callback) {

			if (transform.childCount == 0) { // смотрим, есть ли дочерние элементы
				if (callback != null) {
					callback(); // если каллбек был задан, вызывает делегата
				}
				return;
			}

#if UNITY_EDITOR

			UnityEditor.EditorApplication.delayCall += () => { // отложенный деструктор
				GameObject.DestroyImmediate(transform.GetChild(0).gameObject); // удаляем первый элемент в режиме редактора
				if (callback != null) {
					callback(); // Если был задан каллбек, вызываем делегата
				}
			};

#else

			GameObject.Destroy(transform.GetChild(0).gameObject); // удаляем элемент
			if (callback != null) {
				callback();
			}

#endif

		}

		public static ICollection<T> GetChildComponents<T>(this Transform transform)
		{
			var list = new List<T>();
			var parentComponent = transform.GetComponent<T>();
			if(parentComponent != null)
				list.Add(parentComponent);
			foreach (var child in transform.Childs())
				list.AddRange(GetChildComponents<T>(child));
			return list;
		}

		/// <summary>
		/// Enumerable для дочерних элементов Transform объекта. Позволяет перебирать дочерние элементы в цикле.
		/// </summary>
		/// <returns>Возвращает именованный итератор для перебора дочерних элементов Transform объекта</returns>
		/// <example>
		/// Пример использования:
		/// <code>
		/// foreach(Transform child in transform.Childs()) {
		///		...
		/// }
		/// </code>
		/// </example>
		public static System.Collections.Generic.IEnumerable<Transform> Childs(this Transform transform) {
			for (int i = 0; i < transform.childCount; i++) {
				yield return transform.GetChild(i);
			}
		}

		/// <summary>
		/// Enumerable для активных дочерних элементов Transform объекта. Позволяет перебирать дочерние элементы в цикле.
		/// </summary>
		/// <returns>Возвращает именованный итератор для перебора активных дочерних элементов Transform объекта</returns>
		/// <example>
		/// Пример использования:
		/// <code>
		/// foreach(Transform child in transform.ActiveChilds()) {
		///		...
		/// }
		/// </code>
		/// </example>
		public static System.Collections.Generic.IEnumerable<Transform> ActiveChilds(this Transform transform) {
			Transform[] items = transform.GetComponentsInChildren<Transform>();
			for (int i = 0; i < items.Length; i++) {
				yield return items[i];
			}
		}

		/// <summary>
		/// Возвращает "путь" к объекту Transform в дереве объектов
		/// </summary>
		/// <param name="transform"></param>
		/// <returns>"Путь" к объекту Transform в дереве объектов</returns>
		/// <example>
		/// Пример использования:
		/// <code>
		/// string path = transform.GetTransformPath();
		/// </code>
		/// </example>
		public static string GetTransformPath(this Transform transform) {
			string path = transform.name;
			if (transform.parent!=null) {
				path = GetTransformPath(transform.parent) + "/" + path;
			}
			return path;
		}

	}

}
