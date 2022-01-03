using System;
using UnityEngine;

namespace Engine.EGUI {

	/// <summary>
	/// Класс, группирующий UI композиты в иконки
	/// </summary>
	public abstract class IconViewer<T> : Viewer<IIconContainer<T>>, IIconViewer<IIconContainer<T>>
		where T : class {

		#region Share Fields
		
		/// <summary>
		/// Отступ по X между иконками
		/// </summary>
		[SerializeField]
		protected float innerBorderX = 1f;

		/// <summary>
		/// Отступ по Y между иконками
		/// </summary>
		[SerializeField]
		protected float innerBorderY = 1f;

		/// <summary>
		/// Текущее число ячеек в сетке по X
		/// </summary>
		[SerializeField]
		protected int cellCountX;

		/// <summary>
		/// Текущее число ячеек в сетке по Y
		/// </summary>
		[SerializeField]
		protected int cellCountY;

		/// <summary>
		/// Текущий размер ячейки
		/// </summary>
		[SerializeField]
		protected Vector2 cellSize;

		#endregion

		#region Hide Fields

		/// <summary>
		/// Текущий массив композитов
		/// </summary>
		private RectTransform[]     items = Arrays<RectTransform>.Empty;
		private IIconContainer<T>[] icons = Arrays<IIconContainer<T>>.Empty;
		
		#endregion

		#region Events

		private void resizeCellsArea() {

			Vector2 pos = viewerScreen.localPosition;

			Vector2 size = CellSize;
			size.x *= CellCountX;
			size.y *= CellCountY;


			viewerScreen.offsetMin = new Vector2(0, pos.y);
			viewerScreen.offsetMax = new Vector2(viewerScreen.sizeDelta.x - size.x, pos.y+size.y);

			//viewerScreen.sizeDelta     = size;
			viewerScreen.localPosition = pos;

		}
		
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

				icons = ContentProvider.GetElements(input);

				IIconViewerCompositeProvider<T> iconViewerCompositeProvider = (CompositeProvider as IIconViewerCompositeProvider<T>);

#if UNITY_EDITOR

				if (iconViewerCompositeProvider == null) {
					throw new NullReferenceException("Провайдер композитов у IconViewer обязан быть совместимым с IIconViewerCompositeProvider!");
				}

#endif

				iconViewerCompositeProvider.ResizeViewer(this, icons);

#if UNITY_EDITOR

				if(icons==null){
					throw new NullReferenceException("Провайдер контента не должен возвращать Null! Убедитесь в том, что провайдер работает правильно.");
				}

#endif

				if (icons==null || icons.Length==0) {
					viewerScreen.localScale = scale;
					return;
				}

				this.items = new RectTransform[icons.Length];

				//float containerWidth = viewerScreen.sizeDelta.x;
				//float containerHeight = border.Height;

				int index = 0;
				foreach(IIconContainer<T> icon in icons){
					
					RectTransform itemUI = CompositeProvider.GetComposite(icon);

					itemUI.transform.SetParent(transform);
					itemUI.localScale = Vector3.one;
					items[index++] = itemUI;

					SetItemSettings(itemUI,icon);
					
				}
				
				//viewerScreen.sizeDelta = new Vector2(containerWidth, containerHeight);
				//viewerScreen.localScale = scale;

				resizeCellsArea();

			});
			
		}

		#endregion

		#region Private Handlers

		/// <summary>
		/// Метод заставляет переконфигурировать композиты (не пересоздаёт старые, а конфигурирует их по новому)
		/// </summary>
		public override void ReconfigComposites(){

			if (items == null || icons == null) {
				return;
			}

			for (int i = 0; i < icons.Length; i++) {
				SetItemSettings(items[i],icons[i]);
			}

		}
		
		private void SetItemSettings(RectTransform itemUI, IIconContainer<T> icon){

			float height = itemUI.sizeDelta.y;
			float width  = itemUI.sizeDelta.x;

			float posYOffset = -border.Top - (icon.PosY)*(height + innerBorderY);
			float posXOffset = border.Left + (icon.PosX)*(width + innerBorderX);

			itemUI.localPosition = new Vector3(posXOffset, posYOffset, 0);

			//itemUI.offsetMin = new Vector2(border.Left+posXOffset,  -height + posYOffset);
			//itemUI.offsetMax = new Vector2(-border.Right, posYOffset);

		}
		
		#endregion

		#region Icons

		/// <summary>
		/// Устанавливает/Возвращает отступ композитов внутри контейнера по X
		/// </summary>
		/// <value>Отступ композитов комопзитов внутри контейнера</value>
		public float InnerBorderX {
			get {
				return innerBorderX;
			}
			set {
				this.innerBorderX = value;
				ReconfigComposites();
			}
		}

		/// <summary>
		/// Устанавливает/Возвращает отступ композитов внутри контейнера по Y
		/// </summary>
		/// <value>Отступ композитов комопзитов внутри контейнера</value>
		public float InnerBorderY {
			get {
				return innerBorderY;
			}
			set {
				this.innerBorderY = value;
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

		/// <summary>
		/// Устанавливает/возвращает размер сетки композитов
		/// </summary>
		public int CellCountX {
			get {
				return cellCountX;
			}
			set {
				this.cellCountX = value;
				resizeCellsArea();
			}
		}

		/// <summary>
		/// Устанавливает/возвращает размер сетки композитов
		/// </summary>
		public int CellCountY {
			get {
				return cellCountY;
			}
			set {
				this.cellCountY = value;
				resizeCellsArea();
			}
		}

		/// <summary>
		/// Устанавливает/Возвращает размер ячейки под композит
		/// </summary>
		public Vector2 CellSize {
			get {
				return this.cellSize;
			}
			set {
				this.cellSize = value;
				resizeCellsArea();
			}
		}

		#endregion

		#region Unity Editor

#if UNITY_EDITOR

		private int     oldCellCountX;
		private int     oldCellCountY;
		private Vector2 oldCellBorder;
		private Vector2 oldCellSize;
		private Border  oldBorder;
		private float   oldInnerBorderX;
		private float   oldInnerBorderY;

		/// <summary>
		/// Проверяет обновления полей в редакторе Unity, при необходимости, метод должен выставить флаг обновлений через
		/// метод UpdateUI, либо перерисовать область сетки через метод resizeCellsArea
		/// </summary>
		protected override void UpdateUICheck() {

			if (oldBorder != border) {
				oldBorder = border;
				UpdateUI();
			}

			if (oldInnerBorderX != innerBorderX) {
				oldInnerBorderX = innerBorderX;
				UpdateUI();
			}

			if (oldInnerBorderY != innerBorderY) {
				oldInnerBorderY = innerBorderY;
				UpdateUI();
			}

			if (oldCellCountX != cellCountX) {
				oldCellCountX = cellCountX;
				resizeCellsArea();
			}

			if (oldCellCountY != cellCountY) {
				oldCellCountY = cellCountY;
				resizeCellsArea();
			}

			if (oldCellSize != cellSize) {
				oldCellSize = cellSize;
				resizeCellsArea();
			}
				
		}

#endif

		#endregion


	}

}
