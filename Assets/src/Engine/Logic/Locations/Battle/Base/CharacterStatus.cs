using System;
using Engine.Data;
using UnityEngine;

namespace Engine.Logic.Locations
{

    [Serializable]
    public class CharacterStatus
    {

        public event Action ChangeEnabledAI;
        public event Action ChangeState;
        public event Action ChangeDead;

        [SerializeField] private bool isEnabledAI = true;
        [SerializeField] private CharacterStateType state = CharacterStateType.Normal;
        [SerializeField] private bool isDead = false;
        
        /// <summary>
        ///     Переключатель ИИ
        ///     Можно выключить ИИ, например, для того чтобы персонаж выполнил сюжетные действия, не отвлекаясь на обстановку и врагов,
        ///     затем включить, чтобы персонаж начал действовать по обстановке.
        ///     ---
        ///     AI switch
        ///     You can turn off the AI, for example, so that the character performs story actions without being distracted by the environment and enemies,
        ///     then turn it on so that the character starts to act according to the situation.
        /// </summary>
        public bool IsEnabledAI
        {
            get { return this.isEnabledAI; }
            set
            {
                if(value == isEnabledAI)
                    return;
                isEnabledAI = value;
                ChangeEnabledAI?.Invoke();
            }
        }

        /// <summary>
        ///     Текущее состояние персонажа (паттерн поведения).
        ///     На основании состояния будет разрабатываться подходящая стратегия поведения по обстановке
        ///     ---
        ///     /// The current state of the character (behavior pattern).
        /// Based on the state, a suitable behavior strategy will be developed according to the situation
        /// </summary>
        public CharacterStateType State
        {
            get { return this.state; }
            set
            {
                if(value == state)
                    return;
                state = value;
                ChangeState?.Invoke();
            }
        }

        /// <summary>
        ///     Персонаж мёртв?
        ///     Если персонаж умер, он не может ничего рассчитывать, и не может влиять на окружающих персонажей
        ///     ---
        ///     Is the character dead?
        ///     If a character is dead, he can't count on anything, and he can't influence the characters around him
        /// </summary>
        public bool IsDead
        {
            get { return this.isDead; }
            set
            {
                if(value == isDead)
                    return;
                isDead = value;
                ChangeDead?.Invoke();
            }
        }
        
    }

}
