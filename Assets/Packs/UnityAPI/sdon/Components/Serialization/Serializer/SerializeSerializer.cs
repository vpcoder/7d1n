using System;
using System.Collections.Generic;

namespace Engine.Serialization {

	/// <summary>
	/// Парсер сериализованных данных
	/// </summary>
	public class SerializeSerializer {

		#region Hidden Fields

		/// <summary>
		/// Список всех возможных сериализаторов данных
		/// </summary>
		private static List<ISerializer> serializerList = new List<ISerializer>();

		#endregion


		/// <summary>
		/// Инициализатор списка парсеров
		/// </summary>
		static SerializeSerializer() {

			#region Collections

			serializerList.Add(new CSharpListSerializer());

			#endregion

			#region Default Serializer

			serializerList.Add(new MainSerializer());

			#endregion

		}

		#region API Functions

		/// <summary>
		/// Функция пытается парсить строку item, и привести её к типу type
		/// </summary>
		/// <param name="item">Строка с данными, которые надо распарсить</param>
		/// <param name="type">Тип данных к которому надо примести строку</param>
		/// <returns>Возвращает экземпляр объекта типа type</returns>
		/// <exception cref="System.Exception">Exception</exception>
		/// <example>
		/// Пример использования:
		/// <code>
		///		string value  = SerializeParser.Parse("1.45",typeof(float));
		///		string vector = SerializeParser.Parse("(0.0,0.45,0.32)",typeof(Vector3));
		/// </code>
		/// </example>
		public static string Serialize(object instance, Type type) {
			if (instance == null) {
				return null;
			}
			if (type == null) {
				throw new ArgumentNullException();
			}
			foreach (ISerializer serializer in serializerList) {
				if (serializer.CanSerialize(type)) {
					return serializer.Serialize(instance, type);
				}
			}
			throw new Exception();
		}

		#endregion

	}

}
