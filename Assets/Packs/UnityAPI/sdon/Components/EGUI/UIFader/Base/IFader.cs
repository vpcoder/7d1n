using UnityEngine;
using UnityEngine.UI;

namespace Engine.EGUI {

	/// <summary>
	/// Интерфейс для анимации скрытия/отображения UI панелей
	/// </summary>
	public interface IFader : IWidget, IMonoBehaviourOverrideStartEvent {

		/// <summary>
		/// Отображает UI элемент
		/// </summary>
		void OnShow();

		/// <summary>
		/// Скрывает UI элемент
		/// </summary>
		void OnHide();

		/// <summary>
		/// Возвращает видимость элемента
		/// </summary>
		bool isVisible();

		/// <summary>
		/// Устанавливает начальное значение видимости как "скрыто"
		/// </summary>
		void SetupHide();

		/// <summary>
		/// Устанавливает начальное значение видимости как "показывать"
		/// </summary>
		void SetupShow();

		/// <summary>
		/// Изменяет видимость элемента
		/// </summary>
		/// <param name="value">Видимость от 0f (элемент скрыт) до 1f (элемент полностью видим)</param>
		float Visible {
			get;
			set;
		}

		/// <summary>
		/// Возвращает/Устанавливает максимальную границу видимости
		/// </summary>
		float VisibleMax {
			get;
			set;
		}

		/// <summary>
		/// Возвращает/Устанавливает минимальную границу видимости
		/// </summary>
		float VisibleMin {
			get;
			set;
		}

		/// <summary>
		/// Устанавливает/Возвращает параметр "выключать дочерние transform компоненты".
		/// </summary>
		bool DisableChilds {
			get;
			set;
		}

		/// <summary>
		/// Устанавливает/Возвращает параметр "делать OnDrawEvent каскадно (для всех дочерних элементов текущего UI слоя)"
		/// </summary>
		bool FadeCascade {
			get;
			set;
		}

		/// <summary>
		/// Устанавливает/Возвращает переключатель группы слоёв
		/// </summary>
		IFaderSwitcher Switcher {
			get;
			set;
		}

		/// <summary>
		/// Срабатывает когда нобходимо перерисовать UI слой относительно заданной видимости
		/// </summary>
		/// <param name="rect">TransformRect объект на котором надо обновить видимость</param>
		/// <param name="graphic">Graphic объект, на котором надо обновить видимость</param>
		/// <param name="visible">Значение видимости от 0f до 1f</param>
		/// <param name="isChild">Флаг показывает, является ли текущий RectTransform дочерним (true) или родительским (false)</param>
		void OnDrawEvent(RectTransform rect, Graphic graphic, float visible, bool isChild);

		/// <summary>
		/// Срабатывает когда UI слой полностью скрылся
		/// </summary>
		void OnFullHideEvent();

		/// <summary>
		/// Срабатывает когда UI слой полностью отобразился
		/// </summary>
		void OnFullShowEvent();

	}

}
