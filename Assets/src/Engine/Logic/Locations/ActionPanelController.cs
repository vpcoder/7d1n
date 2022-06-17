using Engine.Data;
using Engine.EGUI;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Панель дополнительных действий над объектами в локации
    /// ---
    /// Panel of additional actions on objects in the location
    /// 
    /// </summary>
    public class ActionPanelController : Panel
    {

        #region Hidden Fields
        
        [SerializeField] private UIHintMessageWorldPosition hintMessagePrefab;
        [SerializeField] private Text txtName;
        [SerializeField] private Text txtDescription;
        [SerializeField] private Image imgIcon;

        private LocationObjectItemBehaviour selectedItemBehaviour;

        #endregion
        
        public void Show(LocationObjectItemBehaviour selectedItemBehaviour)
        {
            Game.Instance.Runtime.Mode = Mode.GUI;

            this.selectedItemBehaviour = selectedItemBehaviour;

            var item = selectedItemBehaviour.Item;

            txtName.text = Localization.Instance.Get(item.Name);
            txtDescription.text = Localization.Instance.Get(item.Description);
            imgIcon.sprite = item.Sprite;

            base.Show();
        }

        public override void Hide()
        {
            Game.Instance.Runtime.Mode = Mode.Game;
            base.Hide();
        }

        public void OnScrapClick()
        {
            ObjectFinder.Find<ScrapPanelController>().Show(selectedItemBehaviour, this);
            // Hide();
        }

        public void OnPickUpClick()
        {
            if (selectedItemBehaviour == null)
            {
                Hide();
                return;
            }

            if(!CanPickup(selectedItemBehaviour))
            {
                var character = ObjectFinder.Find<LocationCharacter>().transform;
                UIHintMessageManager.Show(hintMessagePrefab.gameObject, character.position, Localization.Instance.Get("msg_error_cant_pickup_item"));
                Hide();
                return;
            }

            var door = selectedItemBehaviour.GetComponent<DoorController>();
            if (door != null)
                door.State = DoorState.OPENED;

            Game.Instance.Character.Inventory.Add(selectedItemBehaviour.Item);
            Destroy(selectedItemBehaviour.gameObject);

            Hide();
        }

        /// <summary>
        ///     Определяет, можно ли взять объект в инвентарь?
        ///     ---
        ///     Determines if an object can be taken into inventory?
        /// </summary>
        /// <param name="objectItemBehaviour">
        ///     Предмет, который хотят поднять
        ///     ---
        ///     The subject they want to raise
        /// </param>
        /// <returns>
        ///     Вернёт false, если:
        ///         - У объекта есть дочерние объекты (например, нельзя поднимать шкаф с предметами внутри него)
        ///     В остальнях случаях вернёт true
        ///     ---
        ///     Returns false if:
        ///         - The object has child objects (for example, you can't bring up a closet with items inside it)
        ///     In other cases it returns true
        /// </returns>
        private bool CanPickup(LocationObjectItemBehaviour objectItemBehaviour)
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
