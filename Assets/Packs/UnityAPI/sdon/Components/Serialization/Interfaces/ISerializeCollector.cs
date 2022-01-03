using System;
using System.Collections.Generic;

namespace Engine.Serialization {

	/// <summary>
	/// Общий интерфейс коллектора-сериализатора
	/// </summary>
	/// <typeparam name="K">Тип ключа</typeparam>
	/// <typeparam name="V">Тип элемента коллекции</typeparam>
	public interface ISerializeCollector<K,V> where V : class {

		/// <summary>
		/// Возвращает элемент коллекции по ключу
		/// </summary>
		/// <param name="key">Ключ элемента</param>
		/// <returns>Элемент коллекции</returns>
		V Get(K key);

		/// <summary>
		/// Возвращает ключ по значению
		/// </summary>
		/// <param name="value">Значение, ключ которого надо вернуть</param>
		/// <returns>Возвращает ключ</returns>
		K ToKey(V value);

	}

}
