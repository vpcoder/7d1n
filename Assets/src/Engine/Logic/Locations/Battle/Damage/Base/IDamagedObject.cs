using System;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Объект, который может получать урон
    /// ---
    /// An object that can take damage
    /// 
    /// </summary>
    public interface IDamagedObject
    {

        #region Properties

        /// <summary>
        ///     Здоровье/Состояние цели
        ///     ---
        ///     Health/target state
        /// </summary>
        int Health { get; set; }

        /// <summary>
        ///     Защита цели
        ///     ---
        ///     Protecting the target
        /// </summary>
        int Protection { get; }

        /// <summary>
        ///     Кем то уже получен опыт за него?
        ///     ---
        ///     Is someone already experienced for him?
        /// </summary>
        bool ExpGeted { get; set; }

        /// <summary>
        ///     Опыт, который выдаётся за уничтожение этого существа или объекта
        ///     ---
        ///     The experience given for destroying that creature or object
        /// </summary>
        long Exp { get; }

        /// <summary>
        ///     Источник воспроизведения звуков, связанный с этой целью
        ///     ---
        ///     The source of sound reproduction associated with this target
        /// </summary>
        AudioSource DamageAudioSource { get; }

        /// <summary>
        ///     Ссылка на этот объект
        ///     ---
        ///     Link to this object
        /// </summary>
        GameObject ToObject { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     Вызывается когда цель получает урон
        ///     ---
        ///     Called when the target takes damage
        /// </summary>
        void TakeDamage();

        #endregion

    }

}
