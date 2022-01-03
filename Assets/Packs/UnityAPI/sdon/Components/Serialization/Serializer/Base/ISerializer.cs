using System;

namespace Engine.Serialization {

	/// <summary>
	/// Интерфейс сериализатора
	/// </summary>
	public interface ISerializer {

		/// <summary>
		/// Проверяет, может ли этот сериализатор распознать данный тип
		/// </summary>
		/// <param name="type">Тип который надо распозпонать</param>
		/// <returns>Возвращает логическое значение, возможно ли сериализовать этот объект</returns>
		bool CanSerialize(Type type);

		/// <summary>
		/// Сериализует указанный объект
		/// </summary>
		/// <param name="item">Экземпляр объекта с данными</param>
		/// <param name="type">Тип объекта</param>
		/// <returns>Возвращает преобразованные в строку данные</returns>
		string Serialize(object item, Type type);

	}

}
