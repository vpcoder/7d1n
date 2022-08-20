using System;

namespace Engine.EGUI {
	
	public interface IBorder {

		/// <summary>
		/// Устанавливает все параметры границы
		/// </summary>
		/// <param name="x1">Left</param>
		/// <param name="y1">Top</param>
		/// <param name="x2">Width</param>
		/// <param name="y2">Height</param>
		void Set(float x1, float y1, float x2, float y2);

		/// <summary>
		/// Возвращает ширину вертикальных границ
		/// </summary>
		/// <value>Ширина вертикальных границ</value>
		float Width {
			get;
		}

		/// <summary>
		/// Возвращает высоту горизонтальных границ
		/// </summary>
		/// <value>Высота горизонтальных границ</value>
		float Height {
			get;
		}

		/// <summary>
		/// Левая вертикальная граница
		/// </summary>
		float Left {
			get;
			set;
		}

		/// <summary>
		/// Правая вертикальная граница
		/// </summary>
		float Right {
			get;
			set;
		}

		/// <summary>
		/// Верхняя горизонтальная граница
		/// </summary>
		float Top {
			get;
			set;
		}

		/// <summary>
		/// Нижняя горизонтальная граница
		/// </summary>
		float Bottom {
			get;
			set;
		}

	}

}
