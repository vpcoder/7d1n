using System;
using System.Reflection;

namespace Engine.Serialization {

	public class CSharpListSerializer : ISerializer {

		/// <summary>
		/// Проверяет, может ли этот сериализатор распознать данный тип
		/// </summary>
		/// <param name="type">Тип который надо распозпонать</param>
		/// <returns>Возвращает логическое значение, возможно ли сериализовать этот объект</returns>
		public bool CanSerialize(Type type) {
			return type.Name == "List`1" && type.Namespace == "System.Collections.Generic";
		}

		/// <summary>
		/// Сериализует указанный объект
		/// </summary>
		/// <param name="item">Экземпляр объекта с данными</param>
		/// <param name="type">Тип объекта</param>
		/// <returns>Возвращает преобразованные в строку данные</returns>
		public string Serialize(object instance, Type type) {

			Type genericType = instance.GetType().GetGenericArguments()[0];

			string listType  = SerializeHandler.GetName(type);
			string itemsType = SerializeHandler.GetName(genericType);

			string items = listType + "^" + itemsType + ":";

			int count = 0;

			foreach(object item in (System.Collections.IEnumerable)instance) {
				items += SerializeHandler.GetSafeData(SerializeSerializer.Serialize(item, genericType))+";";
				count++;
			}

			if (count > 0) {
				items = items.Substring(0, items.Length - 1);
			}

			return items;
		}

	}

}
