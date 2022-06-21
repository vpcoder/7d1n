using Engine.Data;
using Engine.DB.I18n;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic
{

    public class InventoryActionPanelController : MonoBehaviour
    {

        [SerializeField] private Image icon;
        [SerializeField] private Button btnDrop;
        [SerializeField] private Button btnUse;
        [SerializeField] private Text txtName;
        [SerializeField] private Text txtDescription;
        [SerializeField] private Text txtDetail;

        private IItem item;

        public IItem Item
        {
            get
            {
                return this.item;
            }
            set
            {
                this.item = value;

                if(item == null)
                {
                    icon.gameObject.SetActive(false);
                    txtName.gameObject.SetActive(false);
                    txtDescription.gameObject.SetActive(false);
                    txtDetail.gameObject.SetActive(false);
                    btnUse.gameObject.SetActive(false);
                    btnDrop.gameObject.SetActive(false);
                }
                else
                {
                    icon.gameObject.SetActive(true);
                    txtName.gameObject.SetActive(true);
                    txtDescription.gameObject.SetActive(true);
                    txtDetail.gameObject.SetActive(true);
                    btnDrop.gameObject.SetActive(true);

                    icon.sprite = item.Sprite;
                    txtName.text = Localization.Instance.Get(item.Name);
                    txtDetail.text = WeightCalculationService.GetWeightFormat(item.Weight * item.Count);
                    txtDescription.text = Localization.Instance.Get(item.Description);

                    if (item.Type.IsOneOf(GroupType.Used,
                                                    GroupType.Food,
                                                    GroupType.MedKit))
                    {
                        btnUse.gameObject.SetActive(true);
                    }
                }
            }
        }

    }

}
