using Engine.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Engine.Logic
{

    /// <summary>
    /// 
    /// Простая сумка
    /// Логика простого инвентаря, внутри которого могут отображаться предметы,
    /// и производиться минимальный набор действий (фильтрация и отлов нажатий на предметы)
    /// ---
    /// A simple bag
    /// The logic of a simple inventory, within which items can be displayed,
    /// and performs a minimal set of actions (filtering and item clicks catching)
    /// 
    /// </summary>
	public class Bag : AbstractBag
    {
        
        /// <summary>
        ///     Расстояние между иконками предметов
        ///     ---
        ///     Distance between item icons
        /// </summary>
        [SerializeField] private Vector2 incellOffset = new Vector2(4, 4);
        
        [SerializeField] private int cellSizeX;
        [SerializeField] private int cellSizeY;

        [SerializeField] private RectTransform contentContainer;
        [SerializeField] private RectTransform viewportContainer;

        [SerializeField] private InventoryItemBehaviour itemBehaviourPrefab;

        /// <summary>
        /// Ширина всей видимой области инвентаря
        /// </summary>
        public override int FrameWidth
        {
            get
            {
                return (int)viewportContainer.rect.width;
            }
        }

        /// <summary>
        /// Высота всей видимой области инвентаря
        /// </summary>
        public override int FrameHeight
        {
            get
            {
                return (int)viewportContainer.rect.height;
            }
        }

        /// <summary>
        /// Количество ячеек умещающихся в ширину инвентаря
        /// </summary>
        public override int CellCountX => (int)(FrameWidth / (cellSizeX + incellOffset.x));

        /// <summary>
        ///  Количество ячеек умещающихся в высоту инвентаря
        /// </summary>
        public override int CellCountY => (int)(FrameHeight / (cellSizeY + incellOffset.y));

        /// <summary>
        /// Отображает UI сумки
        /// </summary>
        public override void Show()
        {
            if (Visible)
                return;
            base.Show();
        }

        /// <summary>
        /// Скрывает UI сумки
        /// </summary>
        public override void Hide()
        {
            if (Visible == false)
                return;
            Clear();
            base.Hide();
        }

        /// <summary>
        /// Список уже созданных UI компонентов предметов
        /// </summary>
        private readonly List<InventoryItemBehaviour> existsItems = new List<InventoryItemBehaviour>();

        /// <summary>
        /// Очищает список созданных UI компонентов
        /// </summary>
        public void Clear()
        {
            _lastSelectedItemBehaviour = null;
            foreach (var item in existsItems)
                Destroy(item.gameObject);
            existsItems.Clear();
        }

        /// <summary>
        /// Формирует контент GUI для сумки
        /// (Каждому предмету в сумке формируется визуальное представление)
        /// </summary>
        public override void Redraw()
        {
            Clear();
            int flatIndex = 0;
            foreach (var item in Items.ToList())
                CreateItem(item, flatIndex++);

            // Вычисляем высоту контента, чтобы его можно было скроллить
            var countX = Mathf.Max(1, CellCountX);
            var countY = (Items.Count / countX) + 1;
            var deltaSizeY = countY * cellSizeY + countY * incellOffset.y;
            contentContainer.sizeDelta = new Vector2(contentContainer.sizeDelta.x, deltaSizeY);
        }

        /// <summary>
        /// Создаёт UI компонент предмета
        /// </summary>
        /// <param name="item">Предмет который надо создать</param>
        /// <param name="flatIndex">Плоский индекс предмета в сумке</param>
        private void CreateItem(IItem item, int flatIndex)
        {
            // Создаём новый компонент предмета
            var itemComponent = Instantiate(itemBehaviourPrefab, contentContainer);
            itemComponent.Bag = this;
            itemComponent.Item = item;

            int countX = Mathf.Max(1, CellCountX);
            
            // Конвертируем одномерный индекс в двумерный
            var indexX = (flatIndex % countX);
            var indexY = (flatIndex / countX); // countX - это не опечатка
            
            // Рассчёт положения предмета в сумке по индексам
            var posX = indexX * cellSizeX;
            var posY = indexY * cellSizeY;

            // Смещение
            var offsetX = indexX * incellOffset.x;
            var offsetY = indexY * incellOffset.y;

            // Располагаем предмет в сумке
            itemComponent.SetBounds(cellSizeX, cellSizeY, posX, posY, offsetX, offsetY);

            existsItems.Add(itemComponent);
        }

        public override void ClickItem(IItem item)
        {
            // В простейшей реализации - тут ничего не делаем, кому надо переопределит этот метод
        }

        public override void SetSelected(InventoryItemBehaviour selected)
        {
            if (_lastSelectedItemBehaviour == selected)
                return;
            if(_lastSelectedItemBehaviour != null)
                _lastSelectedItemBehaviour.OnUnselected();
            _lastSelectedItemBehaviour = selected;
        }

        public override InventoryItemBehaviour Selected => _lastSelectedItemBehaviour;
        
        private InventoryItemBehaviour _lastSelectedItemBehaviour;
        
    }

}
