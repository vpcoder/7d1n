using System;
using System.Reflection;
using System.Collections.Generic;

namespace UnityEngine {

    /// <summary>
    /// Класс-расширение **FieldInfoAdditions** для FieldInfo объекта C# 
    /// Авторы: sdon
    /// Дата: 20.01.2017
    /// Версия: 1.0.0
    /// </summary>
    public static class FieldInfoAdditions {

		/// <summary>
		/// Возвращает искомый атрибут (если атрибут не найден, вернёт null)
		/// </summary>
		/// <typeparam name="T">Тип искомого атрибута</typeparam>
		/// <returns>Искомый атрибут (если атрибут не найден, вернёт null)</returns>
		/// <exception cref="System.InvalidOperationException">InvalidOperationException</exception>
		/// <exception cref="System.TypeLoadException">TypeLoadException</exception>
		public static T GetAttribute<T>(this FieldInfo field) where T : Attribute {
			foreach (object attribute in field.GetCustomAttributes(true)) {
				if (attribute.GetType() == typeof(T)) {
					return (T)attribute;
				}
			}
			return null;
		}

		/// <summary>
		/// Возвращает логическое значение - содержится хотябы один атрибут из items в поле
		/// </summary>
		/// <param name="items">Список атрибутов, которые ищутся в поле field</param>
		/// <returns>Содержится хотябы один атрибут из items в поле field</returns>
		/// <exception cref="System.InvalidOperationException">InvalidOperationException</exception>
		/// <exception cref="System.TypeLoadException">TypeLoadException</exception>
		/// <example>
		/// Пример использования 1:
		/// <code>
		/// ...
		/// bool isSerializeAttribute = field.ContainsOneAttribute(typeof(SerializeField));
		/// </code>
		/// Пример использования 2:
		/// <code>
		/// ...
		/// bool isSerializeOrRangeAttribute = field.ContainsOneAttribute(new Type[] { typeof(SerializeField), typeof(RangeAttribute) });
		/// </code>
		/// </example>
		public static bool ContainsOneAttribute(this FieldInfo field, params Type[] items) {
			foreach (object item in field.GetCustomAttributes(false)) {
				foreach (Type attrib in items) {
					if (item.GetType() == attrib) {
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Возвращает логическое значение - содержится ли атрибут из item в поле
		/// </summary>
		/// <param name="item">Атрибут, который ищется в поле field</param>
		/// <returns>Содержится атрибут из item в поле field</returns>
		/// <exception cref="System.InvalidOperationException">InvalidOperationException</exception>
		/// <exception cref="System.TypeLoadException">TypeLoadException</exception>
		/// <example>
		/// Пример использования:
		/// <code>
		/// ...
		/// bool isSerializeAttribute = field.ContainsAttribute(typeof(SerializeField));
		/// </code>
		/// </example>
		public static bool ContainsAttribute(this FieldInfo field, Type item) {
			return ContainsOneAttribute(field,item);
		}

		/// <summary>
		/// Возвращает логическое значение - содержатся все атрибуты из items в поле
		/// </summary>
		/// <param name="items">Список атрибутов, которые ищутся в поле field</param>
		/// <returns>Содержатся все атрибуты из items в поле field</returns>
		/// <exception cref="System.InvalidOperationException">InvalidOperationException</exception>
		/// <exception cref="System.TypeLoadException">TypeLoadException</exception>
		/// <example>
		/// Пример использования 1:
		/// <code>
		/// ...
		/// bool isSerializeAttribute = field.ContainsAllAttribute(typeof(SerializeField));
		/// </code>
		/// Пример использования 2:
		/// <code>
		/// ...
		/// bool isSerializeAndRangeAttribute = field.ContainsAllAttribute(new Type[] { typeof(SerializeField), typeof(RangeAttribute) });
		/// </code>
		/// </example>
		public static bool ContainsAllAttribute(this FieldInfo field, params Type[] items) {
			int count = 0;
			foreach (Type attrib in items) {
				foreach (object item in field.GetCustomAttributes(false)) {
					if (attrib == item.GetType()) {
						count++;
						break;
					}
				}
			}
			return count==items.Length;
		}

		/// <summary>
		/// Возвращает логическое значение, является ли данное поле открытым для редактора Unity (SerializeField)
		/// </summary>
		/// <returns>Является ли данное поле открытым для редактора Unity (SerializeField)</returns>
		/// <example>
		/// Пример использвоания:
		/// <code>
		/// bool isUnityField = field.IsUnityEditorField();
		/// </code>
		/// </example>
		public static bool IsUnityEditorField(this FieldInfo field) {
			return field.IsPublic || (field.ContainsAttribute(typeof(UnityEngine.SerializeField)));
		}

	}

}
