using System;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.EGUI {

	public interface IProgress : IWidget, IMonoBehaviourOverrideStartEvent {

		/// <summary>
		/// Устанавливает/Возвращает ширину полосы прогресса
		/// </summary>
		float Width {
			get;
		}

		/// <summary>
		/// Устанавливает/Возвращает высоту полосы прогресса
		/// </summary>
		float Height {
			get;
		}

		/// <summary>
		/// Устанавливает/Возвращает текущее значение прогресса [0; 1]
		/// </summary>
		float Value {
			get;
			set;
		}

		/// <summary>
		/// Выполняет отрисовку прогресса
		/// </summary>
		/// <param name="rect">Текущий RectTransform компонент</param>
		/// <param name="value">Текущее значение прогресса</param>
		void OnDrawEvent(RectTransform rect, float value);

	}

}
