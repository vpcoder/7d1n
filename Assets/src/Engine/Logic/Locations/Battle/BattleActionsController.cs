using Engine.Data;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Управление всеми действиями которые может совершить игрок во время боя
    /// </summary>
    public class BattleActionsController : MonoBehaviour
    {
        /// <summary>
        /// Контейнер
        /// </summary>
        [SerializeField] private GameObject body;

        /// <summary>
        /// Описание действия совершаемого игроком
        /// </summary>
        [SerializeField] private Text txtActionDescription;

        /// <summary>
        /// Кнопка совершения действия
        /// </summary>
        [SerializeField] private Button btnActionRun;

        /// <summary>
        /// Кнопка отмены действия
        /// </summary>
        [SerializeField] private Button btnActionCancel;

        /// <summary>
        /// Выбранная рука и выбранное действие
        /// </summary>
        [SerializeField] private HandsController handsController;

        public BattleActionMoveContext MoveContext = new BattleActionMoveContext();
        public BattleActionUseContext UseContext = new BattleActionUseContext();
        public BattleActionAttackContext AttackContext = new BattleActionAttackContext();

        public void Show()
        {
            Visible = true;
            body.SetActive(true);
        }

        public void Hide()
        {
            Visible = false;
            body.SetActive(false);
        }

        public bool Visible
        {
            get;
            set;
        } = false;

        public int NeedAP
        {
            get;
            set;
        }

        public BattleAction Action
        {
            get;
            set;
        }

        public void UpdateState()
        {
            if (Action != BattleAction.Move)
            {
                ObjectFinder.Find<CharacterMoveVisializerController>().HidePath();
                MoveContext.Points = null;
            }

            if (Action != BattleAction.Use)
            {
                UseContext.UseItem = null;
            }

            if(Action != BattleAction.Attack)
            {
                AttackContext.Weapon = null;
                if (AttackContext.AttackMarker != null)
                    GameObject.Destroy(AttackContext.AttackMarker);
                AttackContext.AttackMarker = null;
                var handsActionsController = ObjectFinder.Find<HandsActionsController>();
                handsActionsController.DoUnselectActions();
                handsActionsController.HideActions();
            }

            txtActionDescription.text = CreateActionMessage();
            btnActionRun.enabled = Game.Instance.Runtime.BattleContext.CurrentCharacterAP >= NeedAP;
        }

        private string GetMessageBattleAction(BattleAction action)
        {
            switch(action)
            {
                case BattleAction.Move:
                    return Localization.Instance.Get("msg_battle_action_move");
                case BattleAction.Attack:
                    switch(AttackContext.Action)
                    {
                        case HandActionType.AttackFirearms:
                            return Localization.Instance.Get("msg_battle_action_attack_firearms");
                        case HandActionType.ReloadFirearms:
                            return Localization.Instance.Get("msg_battle_action_reload_firearms");
                        case HandActionType.AttackEdged:
                            return Localization.Instance.Get("msg_battle_action_attack_edged");
                        case HandActionType.ThrowEdged:
                            return Localization.Instance.Get("msg_battle_action_throw_edged");
                        case HandActionType.ThrowGrenade:
                            return Localization.Instance.Get("msg_battle_action_throw_grenade");
                    }
                    break;
                case BattleAction.Use:
                    return Localization.Instance.Get("msg_battle_action_use");
            }
            return "?";
        }

        private string CreateActionMessage()
        {
            var builder = new StringBuilder();
            builder.Append(Localization.Instance.Get("msg_battle_action_description1")); // Вы совершаете
            builder.Append(" <color=\"#ff0\">");
            builder.Append(GetMessageBattleAction(Action));
            builder.Append(" </color>\r\n");
            builder.Append(Localization.Instance.Get("msg_battle_action_description2"));

            if(Game.Instance.Runtime.BattleContext.CurrentCharacterAP >= NeedAP)
                builder.Append(": <color=\"#0f0\">");
            else
                builder.Append(": <color=\"#f00\">");

            builder.Append(NeedAP);
            builder.Append(" </color>");
            builder.Append(Localization.Instance.Get("msg_ap"));
            return builder.ToString();
        }

        public void DoActionClick()
        {
            if (Game.Instance.Runtime.BattleContext.CurrentCharacterAP < NeedAP)
                return;

            switch(Action)
            {
                case BattleAction.Move:
                    DoMove();
                    break;
                case BattleAction.Use:
                    DoUseLocationObject();
                    break;
                case BattleAction.Attack:
                    DoAttack();
                    break;
            }
        }

        public void DoCancelClick()
        {
            switch (Action)
            {
                case BattleAction.Move:
                    DoCancelMove();
                    break;
                case BattleAction.Use:
                    DoCancelUseLocationObject();
                    break;
                case BattleAction.Attack:
                    DoCancelAttack();
                    break;
            }
        }

        public void DoCancelAttack()
        {
            NeedAP = 0;

            AttackContext.Weapon = null;
            if (AttackContext.AttackMarker != null)
                GameObject.Destroy(AttackContext.AttackMarker);
            AttackContext.AttackMarker = null;

            var handsActionsController = ObjectFinder.Find<HandsActionsController>();
            handsActionsController.DoUnselectActions();
            handsActionsController.HideActions();

            Hide();
        }

        public void DoCancelMove()
        {
            NeedAP = 0;

            ObjectFinder.Find<CharacterMoveVisializerController>().HidePath();
            MoveContext.Points = null;

            Hide();
        }

        public void DoCancelUseLocationObject()
        {
            UseContext.UseItem = null;

            Hide();
        }

        public void DoUseLocationObject()
        {
            Game.Instance.Runtime.BattleContext.CurrentCharacterAP -= NeedAP; // Тратим ОД

            var useObject = UseContext.UseItem.GetComponent<IUseObjectController>();
            if(useObject != null)
            {
                useObject.DoUse();
                Hide();
                return;
            }
        }

        public void DoAttack()
        {
            var handsController = ObjectFinder.Find<HandsController>();
            var handsActionsController = ObjectFinder.Find<HandsActionsController>();
            var character = ObjectFinder.Find<LocationCharacter>();
            if(AttackContext.Weapon != null)
            {
                character.Weapon = AttackContext.Weapon;

                switch (AttackContext.Weapon.Type)
                {
                    case GroupType.WeaponEdged:

                        if (AttackContext.Action == HandActionType.AttackEdged && AttackContext.Target != null) // Атакуем ножом вблизи
                        {

                            BattleCalculationService.DoEdgedAttack(character, AttackContext.Target);

                            AttackContext.Weapon = null;
                            if (AttackContext.AttackMarker != null)
                                GameObject.Destroy(AttackContext.AttackMarker);

                            Game.Instance.Runtime.BattleContext.CurrentCharacterAP -= NeedAP; // Тратим ОД
                            Hide();
                        }

                        if (AttackContext.Action == HandActionType.ThrowEdged) // Кидаем нож
                        {
                            var edged = (IEdgedWeapon)AttackContext.Weapon;
                            character.TargetAttackPos = AttackContext.AttackMarker.transform.position;
                            BattleCalculationService.DoEdgedThrowAttack(character);

                            Game.Instance.Character.Inventory.RemoveByAddress(edged); // Удаляем то что выкинули
                            Game.Instance.Character.Equipment.TryRemoveItem(edged); // Убираем из экипировки
                            handsController.TryRemoveItem(edged); // Убираем из рук
                            handsController.Selected?.UpdateCellInfo();

                            AttackContext.Weapon = null;
                            if (AttackContext.AttackMarker != null)
                                GameObject.Destroy(AttackContext.AttackMarker);

                            Game.Instance.Runtime.BattleContext.CurrentCharacterAP -= NeedAP; // Тратим ОД
                            Hide();
                        }

                        break;
                    case GroupType.WeaponFirearms:
                        var firearms = (IFirearmsWeapon)AttackContext.Weapon;

                        if (AttackContext.Action == HandActionType.AttackFirearms)
                        {
                            if (firearms.AmmoCount > 0)
                            {
                                character.TargetAttackPos = AttackContext.AttackMarker.transform.position;

                                BattleCalculationService.DoFirearmsAttack(character);
                                firearms.AmmoCount--;
                                handsController.Selected?.UpdateCellInfo();

                                AttackContext.Weapon = null;
                                if (AttackContext.AttackMarker != null)
                                    GameObject.Destroy(AttackContext.AttackMarker);

                                Game.Instance.Runtime.BattleContext.CurrentCharacterAP -= NeedAP; // Тратим ОД
                                Hide();
                            }
                        }

                        if(AttackContext.Action == HandActionType.ReloadFirearms)
                        {
                            long needAmmo = firearms.AmmoStackSize - firearms.AmmoCount;
                            long haveAmmo = Game.Instance.Character.Inventory.HasCount(firearms.AmmoID);

                            int valueAmmo = Mathf.Min((int)needAmmo, (int)haveAmmo);
                            if (valueAmmo > 0)
                            {
                                AudioController.Instance.PlaySound(firearms.ReloadSoundType);
                                Game.Instance.Runtime.BattleContext.CurrentCharacterAP -= NeedAP; // Тратим ОД
                                Game.Instance.Character.Inventory.Remove(firearms.AmmoID, valueAmmo);
                                firearms.AmmoCount += valueAmmo;
                                handsController.Selected?.UpdateCellInfo();
                                Hide();
                            }
                        }

                        break;
                    case GroupType.WeaponGrenade:
                        var grenade = (IGrenadeWeapon)AttackContext.Weapon;

                        if (AttackContext.Action == HandActionType.ThrowGrenade)
                        {
                            character.TargetAttackPos = AttackContext.AttackMarker.transform.position;

                            BattleCalculationService.DoGrenadeAttack(character);

                            Game.Instance.Character.Inventory.RemoveByAddress(grenade); // Удаляем то что выкинули
                            Game.Instance.Character.Equipment.TryRemoveItem(grenade); // Убираем из экипировки
                            handsController.TryRemoveItem(grenade); // Убираем из рук

                            handsController.Selected?.UpdateCellInfo();

                            AttackContext.Weapon = null;
                            if (AttackContext.AttackMarker != null)
                                GameObject.Destroy(AttackContext.AttackMarker);

                            Game.Instance.Runtime.BattleContext.CurrentCharacterAP -= NeedAP; // Тратим ОД
                            Hide();
                        }
                        break;
                }
            }

            handsActionsController.DoUnselectActions();

            AttackContext.Weapon = null;
            if (AttackContext.AttackMarker != null)
                GameObject.Destroy(AttackContext.AttackMarker);
            AttackContext.AttackMarker = null;
        }

        public void DoMove()
        {
            Game.Instance.Runtime.BattleContext.CurrentCharacterAP -= NeedAP; // Тратим ОД

            var character = ObjectFinder.Find<LocationCharacter>();
            character.SetPath(MoveContext.Points); // Перемещаемся

            ObjectFinder.Find<CharacterMoveVisializerController>().HidePath(); // Сбрасываем путь
            MoveContext.Points = null;

            Hide();
        }

    }

}
