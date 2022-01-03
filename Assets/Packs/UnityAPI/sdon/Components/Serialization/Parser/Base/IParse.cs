using System;

namespace Engine.Serialization {

	/// <summary>
	/// Интерфейс парсера
	/// </summary>
	public interface IParse {

		/// <summary>
		/// Проверяет, может ли этот парсер распознать данный тип
		/// </summary>
		/// <param name="type">Тип который надо распозпонать</param>
		/// <returns>Возвращает логическое значение, возможно ли распарсить этот объект</returns>
		bool CanParse(Type type);

		/// <summary>
		/// Парсит указанный объект
		/// </summary>
		/// <param name="item">Сериализованные данные объекта</param>
		/// <param name="type">Тип объекта</param>
		/// <returns>Возвращает экземпляр объекта, с заполненными данными</returns>
		object Parse(string item, Type type);

	}

}
