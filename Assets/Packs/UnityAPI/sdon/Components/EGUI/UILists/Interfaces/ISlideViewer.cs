using System;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.EGUI {

	/// <summary>
	/// Интерфейс Viewer для предоставления композитов в виде слайдов
	/// </summary>
	public interface ISlideViewer {

		/// <summary>
		/// Возвращает кнопку перемотки на следующий слайд
		/// </summary>
		/// <returns>Кнопка перемотки на следующий слайд</returns>
		Button GetNextButton();

		/// <summary>
		/// Возвращает кнопку перемотки на предыдущий слайд
		/// </summary>
		/// <returns>Кнопка перемотки на предыдущий слайд</returns>
		Button GetPrevButton();

	}

}
