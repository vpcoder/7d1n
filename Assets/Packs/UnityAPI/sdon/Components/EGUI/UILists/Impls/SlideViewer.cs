using System;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.EGUI {

	/// <summary>
	/// Класс, формирующий композиты слайдами с панелью навигации
	/// </summary>
	public abstract class SlideViewer<T> : Viewer<T>, ISlideViewer
		where T : class {

		#region Share Fields

		public const int EMPTY_SELECT_INDEX = -1;

		/// <summary>
		/// Кнопка, перелистывающая слайд вперёд
		/// </summary>
		[SerializeField] private Button nextButton;

		/// <summary>
		/// Кнопка, перелистывающая слайд назад
		/// </summary>
		[SerializeField] private Button prevButton;

		#endregion


		#region Hide Fields

		/// <summary>
		/// Текущий композит
		/// </summary>
		private RectTransform item = null;

		/// <summary>
		/// Массив текущих данных (под все композиты)
		/// </summary>
		private T[] elements = Arrays<T>.Empty;

		/// <summary>
		/// Индекс выбранного композита
		/// </summary>
		private int selectedIndex = EMPTY_SELECT_INDEX;

		#endregion


		#region Events

		/// <summary>
		/// Возвращает кнопку перемотки на следующий слайд
		/// </summary>
		/// <returns>Кнопка перемотки на следующий слайд</returns>
		public Button GetNextButton() {
			return nextButton;
		}

		/// <summary>
		/// Возвращает кнопку перемотки на предыдущий слайд
		/// </summary>
		/// <returns>Кнопка перемотки на предыдущий слайд</returns>
		public Button GetPrevButton() {
			return prevButton;
		}

		/// <summary>
		/// Метод срабатывает в момент, когда получены новые входные данные и необходимо перестроить UI композиты на экране компонента
		/// </summary> 
		public override void OnContent(){

#if UNITY_EDITOR

			if(viewerScreen==null){
				Debug.LogError("Установите viewerScreen, как объект, на котором будет рисоваться список! viewerScreen не может быть null!");
				return;
			}

#endif

			viewerScreen.transform.DestroyAllChilds(() => {

				Vector3 scale = viewerScreen.localScale;
				viewerScreen.localScale = Vector3.one;

				elements = ContentProvider.GetElements(input);

#if UNITY_EDITOR

				if (elements == null) {
					throw new NullReferenceException("Провайдер контента не должен возвращать Null! Убедитесь в том, что провайдер работает правильно.");
				}

#endif

				if (elements == null || elements.Length == 0) {
					viewerScreen.localScale = scale;
					return;
				}

				tryBindButtonEvents();
				SelectedIndex = 0;
				
				viewerScreen.localScale = scale;

			});

		}

		/// <summary>
		/// Перепривязывает события нажатий на кнопки "Вперёд"/"Назад"
		/// </summary>
		private void tryBindButtonEvents() {

			if (nextButton != null) {
				nextButton.onClick.RemoveListener(Next);
				nextButton.onClick.AddListener(Next);
			}

			if (prevButton != null) {
				prevButton.onClick.RemoveListener(Prev);
				prevButton.onClick.AddListener(Prev);
			}

		}

		/// <summary>
		/// Пересоздаёт активный UI композит
		/// </summary>
		/// <param name="index">Индекс элемента композита, который возьмётся за основу</param>
		private void createComposite(int index) {

			if (index == EMPTY_SELECT_INDEX) {
				return;
			}

			RectTransform itemUI = CompositeProvider.GetComposite(elements[index]);
			itemUI.transform.SetParent(transform);
			itemUI.localScale = Vector3.one;
			item = itemUI;

		}

		#endregion


		#region Slide

		/// <summary>
		/// Возвращает активный композит на канвасе
		/// </summary>
		/// <value>Используемый UI композит</value>
		public RectTransform Item {
			get {
				return item;
			}
		}

		/// <summary>
		/// Устанавливает/Возвращает выбранный индекс слайда
		/// </summary>
		public int SelectedIndex {
			get {
				return selectedIndex;
			}
			set {

				int prev = selectedIndex;
				selectedIndex = value;

				if (elements.Length == 0) {
					selectedIndex = EMPTY_SELECT_INDEX;
				}

				if (selectedIndex < 0) {
					selectedIndex = 0;
				}

				if (selectedIndex >= elements.Length) {
					selectedIndex = elements.Length - 1;
				}

				if (elements.Length == 0) { // нечего обновлять
					return;
				}

				if (prev==selectedIndex) { // изменений индекса не было
					return;
				}

				createComposite(selectedIndex);
				
			}
		}

		/// <summary>
		/// Перелистывает слайд вперёд (если достигнут конец - метод ничего не делает)
		/// </summary>
		public void Next() {
			if (elements.Length == 0) {
				return;
			}
			if (selectedIndex == elements.Length - 1) {
				return;
			}
			SelectedIndex++;
		}

		/// <summary>
		/// Перелистывает слайд назад (если достигнуто начало - метод ничего не делает)
		/// </summary>
		public void Prev() {
			if (elements.Length == 0) {
				return;
			}
			if (selectedIndex == 0) {
				return;
			}
			SelectedIndex--;
		}

		/// <summary>
		/// Возвращает число слайдов в списке
		/// </summary>
		public int Count {
			get {
				return elements.Length;
			}
		}

		#endregion

       }

}
