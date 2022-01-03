using System;
using UnityEngine;

namespace Engine.EGUI {

	/// <summary>
	/// Класс, группирующий UI композиты в таблицу
	/// </summary>
	public abstract class TableViewer<T> : Viewer<T>, ITableViewer
		where T : class {
	
		#region Share Fields

		/// <summary>
		/// Ширина и высота отступа между UI композитами
		/// </summary>
		[SerializeField] protected Vector2 cellBorder = new Vector2(5f,5f);

		/// <summary>
		/// Тип привязки UI композитов внутри контейнера
		/// </summary>
		[SerializeField] protected InnerAlignType innerAlignType = InnerAlignType.FixedVertical;

		#endregion

		#region Hide Fields

		/// <summary>
		/// Текущий массив композитов
		/// </summary>
		private RectTransform[] items = Arrays<RectTransform>.Empty;

		#endregion

		#region Events

		/// <summary>
		/// Метод срабатывает в момент, когда получены новые входные данные и необходимо перестроить UI композиты на экране компонента
		/// </summary>
		public override void OnContent() {

#if UNITY_EDITOR

			if(viewerScreen==null){
				Debug.LogError("Установите viewerScreen, как объект, на котором будет рисоваться список! viewerScreen не может быть null!");
				return;
			}

#endif

			viewerScreen.transform.DestroyAllChilds(() => {
				
				Vector3 scale = viewerScreen.localScale;
				viewerScreen.localScale = Vector3.one;

				T[] data = ContentProvider.GetElements(input);

#if UNITY_EDITOR

				if(data==null){
					throw new NullReferenceException("Провайдер контента не должен возвращать Null! Убедитесь в том, что провайдер работает правильно.");
				}

#endif

				if (data==null || data.Length==0) {
					viewerScreen.localScale = scale;
					return;
				}

				this.items = new RectTransform[data.Length];

				float containerWidth = viewerScreen.sizeDelta.x;
				float containerHeight = border.Height;

				int index = 0;

				foreach(T item in data){
					
					RectTransform itemUI = CompositeProvider.GetComposite(item);

					itemUI.transform.SetParent(transform);
					itemUI.localScale = Vector3.one;
					items[index]      = itemUI;

					switch(innerAlignType) {
						case InnerAlignType.FixedVertical:
							SetItemSettingsFixedVertical(itemUI,index);
							break;
						case InnerAlignType.Fixed:
							SetItemSettingsFixed(itemUI,index);
							break;
						case InnerAlignType.FixedHorizontal:
							SetItemSettingsFixedHorizontal(itemUI,index);
							break;
					}

					index++;

				}
				
				viewerScreen.sizeDelta = new Vector2(containerWidth, containerHeight);
				viewerScreen.localScale = scale;

			});

		}

		#endregion

		#region Private Handlers

		/// <summary>
		/// Метод заставляет переконфигурировать композиты (не пересоздаёт старые, а конфигурирует их по новому)
		/// </summary>
		public override void ReconfigComposites(){

			if (items == null) {
				return;
			}

			for (int i = 0; i < items.Length; i++) {
				RectTransform itemUI = items[i];

				switch(innerAlignType) {
					case InnerAlignType.FixedVertical:
						SetItemSettingsFixedVertical(itemUI,i);
						break;
					case InnerAlignType.Fixed:
						SetItemSettingsFixed(itemUI,i);
						break;
					case InnerAlignType.FixedHorizontal:
						SetItemSettingsFixedHorizontal(itemUI,i);
						break;
				}
			}

		}

		private void SetItemSettingsFixed(RectTransform itemUI, int  index){

			// <-------- FIXME

		}

		private void SetItemSettingsFixedVertical(RectTransform itemUI, int  index){

			float height = itemUI.sizeDelta.y;
			float posYOffset = -border.Top - (index)*(height + cellBorder.y);
			float posXOffset = 0f;

			itemUI.offsetMin = new Vector2(border.Left+posXOffset,  -height + posYOffset);
			itemUI.offsetMax = new Vector2(-border.Right, posYOffset);

		}

		private void SetItemSettingsFixedHorizontal(RectTransform itemUI, int  index){

			// <-------- FIXME

		}

		#endregion

		#region Table

		/// <summary>
		/// Устанавливает/Возвращает тип привязки композитов внутри контейнера
		/// </summary>
		/// <value>Тип привязки комопзитов внутри контейнера</value>
		public InnerAlignType InnerAlignType {
			get {
				return innerAlignType;
			}
			set {
				this.innerAlignType = value;
				ReconfigComposites();
			}
		}

		/// <summary>
		/// Ширина и высота отступов между UI композитами
		/// </summary>
		/// <value>Ширина и высота отступов между композитами</value>
		public Vector2 CellBorder {
			get {
				return cellBorder;
			}
			set {
				cellBorder = value;
				ReconfigComposites();
			}
		}

		/// <summary>
		/// Возвращает все активные композиты на канвасе
		/// </summary>
		/// <value>Все используемые UI композиты</value>
		public RectTransform[] Items {
			get {
				return items;
			}
		}

		#endregion

		#region Unity Editor

#if UNITY_EDITOR

		private Vector2        oldCellBorder;
		private Border         oldBorder;
		private InnerAlignType oldInnerAlignType;

		/// <summary>
		/// Проверяет обновления полей в редакторе Unity, при необходимости, метод должен выставить флаг обновлений через
		/// метод UpdateUI
		/// </summary>
		protected override void UpdateUICheck() {

			if (oldBorder != border) {
				oldBorder = border;
				UpdateUI();
			}

			if (oldCellBorder != cellBorder) {
				oldCellBorder = cellBorder;
				UpdateUI();
			}

			if (oldInnerAlignType != innerAlignType) {
				oldInnerAlignType = innerAlignType;
				UpdateUI();
			}
				
		}

#endif

		#endregion

	}

}
