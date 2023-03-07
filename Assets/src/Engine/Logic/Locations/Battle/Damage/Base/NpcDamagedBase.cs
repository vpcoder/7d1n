using Engine.Logic.Locations.Animation;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Базовый NPC который может получать урон
    /// ---
    /// A basic NPC who can take damage
    /// 
    /// </summary>
    [RequireComponent(typeof(EnemyNpcBehaviour))]
    public abstract class NpcDamagedBase : DamagedBase
    {

        [SerializeField] private EnemyNpcBehaviour characterLink;
        
        #region Properties

        /// <summary>
        ///     Ссылка на текущего NPC
        ///     ---
        ///     Link to current NPC
        /// </summary>
        protected EnemyNpcBehaviour CurrentNPC => characterLink;

        #endregion

        #region Methods

        /// <summary>
        ///     Вызывается когда цель получает урон
        ///     ---
        ///     Called when the target takes damage
        /// </summary>
        public override void TakeDamage()
        {
            CurrentNPC.Animator.SetInteger(AnimationKey.DamageKey, 1);
            if (Health <= 0)
                CurrentNPC.Died();
        }

        #endregion

    }

}
