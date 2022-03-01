using Engine.Data;
using Engine.Logic.Locations.Battle;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Контроллер управляющий доступными быстрыми слотами оружия
    /// </summary>
    public class HandsController : MonoBehaviour
    {

        /// <summary>
        /// Все доступные слоты
        /// </summary>
        [SerializeField] private List<HandCellItem> hands;

        [SerializeField] private HandsActionsController actionController;

        private void Start()
        {
            if (Game.Instance.Runtime.Scene != Scenes.SceneName.Location)
                Hide();
        }

        public void Show()
        {
            foreach (var hand in hands)
                hand.gameObject.SetActive(true);
        }

        public void Hide()
        {
            foreach (var hand in hands)
                hand.gameObject.SetActive(false);
        }

        public HandCellItem Selected { get; set; }

        public HandCellItem GetCell(int index)
        {
            return hands[index];
        }

        public void TryRemoveItem(IItem item)
        {
            actionController.HideActions();
            foreach (var hand in hands)
            {
                if(hand.Weapon == item)
                {
                    hand.Weapon = null;
                    return;
                }
            }
        }

        public void DoResetSelectedCell(HandCellItem selected)
        {
            if (selected != null)
                selected.DoUnselect();

            actionController.HideActions();
            Game.Instance.Runtime.ActionMode = ActionMode.Move;
            Selected = null;

            ObjectFinder.Find<BattleActionsController>().Hide();
            ObjectFinder.Find<CharacterAimController>().Hide();
        }

        public void DoSelectHand(HandCellItem selected)
        {
            foreach (var hand in hands)
                if (hand != selected)
                    hand.DoUnselect();

            if(selected.IsSelected)
            {
                DoResetSelectedCell(selected);
                return;
            }
            selected.DoSelect();
            Selected = selected;

            actionController.HideActions();

            // if (Game.Instance.Runtime.Mode != Mode.Battle)
            //    return;

            if (selected.Weapon != null)
            {
                ObjectFinder.Find<LocationCharacter>().EquipWeapon(selected.Weapon);
                switch (selected.Weapon.Type)
                {
                    case GroupType.WeaponGrenade:
                        actionController.ShowActions(HandActionType.ThrowGrenade);
                        break;
                    case GroupType.WeaponEdged:
                        var edged = (IEdgedWeapon)selected.Weapon;
                        if (edged.CanThrow)
                            actionController.ShowActions(HandActionType.AttackEdged, HandActionType.ThrowEdged);
                        else
                            actionController.ShowActions(HandActionType.AttackEdged);
                        break;
                    case GroupType.WeaponFirearms:
                        actionController.ShowActions(HandActionType.AttackFirearms, HandActionType.ReloadFirearms);
                        break;
                }
            }

            ObjectFinder.Find<BattleActionsController>().Hide();
        }

        public void DoSelectHandAction(HandActionType action)
        {
            ObjectFinder.Find<CharacterAimController>().DoHandsActionClick(action, Selected?.Weapon);
        }

    }

}
