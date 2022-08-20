using Engine.Data;
using Engine.EGUI;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic
{

    /// <summary>
    /// Абстрактная сумка
    /// </summary>
	public abstract class AbstractBag : Panel, IBag
    {
		
		#region Hidden Fields
		
		protected IList<IItem> items = new List<IItem>();
		
		#endregion
		
		#region Properties
		
		public virtual IList<IItem> Items {
			get { return this.items; }
			set { this.items = value; }
		}

        #endregion

        /// <summary>
        /// Ширина всей видимой области инвентаря
        /// </summary>
        public abstract int FrameWidth { get; }

        /// <summary>
        /// Высота всей видимой области инвентаря
        /// </summary>
        public abstract int FrameHeight { get; }

        /// <summary>
        /// Количество ячеек умещающихся в ширину инвентаря
        /// </summary>
        public abstract int CellCountX { get; }

        /// <summary>
        ///  Количество ячеек умещающихся в высоту инвентаря
        /// </summary>
        public abstract int CellCountY { get; }

        /// <summary>
        /// Перестраивает гуи сумки
        /// </summary>
        public abstract void Redraw();

        public abstract void ClickItem(IItem item);

        public abstract void SetSelected(InventoryItemBehaviour itemBehaviour);
        public abstract InventoryItemBehaviour Selected { get; }

    }
	
}
