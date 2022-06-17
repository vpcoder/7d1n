using Engine.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Engine.Logic
{

    /// <summary>
    /// Простая сумка
    /// </summary>
	public class Bag : AbstractBag
    {


        #pragma warning disable 0649, IDE0044, CS0414

        [SerializeField] private int cellSizeX;
        [SerializeField] private int cellSizeY;

        [SerializeField] private RectTransform contentContainer;
        [SerializeField] private RectTransform viewportContainer;

        [SerializeField] private AbstractItem itemPrefab;

        #pragma warning restore 0649, IDE0044, CS0414

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
        public override int CellCountX
        {
            get
            {
                return FrameWidth / cellSizeX;
            }
        }

        /// <summary>
        ///  Количество ячеек умещающихся в высоту инвентаря
        /// </summary>
        public override int CellCountY
        {
            get
            {
                return FrameHeight / cellSizeY;
            }
        }

        /// <summary>
        /// Отображает UI сумки
        /// </summary>
        public override void Show()
        {
            if (Visible == true)
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
        private readonly List<AbstractItem> existsItems = new List<AbstractItem>();

        /// <summary>
        /// Очищает список созданных UI компонентов
        /// </summary>
        public void Clear()
        {
            foreach (var item in existsItems)
                GameObject.Destroy(item.gameObject);
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
            var deltaSizeY = ((Items.Count / countX) + 1) * cellSizeY;
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
            AbstractItem itemComponent = GameObject.Instantiate<AbstractItem>(itemPrefab, contentContainer);
            itemComponent.Bag = this;
            itemComponent.Item = item;

            int countX = Mathf.Max(1, CellCountX);

            // Рассчёт положения предмета в сумке по плоскому индексу
            float posX = (flatIndex % countX) * cellSizeX;
            float posY = (flatIndex / countX) * cellSizeY; // countX - это не опечатка

            // Смещение
            float offsetX = 0;
            float offsetY = 0;

            // Располагаем предмет в сумке
            itemComponent.SetBounds(cellSizeX, cellSizeY, posX, posY, offsetX, offsetY);

            existsItems.Add(itemComponent);
        }

        public override void ClickItem(IItem item)
        {

        }

        public override void SetSelected(AbstractItem selected)
        {
            foreach (var item in existsItems)
                if (item != selected)
                    item.OnUnselected();
        }

    }

}
