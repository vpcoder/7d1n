using System;
using UnityEngine;

namespace Engine.EGUI {

	/// <summary>
	/// Абстрактная основа для всех Viewer-ов
	/// </summary>
	public abstract class Viewer<T> : EmptyViewer, IViewer<T>
		where T : class {

		#region Share Fields

		[Caption("Экран Viewer-а")]
		[Comments("Экран компонента - панель-контейнер, на которой будут расположены все UI композиты")]
		[SerializeField] protected RectTransform viewerScreen;

		//[Caption("Проводник контента")]
		//[Comments("По запросу Viewer-а, должен формировать элементы, по указанному входному аргументу input")]
		//[SerializeField]
		protected IContentProvider<T> contentProvider;

		//[Caption("Проводник композитов")]
		//[Comments("По запросу Viewer-а, должен формировать UI композит для конкретных элементов, сформированных проводником контента")]
		//[SerializeField] 
		protected ICompositeProvider<T> compositeProvider;

		[Caption("Граница виджета")]
		[Comments("Определяет внешнюю границу Viewer-а, за которую не будут заступать композиты")]
		[SerializeField] protected Border border = new Border(5,5,5,5);

		#endregion

		#region Hide Fields

		/// <summary>
		/// Входные данные для провайдера контента
		/// </summary>
		protected object input;

		#endregion

		#region UnityEvents

		private void Start(){
			OnStart();
		}

		private void Update(){
			OnUpdate();
		}

/// Переконфигурирование композитов в редакторе Unity
/// Проверяет статус обновления, при необходимости, вызывает переконфигурацию
#if UNITY_EDITOR

		/// <summary>
		/// Флаг обновления конфигураций
		/// </summary>
		private bool uiUpdateFlag = false;

		private void OnValidate() {
			UpdateUICheck(); // вызываем проверку обновлений
			if (!uiUpdateFlag) { // обновления не требуется
				return;
			}
			uiUpdateFlag = false; // сбрасываем флаг
			ReconfigComposites(); // обновляем конфигурации
		}

		/// <summary>
		/// Устанавлиает флаг обновления конфигураций в true
		/// </summary>
		protected void UpdateUI(){
			uiUpdateFlag = true;
		}

		/// <summary>
		/// Проверяет обновления полей в редакторе Unity, при необходимости, метод должен выставить флаг обновлений через метод UpdateUI
		/// </summary>
		protected virtual void UpdateUICheck() { }

#endif


		#endregion

		#region Events

		/// <summary>
		/// Метод срабатывает в момент инициализации экземпляра класса
		/// </summary>
		public virtual void OnStart() { }

		/// <summary>
		/// Метод срабатывает в момент update экземпляра MonoBehaviour класса
		/// </summary>
		public virtual void OnUpdate() { }

		/// <summary>
		/// Метод срабатывает в момент, когда получены новые входные данные и необходимо перестроить UI композиты на экране компонента
		/// </summary>
		public virtual void OnContent() { }

		/// <summary>
		/// Метод заставляет переконфигурировать композиты (не пересоздаёт старые, а конфигурирует их по новому)
		/// </summary>
		public virtual void ReconfigComposites() { }

		#endregion

		#region Providers

		/// <summary>
		/// Возвращает проводника контента
		/// </summary>
		/// <returns>Проводник контента</returns>
		public IContentProvider<T> ContentProvider {
			get {
				return contentProvider;
			}
			set {
				contentProvider = value;
			}
		}

		/// <summary>
		/// Возвращает проводника UI композитов
		/// </summary>
		/// <returns>Проводник UI композитов</returns>
		public ICompositeProvider<T> CompositeProvider {
			get {
				return compositeProvider;
			}
			set {
				compositeProvider = value;
			}
		}

		#endregion

		#region Input

		/// <summary>
		/// Устанавливает входные данные для проводника
		/// </summary>
		/// <param name="input">Входные данные</param>
		public object Input {
			get {
				return input;
			}
		}

		/// <summary>
		/// Устанавливает входные данные для проводника.
		/// При установке *НОВЫХ* данных (либо, при установке флага forceUpdate) Viewer перестроит UI композиты.
		/// </summary>
		/// <param name="input">Входные данные</param>
		/// <param name="forceUpdate">Устанавливает флаг обязательного обновления контента. Если true - контент принудительно пересоздаётся</param>
		public void SetInput(object input, bool forceUpdate) {

			if (!forceUpdate && this.input==input) {
				return;
			}

			this.input = input;

#if UNITY_EDITOR
			if (ContentProvider == null || CompositeProvider == null) {
				Debug.LogError("При загрузке класса не был задан ContentProvider или CompositeProvider! Убедитесь в том, что оба провайдера заданы.");
				throw new Exception("Не задан ContentProvider/CompositeProvider!");
			} else {
				OnContent();
			}
#else
			OnContent();
#endif

		}

		#endregion

		#region Widget

		/// <summary>
		/// Отступы границ виджета
		/// </summary>
		/// <value>Границы</value>
		public virtual IBorder Border {
			get {
				return border;
			}
			set {
				this.border = value as Border;
				ReconfigComposites();
			}
		}

		/// <summary>
		/// RectTransform виджета
		/// </summary>
		/// <value>Возвращает объект RectTransform виджета</value>
		public RectTransform Rect {
			get {
				return viewerScreen;
			}
		}

		#endregion

	}

}
