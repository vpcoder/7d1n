using System;
using System.Reflection;

namespace UnityEngine {

	/// <summary>
	/// Класс-сервис, использующий рефлексию
	/// </summary>
	public static class ReflectorHandler {

		/// <summary>
		/// Находит и вызывает void метод без аргументов
		/// </summary>
		/// <typeparam name="T">Тип экземпляра класса</typeparam>
		/// <param name="instance">Экземпляр класса, у которого надо вызвать метод</param>
		/// <param name="method">Имя void метода, который надо вызвать</param>
		/// <exception cref="System.ArgumentNullException">ArgumentNullException</exception>
		/// <exception cref="System.ArgumentException">ArgumentException</exception>
		/// <exception cref="System.Reflection.AmbiguousMatchException">AmbiguousMatchException</exception>
		/// <exception cref="System.Reflection.TargetInvocationException">TargetInvocationException</exception>
		/// <exception cref="System.Reflection.TargetException">TargetException</exception>
		/// <exception cref="System.Reflection.TargetParameterCountException">TargetParameterCountException</exception>
		/// <exception cref="System.MethodAccessException">MethodAccessException</exception>
		/// <exception cref="System.InvalidOperationException">InvalidOperationException</exception>
		/// <example>
		/// Пример использования:
		/// <code>
		/// class Test {
		///  private void doSome() { ... }
		/// }
		/// ...
		/// Test test = new Test();
		/// Reflector.Invoke<Test>(test, "doSome");
		/// </code>
		/// </example>
		public static void Invoke<T>(T instance, string method) {
			MethodInfo methodInfo = instance.GetType().GetMethod(method, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			methodInfo.Invoke(instance, null);
		}

		/// <summary>
		/// Метод устанавливает значение поля указанного класса
		/// </summary>
		/// <param name="T">Тип экземпляра класса</param>
		/// <param name="V">Тип устанавливаемого значения поля</param>
		/// <param name="instance">Экземпляр класса, которому необходимо установить значение</param>
		/// <param name="field">Имя поля, которому необходимо установить значение</param>
		/// <param name="value">Значение, которое необходимо установить полю</param>
		/// <exception cref="System.ArgumentNullException">ArgumentNullException</exception>
		/// <exception cref="System.ArgumentException">ArgumentException</exception>
		/// <exception cref="System.MissingFieldException">MissingFieldException</exception>
		/// <example>
		/// Пример использования:
		/// <code>
		/// class Test {
		///  private int a = 0;
		/// }
		/// ...
		/// Test test = new Test();
		/// Reflector.SetValue<Test,int>(test,"a",666);
		/// </code>
		/// </example>
		public static void SetFieldValue<T, V>(T instance, string field, V value) {
			if (instance == null || field==null) {
				Debug.LogError("Аргументы instance и field не могут быть null pointer-ами!");
				throw new ArgumentNullException();
			}
			if (field == "") {
				Debug.LogError("Имя поля 'field' не может быть пустым!");
				throw new ArgumentException();
			}
			FieldInfo fieldInfo = instance.GetType().GetField(field, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			if (fieldInfo == null) {
				Debug.LogError("Отсутствующее поле: '"+field+"'");
				throw new MissingFieldException();
			}
			fieldInfo.SetValue(instance, value);
		}

		public static void SetFieldValue(object instance, string field, object value) {
			if (instance == null || field == null) {
				Debug.LogError("Аргументы instance и field не могут быть null pointer-ами!");
				throw new ArgumentNullException();
			}
			if (field == "") {
				Debug.LogError("Имя поля 'field' не может быть пустым!");
				throw new ArgumentException();
			}
			FieldInfo fieldInfo = instance.GetType().GetField(field, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			if (fieldInfo == null) {
				Debug.LogError("Отсутствующее поле: '" + field + "'");
				throw new MissingFieldException();
			}
			fieldInfo.SetValue(instance, value);
		}

		/// <summary>
		/// Метод возвращает значение поля указанного класса
		/// </summary>
		/// <param name="T">Тип экземпляра класса</param>
		/// <param name="V">Тип читаемого значения поля</param>
		/// <param name="instance">Экземпляр класса, которому необходимо установить значение</param>
		/// <param name="field">Имя поля, которому необходимо установить значение</param>
		/// <param name="value">Значение, которое необходимо установить полю</param>
		/// <exception cref="System.ArgumentNullException">ArgumentNullException</exception>
		/// <exception cref="System.ArgumentException">ArgumentException</exception>
		/// <returns>Возвращает значение поля field у экземпляра instance класса T</returns>
		/// <example>
		/// Пример использования:
		/// <code>
		/// class Test {
		///  private int a = 0;
		/// }
		/// ...
		/// Test test = new Test();
		/// int a = Reflector.GetValue<Test,int>(test,"a");
		/// </code>
		/// </example>
		public static V GetFieldValue<T, V>(T instance, string field) {
			if (instance == null || field == null) {
				Debug.LogError("Аргументы instance и field не могут быть null pointer-ами!");
				throw new ArgumentNullException();
			}
			if (field == "") {
				Debug.LogError("Имя поля 'field' не может быть пустым!");
				throw new ArgumentException();
			}
			FieldInfo fieldInfo = instance.GetType().GetField(field, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			if (fieldInfo == null) {
				Debug.LogError("Отсутствующее поле: '" + field + "'");
				throw new MissingFieldException();
			}
			return (V)fieldInfo.GetValue(instance);
		}

		public static object GetFieldValue(object instance, string field) {
			if (instance == null || field == null) {
				Debug.LogError("Аргументы instance и field не могут быть null pointer-ами!");
				throw new ArgumentNullException();
			}
			if (field == "") {
				Debug.LogError("Имя поля 'field' не может быть пустым!");
				throw new ArgumentException();
			}
			FieldInfo fieldInfo = instance.GetType().GetField(field, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			if (fieldInfo == null) {
				Debug.LogError("Отсутствующее поле: '" + field + "'");
				throw new MissingFieldException();
			}
			return fieldInfo.GetValue(instance);
		}

	}

}
