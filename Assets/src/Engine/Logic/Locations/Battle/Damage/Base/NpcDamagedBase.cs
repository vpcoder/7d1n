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
    [RequireComponent(typeof(CharacterNpcBehaviour))]
    public abstract class NpcDamagedBase : DamagedBase
    {

        [SerializeField] private CharacterNpcBehaviour characterLink;
        
        #region Properties

        /// <summary>
        ///     Ссылка на текущего NPC
        ///     ---
        ///     Link to current NPC
        /// </summary>
        protected CharacterNpcBehaviour CurrentNPC => characterLink;
        
        #endregion

        #region Methods

        /// <summary>
        ///     Вызывается когда цель получает урон
        ///     ---
        ///     Called when the target takes damage
        /// </summary>
        public override void TakeDamage()
        {
            if(!CanTakeDamage)
                return;
            
            if(CurrentNPC.Animator != null)
                CurrentNPC.Animator.SetCharacterDamageType(TakeDamageType.Take1);
            
            if (Health <= 0)
                CurrentNPC.Died();
        }

        #endregion

    }

}
