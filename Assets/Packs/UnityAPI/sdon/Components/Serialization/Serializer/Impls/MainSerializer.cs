using System;

namespace Engine.Serialization {

	public class MainSerializer : ISerializer {

		/// <summary>
		/// Проверяет, может ли этот сериализатор распознать данный тип
		/// </summary>
		/// <param name="type">Тип который надо распозпонать</param>
		/// <returns>Возвращает логическое значение, возможно ли сериализовать этот объект</returns>
		public bool CanSerialize(Type type) {
			return true;
		}

		/// <summary>
		/// Сериализует указанный объект
		/// </summary>
		/// <param name="item">Экземпляр объекта с данными</param>
		/// <param name="type">Тип объекта</param>
		/// <returns>Возвращает преобразованные в строку данные</returns>
		public string Serialize(object instance, Type type) {
			return instance.ToString();
		}

	}

}
