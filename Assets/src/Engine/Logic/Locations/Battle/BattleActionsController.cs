using Engine.Data;
using Engine.EGUI;
using Engine.Logic.Locations.Battle.Actions;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Управление всеми действиями которые может совершить игрок во время боя
    /// ---
    /// Control all the actions that the player can perform during combat
    /// 
    /// </summary>
    public class BattleActionsController : Panel
    {

        /// <summary>
        /// Описание действия совершаемого игроком
        /// </summary>
        [SerializeField] private Text txtActionDescription;

        /// <summary>
        /// Кнопка совершения действия
        /// </summary>
        [SerializeField] private Button btnActionRun;

        public BattleActionMoveContext MoveContext { get; } = new BattleActionMoveContext();
        public BattleActionUseContext UseContext { get; } = new BattleActionUseContext();
        public BattleActionAttackContext AttackContext { get; } = new BattleActionAttackContext();

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

        /// <summary>
        ///     Выполняет поиск контекста по типу действия
        ///     ---
        ///     Performs a context search by type of action
        /// </summary>
        /// <param name="action">
        ///     Действие, по которому выполняется поиск контекста
        ///     ---
        ///     Action on which the context search is performed
        /// </param>
        /// <returns>
        ///     Контекст, соответствующий указанному действию action
        ///     ---
        ///     Context corresponding to the specified action
        /// </returns>
        private IBattleActionContext GetContextByAction(BattleAction action)
        {
            switch(action)
            {
                case BattleAction.Move   : return MoveContext;
                case BattleAction.Use    : return UseContext;
                case BattleAction.Attack : return AttackContext;
                default:
                    throw new NotSupportedException("value '" + action.ToString() + "' isn't supported!");
            }
        }

        /// <summary>
        /// Клик по кнопке "Выполнить"
        /// ---
        /// Click the "Run" button
        /// </summary>
        public void DoActionClick()
        {
            if (Game.Instance.Runtime.BattleContext.CurrentCharacterAP < NeedAP)
                return;

            var context = GetContextByAction(Action);
            BattleActionFactory.Instance.InvokeProcess(Action, context);
        }

        /// <summary>
        /// Клик по кнопке "Отменить"
        /// ---
        /// Click the "Cancel" button
        /// </summary>
        public void DoCancelClick()
        {
            var context = GetContextByAction(Action);
            BattleActionFactory.Instance.InvokeRollback(Action, context);
        }

    }

}
