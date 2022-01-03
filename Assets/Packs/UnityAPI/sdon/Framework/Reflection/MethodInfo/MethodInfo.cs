using System;
using System.Reflection;
using System.Collections.Generic;

namespace UnityEngine {

    /// <summary>
    /// Класс-расширение **MethodInfoAdditions** для Type объекта C#
    /// Авторы: sdon
    /// Дата: 08.07.2017
    /// Версия: 1.0.0
    /// </summary>
    public static class MethodInfoAdditions {

		/// <summary>
		/// Создаёт и возвращает нового делегата для метода с именем name по указанному типу.
		/// </summary>
		/// <typeparam name="T">Тип делегата</typeparam>
		/// <param name="type">Тип, у которого расширяется текущий метод</param>
		/// <param name="name">Имя метода, по которому надо создать делегата</param>
		/// <param name="force">Указывает принудительно искать метод, даже если он приватный</param>
		/// <returns>Возвращает делегат метода с именем name</returns>
		/// <exception cref="System.ArgumentNullException">ArgumentNullException</exception>
		/// <exception cref="System.ArgumentException">ArgumentException</exception>
		/// <exception cref="System.MissingMethodException">MissingMethodException</exception>
		/// <exception cref="System.MethodAccessException">MethodAccessException</exception>
		/// <exception cref="System.InvalidOperationException">InvalidOperationException</exception>
		/// <exception cref="System.Reflection.AmbiguousMatchException">AmbiguousMatchException</exception>
		/// <example>
		/// Пример использования:
		/// <code><![CDATA[
		///		
		///		public delegate void MyDelegateMethod(int value);
		///		...
		///		
		///		Type type = assembly.GetType("Data.Test");
		///		MyDelegateMethod delegateLink = type.GetMethod<MyDelegateMethod>("Foo",true);
		///		delegateLink.Invoke(42);
		///		
		/// ]]></code>
		/// </example>
		public static T GetMethod<T>(this Type type, string name, bool force) where T : class {
			Type t = typeof(T);
			if (!t.IsSubclassOf(typeof(Delegate))) {
#if UNITY_EDITOR
				Debug.LogError(t.Name + " - не является делегатом!");
#else
				throw new InvalidOperationException(t.Name + " - не является делегатом!");
#endif
			}
			BindingFlags flags  = (force ? BindingFlags.NonPublic : 0) | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;
			MethodInfo   method = type.GetMethod(name, flags);
			if (method == null) {
				throw new System.MethodAccessException("метод: "+name+" не найден!");
			}
			return Delegate.CreateDelegate(t,method) as T;
		}

		/// <summary>
		/// Создаёт и возвращает нового делегата для метода с именем name по указанному типу.
		/// </summary>
		/// <typeparam name="T">Тип делегата</typeparam>
		/// <param name="type">Тип, у которого расширяется текущий метод</param>
		/// <param name="name">Имя метода, по которому надо создать делегата</param>
		/// <returns>Возвращает делегат метода с именем name</returns>
		/// <exception cref="System.ArgumentNullException">ArgumentNullException</exception>
		/// <exception cref="System.ArgumentException">ArgumentException</exception>
		/// <exception cref="System.MissingMethodException">MissingMethodException</exception>
		/// <exception cref="System.MethodAccessException">MethodAccessException</exception>
		/// <exception cref="System.InvalidOperationException">InvalidOperationException</exception>
		/// <exception cref="System.Reflection.AmbiguousMatchException">AmbiguousMatchException</exception>
		/// <example>
		/// Классический пример использования:
		/// <code><![CDATA[
		///		
		///		public delegate void MyDelegateMethod(int value);
		///		...
		///		
		///		Type type = assembly.GetType("Data.Test");
		///		MyDelegateMethod delegateLink = type.GetMethod<MyDelegateMethod>("Foo");
		///		delegateLink.Invoke(42);
		///		
		/// ]]></code>
		/// </example>
		public static T GetMethod<T>(this Type type, string name) where T : class {
			return GetMethod<T>(type,name,true);
		}

	}

}
