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
	public class CommentsAttribute : TooltipAttribute {

		/// <summary>
		/// Поле с комментарием кода
		/// </summary>
		public readonly string comment;

		/// <summary>
		/// Показывает, надо ли скрывать комментарий или нет
		/// </summary>
		public bool visible = false;

		/// <summary>
		/// Конструктор атрибута с 1 аргументом
		/// </summary>
		/// <param name="comment">Комментарий к полю</param>
		public CommentsAttribute(string comment) : base(comment) {
			this.comment = comment;
		}

	}

}
