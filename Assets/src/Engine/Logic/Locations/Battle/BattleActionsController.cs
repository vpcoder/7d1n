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

        #region Hidden Fields
        
        /// <summary>
        ///     Описание действия совершаемого игроком
        ///     ---
        ///     Description of the action performed by the player
        /// </summary>
        [SerializeField] private Text txtActionDescription;

        /// <summary>
        ///     Кнопка совершения действия
        ///     ---
        ///     Button to perform an action
        /// </summary>
        [SerializeField] private Button btnActionRun;

        #endregion
        
        #region Properties
        
        /// <summary>
        ///     Хранит контекст необходимый для совершения действий перемещения
        ///     ---
        ///     Stores the context needed to perform a move action
        /// </summary>
        public BattleActionMoveContext MoveContext { get; } = new BattleActionMoveContext();
        
        /// <summary>
        ///     Хранит контекст необходимый для совершения действий использования
        ///     ---
        ///     Stores the context necessary to perform actions of use
        /// </summary>
        public BattleActionUseContext UseContext { get; } = new BattleActionUseContext();
        
        /// <summary>
        ///     Хранит контекст необходимый для совершения действий атаки
        ///     ---
        ///     Stores the context required for the attack actions
        /// </summary>
        public BattleActionAttackContext AttackContext { get; } = new BattleActionAttackContext();

        /// <summary>
        ///     Требование к ОД для совершения указанного действия
        ///     ---
        ///     Requirement for AP to perform the specified action
        /// </summary>
        public int NeedAP { get; set; }

        /// <summary>
        ///     Текущий тип совершаемого действия
        ///     ---
        ///     The current type of action being performed
        /// </summary>
        public CharacterBattleAction Action { get; set; }

        #endregion
        
        /// <summary>
        ///     Обновляет сведения на панели совершаемого действия (отображает текст действия, возможность его совершения, стоимость ОД и т.д.)
        ///     ---
        ///     Updates the information on the action panel (displays the text of the action, the possibility to perform it, the cost of AP, etc.)
        /// </summary>
        public void UpdateState()
        {
            if (Action != CharacterBattleAction.Move)
            {
                ObjectFinder.Find<CharacterMoveVisializerController>().HidePath();
                MoveContext.Points = null;
            }

            if (Action != CharacterBattleAction.Use)
            {
                UseContext.UseItem = null;
            }

            if(Action != CharacterBattleAction.Attack)
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
            btnActionRun.enabled = !Game.Instance.Runtime.BattleFlag || Game.Instance.Runtime.BattleContext.CurrentCharacterAP >= NeedAP;
        }

        /// <summary>
        ///     Локализует текст действия, в зависимости от типа действия, оружия в руках и т.д.
        ///     ---
        ///     Localizes the text of the action, depending on the type of action, weapon in hand, etc.
        /// </summary>
        /// <param name="action">
        ///     Совершаемое действие
        ///     ---
        ///     The action being performed
        /// </param>
        /// <returns>
        ///     Локализованную строку с описанием того что будет совершено
        ///     ---
        ///     A localized string describing what will be done
        /// </returns>
        private string GetMessageBattleAction(CharacterBattleAction action)
        {
            switch(action)
            {
                case CharacterBattleAction.Move:
                    return Localization.Instance.Get("msg_battle_action_move");
                case CharacterBattleAction.Attack:
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
                case CharacterBattleAction.Use:
                    return Localization.Instance.Get("msg_battle_action_use");
            }
            return "?";
        }

        /// <summary>
        ///     Формирует текст сообщения для описания текущего действия, которое хочет совершить игрок
        ///     ---
        ///     Generates a message text to describe the current action the player wants to take
        /// </summary>
        /// <returns>
        ///     Текст описывающий действие выполняемое персонажем, возможность его совершения, и стоимость в ОД, если игрок в бою
        ///     ---
        ///     Text describing the action performed by the character, the possibility of performing it, and the cost in AP if the player is in combat
        /// </returns>
        private string CreateActionMessage()
        {
            var builder = new StringBuilder();
            builder.Append(Localization.Instance.Get("msg_battle_action_description1")); // Вы совершаете
            builder.Append(" <color=\"#ff0\">");
            builder.Append(GetMessageBattleAction(Action));
            builder.Append(" </color>\r\n");

            if (Game.Instance.Runtime.BattleFlag) // Во время битвы важны траты ОД, показываем их в окне действия, без битвы расход ОД не важен, поэтому мы его прячем
            {
                builder.Append(Localization.Instance.Get("msg_battle_action_description2"));
                builder.Append(Game.Instance.Runtime.BattleContext.CurrentCharacterAP >= NeedAP ? ": <color=\"#0f0\">" : ": <color=\"#f00\">");
                builder.Append(NeedAP);
                builder.Append(" </color>");
                builder.Append(Localization.Instance.Get("msg_ap"));
            }
            
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
        private IBattleActionContext GetContextByAction(CharacterBattleAction action)
        {
            switch(action)
            {
                case CharacterBattleAction.Move   : return MoveContext;
                case CharacterBattleAction.Use    : return UseContext;
                case CharacterBattleAction.Attack : return AttackContext;
                default:
                    throw new NotSupportedException("value '" + action + "' isn't supported!");
            }
        }

        /// <summary>
        ///     Клик по кнопке "Выполнить"
        ///     ---
        ///     Click the "Run" button
        /// </summary>
        public void DoActionClick()
        {
            if (Game.Instance.Runtime.BattleContext.CurrentCharacterAP < NeedAP
                && Game.Instance.Runtime.BattleFlag)
                return;

            var context = GetContextByAction(Action);
            CharacterBattleActionFactory.Instance.InvokeProcess(Action, context);
        }

        /// <summary>
        ///     Клик по кнопке "Отменить"
        ///     ---
        ///     Click the "Cancel" button
        /// </summary>
        public void DoCancelClick()
        {
            var context = GetContextByAction(Action);
            CharacterBattleActionFactory.Instance.InvokeRollback(Action, context);
        }

    }

}
