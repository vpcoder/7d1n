using System;
using System.Collections.Generic;
using Engine.Serialization.Impls;

namespace Engine.Serialization {

	/// <summary>
	/// Парсер сериализованных данных
	/// </summary>
	public class SerializeParser {

		#region Hidden Fields

		/// <summary>
		/// Список всех возможных парсеров данных
		/// </summary>
		private static List<IParse> parserList = new List<IParse>();

		#endregion


		/// <summary>
		/// Инициализатор списка парсеров
		/// </summary>
		static SerializeParser() {

			#region CSharp Types

			parserList.Add(new BooleanParser());
			parserList.Add(new ByteParser());
			parserList.Add(new SByteParser());
			parserList.Add(new CharParser());
			parserList.Add(new DecimalParser());
			parserList.Add(new DoubleParser());
			parserList.Add(new SingleParser());
			parserList.Add(new Int16Parser());
			parserList.Add(new Int32Parser());
			parserList.Add(new Int64Parser());
			parserList.Add(new UInt16Parser());
			parserList.Add(new UInt32Parser());
			parserList.Add(new UInt64Parser());
			parserList.Add(new StringParser());

			#endregion

			#region Unity Types

			parserList.Add(new Matrix4x4Parser());
			parserList.Add(new QuaternionParser());
			parserList.Add(new Vector2Parser());
			parserList.Add(new Vector3Parser());
			parserList.Add(new Vector4Parser());

			#endregion

			#region Collections

			parserList.Add(new CSharpListParser());

			#endregion

		}

		#region API Functions

		/// <summary>
		/// Функция пытается парсить строку item, и привести её к типу type
		/// </summary>
		/// <typeparam name="T">Тип к которому приводится результат</typeparam>
		/// <param name="item">Строка с данными, которые надо распарсить</param>
		/// <param name="type">Тип данных к которому надо примести строку</param>
		/// <returns>Возвращает экземпляр объекта типа type</returns>
		/// <exception cref="System.ArgumentNullException">ArgumentNullException</exception>
		/// <exception cref="System.Exception">Exception</exception>
		/// <example>
		/// Пример использования:
		/// <code>
		///		float   value  = SerializeParser.Parse<float>("1.45",typeof(float));
		///		Vector3 vector = SerializeParser.Parse<Vector3>("(0.0,0.45,0.32)",typeof(Vector3));
		/// </code>
		/// </example>
		public static T Parse<T>(string item, Type type) {
			return (T)Parse(item, type);
		}


		/// <summary>
		/// Функция пытается парсить строку item
		/// </summary>
		/// <param name="item">Строка с данными, которые надо распарсить</param>
		/// <param name="type">Тип данных к которому надо примести строку</param>
		/// <returns>Возвращает экземпляр объекта типа type</returns>
		/// <exception cref="System.ArgumentNullException">ArgumentNullException</exception>
		/// <exception cref="System.Exception">Exception</exception>
		/// <example>
		/// Пример использования:
		/// <code>
		///		float   value  = (float)SerializeParser.Parse("1.45",typeof(float));
		///		Vector3 vector = (Vector3)SerializeParser.Parse("(0.0,0.45,0.32)",typeof(Vector3));
		/// </code>
		/// </example>
		public static object Parse(string item, Type type) {
			if (item == null || type == null) {
				throw new ArgumentNullException();
			}
			foreach (IParse parser in parserList) {
				if (parser.CanParse(type)) {
					return parser.Parse(item, type);
				}
			}
			throw new Exception();
		}

		#endregion

	}

}
