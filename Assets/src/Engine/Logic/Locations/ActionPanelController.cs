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

        private LocationObjectItemBehaviour _selectedItemBehaviour;

        public void Show(LocationObjectItemBehaviour objectItemBehaviour)
        {
            Game.Instance.Runtime.Mode = Mode.GUI;

            _selectedItemBehaviour = objectItemBehaviour;

            IItem item = objectItemBehaviour.Item;

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
            if (_selectedItemBehaviour == null)
            {
                Hide();
                return;
            }

            if(!CanPickup(_selectedItemBehaviour))
            {
                var character = ObjectFinder.Find<LocationCharacter>().transform;
                UIHintMessageManager.Show(hintMessagePrefab.gameObject, character.position, Localization.Instance.Get("msg_error_cant_pickup_item"));
                Hide();
                return;
            }

            var door = _selectedItemBehaviour.GetComponent<DoorController>();
            if (door != null)
                door.State = DoorState.OPENED;

            Game.Instance.Character.Inventory.Add(_selectedItemBehaviour.ID, 1);
            GameObject.Destroy(_selectedItemBehaviour.gameObject);

            Hide();
        }

        public bool CanPickup(LocationObjectItemBehaviour objectItemBehaviour)
        {
            if (objectItemBehaviour.transform.childCount == 0)
                return true;
            for(int child = 0; child < objectItemBehaviour.transform.childCount; child++)
            {
                var childObjectItem = objectItemBehaviour.transform.GetChild(child).GetComponent<LocationObjectItemBehaviour>();
                if (childObjectItem != null)
                    return false;
            }
            return true;
        }

    }

}
