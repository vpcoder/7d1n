using System;

namespace UnityEngine {

	/// <summary>
	/// Атрибут описания SerializeField поля. Этот Атрибут добавит пояснение к редактору помеченного атрибутом поля
	/// <example>
	/// Пример использования:
	/// <code>
	/// ...
	/// [Caption("Базовая скорость перемещения персонажа, при управлении с клавиатуры")]
	/// [SerializeField] private int speed;
	/// ...
	/// </code>
	/// </example>
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class CaptionAttribute : PropertyAttribute {

		/// <summary>
		/// Поле с описанием
		/// </summary>
		public readonly string caption;

		/// <summary>
		/// Конструктор атрибута с 1 аргументом
		/// </summary>
		/// <param name="caption">Описание указанного атрибутом поля</param>
		public CaptionAttribute(string caption) {
			this.caption = caption;
		}

	}

}
