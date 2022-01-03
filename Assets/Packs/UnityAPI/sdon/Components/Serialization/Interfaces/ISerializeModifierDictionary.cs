using System;
using System.Collections.Generic;

namespace Engine.Serialization {

	public interface ISerializeModifierDictionary<K,V> where V : class {

		/// <summary>
		/// Добавляет экземпляр объекта в коллектор
		/// </summary>
		/// <param name="item">Добваляемый экземпляр объекта</param>
		void Put(V item);

		/// <summary>
		/// Возвращает число элементов в коллекторе
		/// </summary>
		int Count {
			get;
		}

	}

}
