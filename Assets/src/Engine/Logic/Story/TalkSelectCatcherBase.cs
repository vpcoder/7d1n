using System;
using Engine.Data;
using Engine.Data.Factories;
using Engine.Logic.Dialog;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story
{
    
    /// <summary>
    ///
    /// Базовый класс взаимодействия с объектами историй для разговоров с NPC.
    /// Класс включае/выключает возможность разговора с NPC в зависимости от его статуса и активности истории.
    /// ---
    /// Basic class for interacting with story objects to talk to NPCs.
    /// The class enables/disables the ability to talk to NPCs depending on their status and story activity.
    /// 
    /// </summary>
    public abstract class TalkSelectCatcherBase : SelectCatcherBase
    {

        [SerializeField] private CharacterNpcBehaviour talker;
        protected override string HintID { get; } = EffectFactory.TALK_HINT;
        public override bool IsDestroyStoreAfterComplete => false;
        
        public override bool IsActive
        {
            get
            {
#if UNITY_EDITOR
                if (talker == null)
                {
                    var parent = transform.parent.gameObject;
                    var npc = parent.GetComponent<CharacterNpcBehaviour>();
                    if (npc == null)
                        throw new NullReferenceException("talk catcher must be in child of CharacterNpcBehaviour!");
                    talker = npc;
                }
                if (talker == null)
                    return false;
#endif
                var status = talker.CharacterContext.Status;
                if (status.IsDead || !status.IsEnabledAI || status.State != CharacterStateType.Normal)
                    return false;
                return base.IsActive;
            }
            set
            {
                base.IsActive = value;
            }
        }

        protected override void StartDialogProcessing(DialogQueue dlg)
        {
            base.StartDialogProcessing(dlg);

            dlg.Run(() =>
            {
                // Запрещаем предикторам формировать новый пулл действий для этого персонажа (он занят разговором)
                // Prevent predictors from forming a new action pool for this character (he is busy talking)
                talker.CharacterContext.Status.IsEnabledAI = false;
                
                // Очищаем текущий пулл действий персонажа, чтобы он не отвлекался от разговора
                // Clear the character's current action pool to keep him from being distracted by the conversation
                talker.StopNPC();
                
                Camera.main.SetState(PlayerEyePos, talker.Eye);
                
                // Поворачиваем NPC в сторону персонажа
                // Turn the NPC towards the character
                StoryActionHelper.NpcLookAt(talker, PlayerCharacter.transform);
            });
        }

        protected override void EndDialogProcessing(DialogQueue dlg)
        {
            dlg.Run(() =>
            {
                // Возобновляем работу предикторов с этим персонажем, придётся составить ему новенький пулл задач...
                // Reactivate the predictors with this character, we'll have to make him a new task pool...
                talker.CharacterContext.Status.IsEnabledAI = true;
            });
            base.EndDialogProcessing(dlg);
        }

        public override void Init()
        {
            base.Init();

#if UNITY_EDITOR
            if (talker == null)
            {
                var parent = transform.parent.gameObject;
                var npc = parent.GetComponent<CharacterNpcBehaviour>();
                if (npc == null)
                    throw new NullReferenceException("talk catcher must be in child of CharacterNpcBehaviour!");
                talker = npc;
            }
#endif
            
            // В различных ситуациях NPC могут отвлекаться на более важные дела и прекращать диалоги,
            // могут возвращаться к нормальному общению. Тут нужно подписаться на смену состояний NPC.
            // In various situations, NPCs may get distracted by more important matters and stop dialogs,
            // may return to normal communication. This is where you need to subscribe to NPC state change.
            var status = talker.CharacterContext.Status;
            status.ChangeDead += OnChangeState;
            status.ChangeEnabledAI += OnChangeState;
            status.ChangeState += OnChangeState;

            // На старте делаем расчёт возможности ведения диалога
            // At the start, we calculate the possibility of dialog
            OnChangeState();
        }

        /// <summary>
        ///     Обновляет состояние хинта (возможность вести диалог с этим НПС)
        ///     ---
        ///     Updates hint status (ability to dialog with this NPC)
        /// </summary>
        private void OnChangeState()
        {
            UpdateHintState();
        }

    }
    
}