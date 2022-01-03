using Engine.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic
{

    /// <summary>
    /// Абстрактный предмет в сумке
    /// Элемент UI
    /// </summary>
    public class AbstractItem : MonoBehaviour
    {

        #region Hidden Fields

        [SerializeField] private Color normalColor;
        [SerializeField] private Color selectedColor;

        [SerializeField] private IItem item;
        [SerializeField] private IBag  bag;

        [SerializeField] private Text  weight;
        [SerializeField] private Text  count;
        [SerializeField] private Image icon;
        [SerializeField] private Image background;

        #endregion

        #region Properties

        public bool IsSelected { get; set; }

        /// <summary>
        /// Информация о текущем предмете
        /// </summary>
        public IItem Item
        {
            get
            {
                return this.item;
            }
            set
            {
                this.item = value;
                this.count.text = item.Count.ToString();
                this.weight.text = EntityCalculationService.GetWeightFormat(item.Weight);
                this.icon.sprite = item.Sprite;
            }
        }

        /// <summary>
        /// Сумка в которой находится текущий предмет
        /// </summary>
        public IBag Bag
        {
            get
            {
                return this.bag;
            }
            set
            {
                this.bag = value;
            }
        }

        #endregion

        /// <summary>
        /// Устанавливает изображение предмета внутри сумки в указанном месте
        /// </summary>
        /// <param name="sizeX">Размер предмета по X</param>
        /// <param name="sizeY">Размер предмета по Y</param>
        /// <param name="posX">Положение предмета по X</param>
        /// <param name="posY">Положение предмета по Y</param>
        /// <param name="offsetX">Смещение положения по X</param>
        /// <param name="offsetY">Смещение положения по Y</param>
        public void SetBounds(float sizeX, float sizeY, float posX, float posY, float offsetX, float offsetY)
        {
            var rect = (RectTransform)transform;
            rect.sizeDelta = new Vector2(sizeX, sizeY);
            rect.localPosition = new Vector2(posX + offsetX, -posY - offsetY);
        }



        /// <summary>
        /// Надо убрать выделение с этого предмета
        /// </summary>
        public void OnUnselected()
        {
            if(IsSelected)
                background.color = normalColor;
            IsSelected = false;
        }

        /// <summary>
        /// Игрок кликнул на предмет
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
