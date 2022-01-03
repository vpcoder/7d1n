using System;

namespace UnityEngine {

	/// <summary>
	/// Атрибут игнорирования документации SerializeField поля. Этот Атрибут заставит сборщика документации проигнорировать указанное поле
	/// <example>
	/// Пример использования:
	/// <code>
	/// ...
	/// [IgnoreDoc] public int speedIndicator;
	/// ...
	/// </code>
	/// </example>
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class IgnoreDocAttribute : PropertyAttribute {

		/// <summary>
		/// Конструктор атрибута
		/// </summary>
		public IgnoreDocAttribute() { }

	}

}
