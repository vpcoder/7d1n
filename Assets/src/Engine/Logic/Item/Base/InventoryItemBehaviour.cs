using Engine.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic
{

    /// <summary>
    /// 
    /// UI модель предмета, необходимая для того чтобы продемонстрировать предмет игроку в сумках
    /// ---
    /// UI model of the item needed to demonstrate the item to the player in the bags
    /// 
    /// </summary>
    public class InventoryItemBehaviour : MonoBehaviour
    {

        #region Hidden Fields

        [SerializeField] private Color normalColor;
        [SerializeField] private Color selectedColor;

        [SerializeField] private Text  weight;
        [SerializeField] private Text  count;
        [SerializeField] private Image icon;
        [SerializeField] private Image background;
        
        private IItem item;
        private IBag  bag;

        #endregion

        #region Properties

        /// <summary>
        ///     Состояние предмета - выделен ли он в данный момент времени?
        ///     ---
        ///     The state of the object - is it highlighted at this point in time?
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        ///     Экземпляр предмета, для которого была создана данная UI модель
        ///     ---
        ///     Instance of the object for which this UI model was created
        /// </summary>
        public IItem Item
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
                count.text = item.Count.ToString();
                weight.text = WeightCalculationService.GetWeightFormat(item.Weight);
                icon.sprite = item.Sprite;
            }
        }

        /// <summary>
        ///     Ссылка на экземпляр сумки в рамках которой отображается этот предмет
        ///     Сумка ссылается на предмет, предмет на сумку, таким образом сумка перехватывает клики на предметах
        ///     ---
        ///     Reference to the bag instance within which this item is displayed
        ///     The bag refers to the object, the object to the bag, thus the bag intercepts clicks on the objects
        /// </summary>
        public IBag Bag
        {
            get
            {
                return bag;
            }
            set
            {
                bag = value;
            }
        }

        #endregion

        /// <summary>
        ///     Инициирует предмет находящийся в инвентаре
        ///     Устанавливает положение и размеры иконке предмета
        ///     ---
        ///     Initiates an item in the inventory
        ///     Sets the position and size of the item icon
        /// </summary>
        /// <param name="sizeX">Размер иконки предмета по X</param>
        /// <param name="sizeY">Размер иконки предмета по Y</param>
        /// <param name="posX">Положение предмета по сетке инвентаря по X</param>
        /// <param name="posY">Положение предмета по сетке инвентаря по Y</param>
        /// <param name="offsetX">Смещение положения предмета по X</param>
        /// <param name="offsetY">Смещение положения предмета по Y</param>
        public void SetBounds(float sizeX, float sizeY, float posX, float posY, float offsetX, float offsetY)
        {
            var rect = (RectTransform)transform;
            rect.sizeDelta = new Vector2(sizeX, sizeY);
            rect.localPosition = new Vector2(posX + offsetX, -posY - offsetY);
        }
        
        /// <summary>
        ///     Снимает выделение с предмета
        ///     ---
        ///     Deselects the item
        /// </summary>
        public void OnUnselected()
        {
            if(IsSelected)
                background.color = normalColor;
            IsSelected = false;
        }

        /// <summary>
        ///     Выполняет выделение предмета (игрок кликает по предмету в инвентаре)
        ///     ---
        ///     Selects an item (the player clicks on the item in the inventory)
        /// </summary>
        public void OnSelected()
        {
            IsSelected = true;
            bag.SetSelected(this);
            bag.ClickItem(Item);
            background.color = selectedColor;
        }

    }

}
