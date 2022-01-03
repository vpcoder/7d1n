using Engine.Data;
using Engine.EGUI;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Панель дополнительных действий над объектами в локации
    /// </summary>
    public class ActionPanelController : MonoBehaviour
    {

        [SerializeField] private UIHintMessageWorldPosition hintMessagePrefab;
        [SerializeField] private Text txtName;
        [SerializeField] private Text txtDescription;
        [SerializeField] private Image imgIcon;
        [SerializeField] private GameObject actionPanel;

        private LocationObjectItem selectedItem;

        public void Show(LocationObjectItem objectItem)
        {
            Game.Instance.Runtime.Mode = Mode.GUI;

            selectedItem = objectItem;

            IItem item = objectItem.Item;

            txtName.text = Localization.Instance.Get(item.Name);
            txtDescription.text = Localization.Instance.Get(item.Description);
            imgIcon.sprite = item.Sprite;

            actionPanel.SetActive(true);
        }

        public void Hide()
        {
            Game.Instance.Runtime.Mode = Mode.Game;
            actionPanel.SetActive(false);
        }

        public void OnScrapClick()
        {

            Hide();
        }

        public void OnPickUpClick()
        {
            if (selectedItem == null)
            {
                Hide();
                return;
            }

            if(!CanPickup(selectedItem))
            {
                var character = ObjectFinder.Find<LocationCharacter>().transform;
                UIHintMessageManager.Show(hintMessagePrefab.gameObject, character.position, Localization.Instance.Get("msg_error_cant_pickup_item"));
                Hide();
                return;
            }

            var door = selectedItem.GetComponent<DoorController>();
            if (door != null)
                door.State = DoorState.OPENED;

            Game.Instance.Character.Inventory.Add(selectedItem.ID, 1);
            GameObject.Destroy(selectedItem.gameObject);

            Hide();
        }

        public bool CanPickup(LocationObjectItem objectItem)
        {
            if (objectItem.transform.childCount == 0)
                return true;
            for(int child = 0; child < objectItem.transform.childCount; child++)
            {
                var childObjectItem = objectItem.transform.GetChild(child).GetComponent<LocationObjectItem>();
                if (childObjectItem != null)
                    return false;
            }
            return true;
        }

    }

}
