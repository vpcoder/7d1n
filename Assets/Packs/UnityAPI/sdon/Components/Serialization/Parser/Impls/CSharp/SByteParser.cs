using System;

namespace Engine.Serialization.Impls {

	public class SByteParser : IParse {

		/// <summary>
		/// Проверяет, может ли этот парсер распознать данный тип
		/// </summary>
		/// <param name="type">Тип который надо распозпонать</param>
		/// <returns>Возвращает логическое значение, возможно ли распарсить этот объект</returns>
		public bool CanParse(Type type) {
			return type.Name == "SByte";
		}

		/// <summary>
		/// Парсит указанный объект
		/// </summary>
		/// <param name="item">Сериализованные данные объекта</param>
		/// <param name="type">Тип объекта</param>
		/// <returns>Возвращает экземпляр объекта, с заполненными данными</returns>
		public object Parse(string item, Type type) {
			return SByte.Parse(item);
		}

	}

}
