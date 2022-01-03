using System;
using System.Collections;
using UnityEngine;

namespace Engine.Serialization.Impls {

	public class CSharpListParser : IParse {

		/// <summary>
		/// Проверяет, может ли этот парсер распознать данный тип
		/// </summary>
		/// <param name="type">Тип который надо распозпонать</param>
		/// <returns>Возвращает логическое значение, возможно ли распарсить этот объект</returns>
		public bool CanParse(Type type) {
			return type.Name == "List`1" && type.Namespace=="System.Collections.Generic";
		}

		/// <summary>
		/// Парсит указанный объект
		/// </summary>
		/// <param name="item">Сериализованные данные объекта</param>
		/// <param name="type">Тип объекта</param>
		/// <returns>Возвращает экземпляр объекта, с заполненными данными</returns>
		public object Parse(string value, Type type) {

			string[] data  = value.Split(':');

			string[] types = data[0].Split('^');
			string[] items = data[1].Split(';');

			if (types.Length!=2) {
				return null;
			}

			//string nameListType  = SerializeHandler.GetName(types[0]);
			string nameItemsType = SerializeHandler.GetName(types[1]);

			Type listType     = typeof(System.Collections.Generic.List<>);
			Type itemsType    = Type.GetType(nameItemsType, true);
			Type concreteType = listType.MakeGenericType(new Type[] { itemsType });

			IList list = (IList)Activator.CreateInstance(concreteType);

			foreach (string item in items) {
				string itemData = SerializeHandler.GetOriginalData(item);
				list.Add(SerializeSerializer.Serialize(itemData, itemsType));
			}

			return list;
		}

	}

}
