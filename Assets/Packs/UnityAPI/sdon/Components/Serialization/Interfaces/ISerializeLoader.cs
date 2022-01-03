using System;
using System.Collections.Generic;

namespace Engine.Serialization {

	/// <summary>
	/// Общий интерфейс загрузки сериализованного словаря
	/// </summary>
	/// <typeparam name="K">Тип идентификатора сериализованного объекта</typeparam>
	/// <typeparam name="V">Тип сериализованного объекта</typeparam>
	public interface ISerializeLoader<K,V> where V : class {

		/// <summary>
		/// Загружает словать dictionary по пути коллекции path
		/// </summary>
		/// <param name="dictionary">Словарь, в который загрузятся данные</param>
		/// <param name="path">Путь, откуда будут браться данные (URL, Путь к файлу, Путь к базе данных и т.д.)</param>
		void LoadDictionary(ref IDictionary<K, V> dictionary, string path);

	}

}
