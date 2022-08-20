using UnityEngine;

namespace Engine.EGUI {

	/// <summary>
	/// Общий интерфейс виджетов
	/// </summary>
	public interface IWidget {

		/// <summary>
		/// Отступы границ виджета
		/// </summary>
		/// <value>Границы</value>
		IBorder Border {
			get;
			set;
		}

		/// <summary>
		/// RectTransform виджета
		/// </summary>
		/// <value>Возвращает объект RectTransform виджета</value>
		RectTransform Rect {
			get;
		}

	}

}
