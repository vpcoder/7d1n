using System.Collections.Generic;
using Engine.Data;
using UnityEngine;

namespace Engine.Logic.Locations
{
    
    /// <summary>
    ///
    /// Предиктор, который будет формировать список действий для NPC в зависимости от его состояния
    /// ---
    /// Predictor that will generate a list of actions for an NPC depending on its state
    /// 
    /// </summary>
    public abstract class AbstractPredictorBase : IPredictor
    {

        /// <summary>
        ///     Состояние, для которого подходит указанный тип предиктора
        ///     ---
        ///     The state for which the specified type of predictor is appropriate
        /// </summary>
        public abstract NpcStateType State { get; }

        /// <summary>
        ///     Метод должен сформировать список действий для NPC
        ///     ---
        ///     The method should generate a list of actions for NPCs
        /// </summary>
        /// <param name="context">
        ///     Контекст предиктора, в котором находятся все необходимые для анализа действий NPC данные
        ///     ---
        ///     Predictor context, which contains all the data needed to analyze NPC actions
        /// </param>
        public void CreateStrategyForNpc(PredictorContext context)
        {
            // TODO: ...
            CreateStrategyForNpcInner(context);
        }

        /// <summary>
        ///     Метод должен сформировать список действий для NPC
        ///     ---
        ///     The method should generate a list of actions for NPCs
        /// </summary>
        /// <param name="context">
        ///     Контекст предиктора, в котором находятся все необходимые для анализа действий NPC данные
        ///     ---
        ///     Predictor context, which contains all the data needed to analyze NPC actions
        /// </param>
        public abstract void CreateStrategyForNpcInner(PredictorContext context);

        
        #region Protected Handlers
        
        protected NpcBaseActionContext CreateWait(float delay)
        {
            return new NpcWaitActionContext()
            {
                Action = NpcActionType.Wait,
                WaitDelay = delay,
            };
        }

        protected NpcBaseActionContext CreateMove(EnemyNpcBehaviour enemy, List<Vector3> path, float speed)
        {
            return new NpcMoveActionContext()
            {
                Action = NpcActionType.Move,
                Path = path,
                Speed = speed,
            };
        }

        protected NpcBaseActionContext CreatePickWeapon(EnemyNpcBehaviour enemy, IWeapon weapon)
        {
            return new NpcPickWeaponActionContext()
            {
                Action = NpcActionType.PickWeapon,
                Weapon = weapon,
            };
        }

        protected NpcBaseActionContext CreateAttack(EnemyNpcBehaviour enemy, IWeapon weapon)
        {
            return new NpcAttackActionContext()
            {
                Action = NpcActionType.Attack,
                Weapon = weapon,
            };
        }

        protected NpcBaseActionContext CreateReload(EnemyNpcBehaviour enemy, IFirearmsWeapon weapon, IItem ammo)
        {
            return new NpcReloadActionContext()
            {
                Action = NpcActionType.Reload,
                FirearmsWeapon = weapon,
                Ammo = ammo,
            };
        }

        protected NpcBaseActionContext CreateLook(IDamagedObject target, float speed)
        {
            return new NpcLookActionContext()
            {
                Action = NpcActionType.Look,
                Speed = speed,
                LookPoint = target.ToObject.transform.position,
            };
        }
        
        #endregion
        
    }
    
}