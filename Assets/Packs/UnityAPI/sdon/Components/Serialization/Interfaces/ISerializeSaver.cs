using System;
using System.Collections.Generic;

namespace Engine.Serialization {

	/// <summary>
	/// Общий интерфейс сохранения сериализованного словаря
	/// </summary>
	/// <typeparam name="K">Тип идентификатора сериализованного объекта</typeparam>
	/// <typeparam name="V">Тип сериализованного объекта</typeparam>
	public interface ISerializeSaver<K, V> where V : class {

		/// <summary>
		/// Записывает словать dictionary по пути коллекции path
		/// </summary>
		/// <param name="dictionary">Словарь, из которого запишутся данные</param>
		/// <param name="path">Путь, куда будут писаться данные (URL, Путь к файлу, Путь к базе данных и т.д.)</param>
		void SaveDictionary(IDictionary<K, V> dictionary, string path);

	}

}
