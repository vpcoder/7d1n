using System;
using UnityEngine;
using System.Collections.Generic;

namespace Engine.Serialization.Impls {

	/// <summary>
	/// Коллектор-сериализатор, хранится только в оперативной памяти.
	/// Удобен для случаев, когда данные не требуется откуда либо читать, или куда либо записывать.
	/// </summary>
	/// <typeparam name="K">Тип ключа</typeparam>
	/// <typeparam name="V">Тип значения</typeparam>
	public abstract class CollectorVirtual<K, V> : SerializeCollector<K, V>, ISerializeModifierDictionary<K,V>
		where V : class {

		public CollectorVirtual() { }

#pragma warning disable 809, 612, 618

		[Obsolete("Не применимо для этого класса",true)]
		public sealed override void LoadDictionary(ref IDictionary<K, V> dictionary, string path) {
#if UNITY_EDITOR
			Debug.LogError("Виртуальный коллектор не может быть загружен или сохранён куда либо!");
#endif
			throw new Exception("Метод не применим для этого класса");
		}

		[Obsolete("Не применимо для этого класса", true)]
		public sealed override void SaveDictionary(IDictionary<K, V> dictionary, string path) {
#if UNITY_EDITOR
			Debug.LogError("Виртуальный коллектор не может быть загружен или сохранён куда либо!");
#endif
			throw new Exception("Метод не применим для этого класса");
		}

#pragma warning restore 809, 612, 618

		/// <summary>
		/// Добавляет экземпляр действия в коллектор
		/// </summary>
		/// <param name="activity">Добваляемый экземпляр действия</param>
		public void Put(V item) {
			collector.Add(ToKey(item),item);
		}

		/// <summary>
		/// Возвращает число элементов в коллекторе
		/// </summary>
		public int Count {
			get {
				return collector.Count;
			}
		}

	}

}
