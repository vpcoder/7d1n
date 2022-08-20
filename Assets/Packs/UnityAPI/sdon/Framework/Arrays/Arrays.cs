using System;

namespace UnityEngine {

    /// <summary>
    /// Параметризированный класс-хандлер **Arrays** для управления массивами в C#
    /// Авторы: sdon
    /// Дата: 11.01.2017
    /// Версия: 1.0.0
    /// </summary>
    /// <typeparam name="T">Тип массива</typeparam>
    public static class Arrays<T> {

		#region Hide Fields

		/// <summary>
		/// Скрытое поле, только для чтение, хранит экземпляр пустого массива заданного типа
		/// </summary>
		private static readonly T[] empty = new T[0];

        #endregion

        #region Properties

        /// <summary>
        /// Свойство возвращает пустой экземпляр массива заданного типа
        /// </summary>
        /// <example>
        /// Пример использования 1:
        /// <code>[[<![CDATA[
        /// private MyClass[] data = Arrays<MyClass>.Empty;
        /// ]]></code>[[<![CDATA[
        /// Пример использования 2:
        /// <code>
        /// if(data == Arrays<MyClass>.Empty) { // массив пуст
        ///		...
        ///	}
        /// ]]></code>
        /// </example>
        public static T[] Empty {
			get {
				return empty;
			}
		}

		#endregion

	}

}
