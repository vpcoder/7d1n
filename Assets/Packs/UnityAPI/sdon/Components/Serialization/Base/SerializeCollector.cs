using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Serialization {

	/// <summary>
	/// Абстрактный класс-сериализатор/десериализатор коллекций
	/// </summary>
	/// <typeparam name="K">Тип ключа элемента коллекции</typeparam>
	/// <typeparam name="V">Тип значения элемента коллекции</typeparam>
	public abstract class SerializeCollector<K, V> :
		ISerializeLoader<K, V>,
		ISerializeSaver<K, V>,
		ISerializeCollector<K, V>
			where V : class {

		#region Hidden Fields

		/// <summary>
		/// Хранит словарь-коллектор
		/// </summary>
		protected IDictionary<K, V> collector = new SortedDictionary<K,V>();

		#endregion


		#region Serialize Methods

		/// <summary>
		/// Возвращает ключ по значению
		/// </summary>
		/// <param name="value">Значение, ключ которого надо вернуть</param>
		/// <returns>Возвращает ключ</returns>
		public abstract K ToKey(V value);

		/// <summary>
		/// Загружает словать dictionary по пути коллекции path
		/// </summary>
		/// <param name="dictionary">Словарь, в который загрузятся данные</param>
		/// <param name="path">Путь, откуда будут браться данные (URL, Путь к файлу, Путь к базе данных и т.д.)</param>
		public abstract void LoadDictionary(ref IDictionary<K, V> dictionary, string path);

		/// <summary>
		/// Записывает словать dictionary по пути коллекции path
		/// </summary>
		/// <param name="dictionary">Словарь, из которого запишутся данные</param>
		/// <param name="path">Путь, куда будут писаться данные (URL, Путь к файлу, Путь к базе данных и т.д.)</param>
		public abstract void SaveDictionary(IDictionary<K, V> dictionary, string path);

		#endregion


		#region Collectors Methods

		/// <summary>
		/// Возвращает элемент коллекции по ключу
		/// </summary>
		/// <param name="key">Ключ элемента</param>
		/// <returns>Элемент коллекции</returns>
		/// <exception cref="System.ArgumentNullException">ArgumentNullException</exception>
		/// <exception cref="System.Collections.Generic.KeyNotFoundException">KeyNotFoundException</exception>
		public V Get(K key) {

#if UNITY_EDITOR

			if (key == null) {
				throw new ArgumentNullException();
			}

			if (!collector.ContainsKey(key)) {
				
				Debug.LogError("Ошибка доступа к элементу '"+key.ToString()+"'! Коллектор не содержит указанного значения!");
				throw new KeyNotFoundException();

			}

#endif

			return collector[key];

		}

		#endregion

	}

}
