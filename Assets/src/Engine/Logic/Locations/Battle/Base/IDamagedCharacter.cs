using System;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Объект, который может получать урон
    /// </summary>
    public interface IDamagedObject
    {

        /// <summary>
        /// Здоровье цели
        /// </summary>
        int Health { get; set; }

        /// <summary>
        /// Защита цели
        /// </summary>
        int Protection { get; set; }

        /// <summary>
        /// Кем то уже полечен опыт за него?
        /// </summary>
        bool ExpGeted { get; set; }

        /// <summary>
        /// Опыт, который выдаётся за убийство этого существа
        /// </summary>
        long Exp { get; }

        AudioSource DamageAudioSource { get; }

        GameObject ToObject { get; }

    }

}
