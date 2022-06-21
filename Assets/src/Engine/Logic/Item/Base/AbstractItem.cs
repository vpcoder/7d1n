using Engine.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic
{

    /// <summary>
    /// ����������� ������� � �����
    /// ������� UI
    /// </summary>
    public class AbstractItem : MonoBehaviour
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

        public bool IsSelected { get; set; }

        /// <summary>
        /// ���������� � ������� ��������
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
                this.weight.text = WeightCalculationService.GetWeightFormat(item.Weight);
                this.icon.sprite = item.Sprite;
            }
        }

        /// <summary>
        /// ����� � ������� ��������� ������� �������
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
        /// ������������� ����������� �������� ������ ����� � ��������� �����
        /// </summary>
        /// <param name="sizeX">������ �������� �� X</param>
        /// <param name="sizeY">������ �������� �� Y</param>
        /// <param name="posX">��������� �������� �� X</param>
        /// <param name="posY">��������� �������� �� Y</param>
        /// <param name="offsetX">�������� ��������� �� X</param>
        /// <param name="offsetY">�������� ��������� �� Y</param>
        public void SetBounds(float sizeX, float sizeY, float posX, float posY, float offsetX, float offsetY)
        {
            var rect = (RectTransform)transform;
            rect.sizeDelta = new Vector2(sizeX, sizeY);
            rect.localPosition = new Vector2(posX + offsetX, -posY - offsetY);
        }



        /// <summary>
        /// ���� ������ ��������� � ����� ��������
        /// </summary>
        public void OnUnselected()
        {
            if(IsSelected)
                background.color = normalColor;
            IsSelected = false;
        }

        /// <summary>
        /// ����� ������� �� �������
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
