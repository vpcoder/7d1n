using System;

namespace UnityEngine {

	/// <summary>
	/// Атрибут заголовка SerializeField поля. Этот Атрибут добавит свой заголовок к редактору помеченного атрибутом поля
	/// <example>
	/// Пример использования 1:
	/// <code>
	/// ...
	/// [Title("Скорость")]
	/// [SerializeField] private int speed;
	/// ...
	/// </code>
	/// Пример использования 2:
	/// <code>
	/// ...
	/// [Title("Скорость",true)]
	/// [SerializeField] private int speed;
	/// ...
	/// </code>
	/// </example>
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class TitleAttribute : PropertyAttribute {

		/// <summary>
		/// Заголовок
		/// </summary>
		public readonly string title;

		/// <summary>
		/// Конструктор атрибута с 1 аргументом
		/// </summary>
		/// <param name="caption">Описание указанного атрибутом поля</param>
		public TitleAttribute(string title) {
			this.title = title;
		}

	}

}
