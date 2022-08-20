using System;

namespace UnityEngine
{
	
	/// <summary>
	/// Класс-расширение **Enums** для Enum объектов C# 
	/// Авторы: sdon
	/// Дата: 09.16.2022
	/// Версия: 1.0.0
	/// </summary>
	public static class EnumsAdditions
	{
		/// <summary>
		///		Ищет значение подобное value в списке values, если такое есть вернёт true
		/// </summary>
		/// <param name="type">
		///		Значение подобие которого нужно искать в списке
		/// </param>
		/// <param name="values">
		///		Список в котором нужно искать подходящее значение
		/// </param>
		/// <returns>
		///		true - если нашли подходящее значение в списке values, которое эквивалентно значению value
		///		false - если values не задан или пустой, а так же, если не удалось найти ни одного подходящего значения в списке values
		/// </returns>
		public static bool IsOneOf<T>(this T type, params T[] values) where T : struct
		{
			return Enums<T>.IsOneOf(type, values);
		}
	}
	
    /// <summary>
    /// Класс-хандлер **Enums** для Enum объектов C# 
    /// Авторы: sdon
    /// Дата: 19.10.2016
    /// Версия: 1.0.1
    /// </summary>
    public static class Enums<T> where T : struct {

		#region Hidden Fields

		/// <summary>
		/// Сохранённые значения
		/// </summary>
		private static T[] values;
		private static string[] names;

		#endregion

		/// <summary>
		/// Инициализирует параметризированные статические классы Enums
		/// </summary>
		static Enums() {
			values = (T[])Enum.GetValues(typeof(T));
			names  = Enum.GetNames(typeof(T));
		}

		#region Functions

		/// <summary>
		///		Ищет значение подобное value в списке values, если такое есть вернёт true
		/// </summary>
		/// <param name="value">
		///		Значение подобие которого нужно искать в списке
		/// </param>
		/// <param name="values">
		///		Список в котором нужно искать подходящее значение
		/// </param>
		/// <returns>
		///		true - если нашли подходящее значение в списке values, которое эквивалентно значению value
		///		false - если values не задан или пустой, а так же, если не удалось найти ни одного подходящего значения в списке values
		/// </returns>
		public static bool IsOneOf(T value, params T[] values)
		{
			if (values == null || values.Length == 0)
				return false;
			foreach (var item in values)
			{
				if (item.Equals(value))
					return true;
			}
			return false;
		}
		
		/// <summary>
		/// Возвращает массив енумов по типу
		/// </summary>
		/// <typeparam name="T">Тип енума</typeparam>
		/// <returns>Возвращает массив, содержащий все элементы енума</returns>
		/// <exception cref="System.ArgumentNullException">ArgumentNullException</exception>
		/// <exception cref="System.ArgumentException">ArgumentException</exception>
		/// <exception cref="System.Exception">Exception</exception>
		/// <example>
		/// Пример использования:
		/// <code><![CDATA[
		/// public enum MyEnum : int { // тривиальный enum
		///		value1 = 0x00, value2 = 0x01, value3 = 0x02, value4 = 0x03
		/// };
		/// ...
		/// foreach(MyEnum value in Enums<MyEnum>.getValuesArray()) { // получает массив всех значений тривиального enum-а и перебирает его в foreach
		///		...
		/// }
		/// ]]></code>
		/// </example>
		public static T[] GetValuesArray() {
			return values;
		}

		/// <summary>
		/// Возвращает список енумов по типу. МЕДЛЕННЕЙ getValuesList()!
		/// </summary>
		/// <typeparam name="L"></typeparam>
		/// <returns></returns>
		public static L GetValuesList<L>() where L : System.Collections.Generic.IList<T>, new() {
			L list = new L();
			foreach (T item in GetValuesArray()) {
				list.Add(item);
			} 
			return list;
		}

		/// <summary>
		/// Возвращает список енумов по типу
		/// </summary>
		/// <typeparam name="T">Тип енума</typeparam>
		/// <returns>Возвращает список, содержащий все элементы енума</returns>
		/// <example>
		/// Пример использования:
		/// <code><![CDATA[
		/// public enum MyEnum : int { // тривиальный enum
		///		value1 = 0x00, value2 = 0x01, value3 = 0x02, value4 = 0x03
		/// };
		/// ...
		/// IList<MyEnum> list = Enums<MyEnum>.getValuesList(); // получает массив всех значений тривиального enum-а и перебирает его в foreach
		/// ]]></code>
		/// </example>
		public static System.Collections.Generic.IList<T> GetValuesList() {
			return new System.Collections.Generic.List<T>(GetValuesArray());
		}

		/// <summary>
		/// Возвращает экземпляр Enum-а типа T, по его имени
		/// </summary>
		/// <param name="name">Имя экземпляра enum-а</param>
		/// <returns>Переводит текстовое название enum-а в экземпляр</returns>
		/// <exception cref="System.ArgumentNullException">ArgumentNullException</exception>
		/// <exception cref="System.ArgumentException">ArgumentException</exception>
		public static T Parse(string name) {
			return (T)Enum.Parse(typeof(T), name);
		}

		/// <summary>
		/// Возвращает количество елементов в enum-е T. (Значение кэшируется)
		/// </summary>
		/// <returns>Возвращает количество элементов Enum-а T</returns>
		/// <example>
		/// <code><![CDATA[
		/// public enum MyEnum : int { // тривиальный enum
		///		value1 = 0x00, value2 = 0x01, value3 = 0x02, value4 = 0x03
		/// };
		/// ...
		/// int count = Enums<MyEnum>.Count; // вернёт 4
		/// ]]></code>
		/// </example>
		public static int Count {
			get {
				return values.Length;
			}
		}

		/// <summary>
		/// Возвращает экземпляр Enum-а типа T, по его значению
		/// </summary>
		/// <param name="value">Значение экземпляра enum-а</param>
		/// <returns>Находит экземпляр enum-а по его значению</returns>
		/// <exception cref="System.ArgumentNullException">ArgumentNullException</exception>
		/// <exception cref="System.ArgumentException">ArgumentException</exception>
		/// <example>
		/// <code><![CDATA[
		/// public enum MyEnum : int {
		///		value1 = 0x00, value2 = 0x22, value3 = 0x33
		/// };
		/// ...
		/// MyEnum val = Enums<MyEnum>.FromValue(0x33); // приведёт к тому же что и val = value3
		/// ]]></code>
		/// </example>
		public static T FromValue(int value) {
			return (T)Enum.ToObject(typeof(T), value);
		}

		public static string[] Names => names;
		
		/// <summary>
		/// Возвращает логическое значение - содержится ли в данном перечислении элемент name или нет?
		/// </summary>
		/// <param name="name">Имя элемента который ищется в коллекции</param>
		/// <returns>Логическое значение - содержится ли в данном перечислении элемент name</returns>
		/// <example>
		/// Пример использования:
		/// <code><![CDATA[
		/// public enum MyEnum : int { // тривиальный enum
		///		value1 = 0x00, value2 = 0x01, value3 = 0x02, value4 = 0x03
		/// };
		/// ...
		/// bool result1 = Enums<MyEnum>.ContainsValue("value1"); // true
		/// bool result2 = Enums<MyEnum>.ContainsValue("value5"); // false
		/// ]]></code>
		/// </example>
		public static bool ContainsValue(string name) {
			if (name == null || name == "") {
				return false;
			}
			for (int i = 0; i <= names.Length; i++) {
				if (names[i] == name) {
					return true;
				}
			}
			return false;
		}

		#endregion

	}

}
