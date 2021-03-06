using System;
using System.Linq;

namespace Engine.Data
{

    /// <summary>
    /// 
    /// Холодное оружие
    /// ---
    /// Cold Weapons
    /// 
    /// </summary>
    [Serializable]
    public class EdgedWeapon : Weapon, IEdgedWeapon
    {

        /// <summary>
        /// Можно метать
        /// </summary>
        public bool CanThrow { get; set; }

        /// <summary>
        /// Сколько стоит ОД для метания
        /// </summary>
        public int ThrowAP { get; set; }

        /// <summary>
        /// Урон от метания
        /// </summary>
        public long ThrowDamage { get; set; }

        /// <summary>
        /// Дистанция метания
        /// </summary>
        public float ThrowDistance { get; set; }

        /// <summary>
        ///     Дистанция прицеливания при метании ножа
        ///     ---
        ///     Aiming distance when throwing a edged
        /// </summary>
        public float ThrowAimRadius { get; set; }

        /// <summary>
        /// Как выглядит снаряд при метании
        /// </summary>
        public string ThrowEffectType { get; set; }

        /// <summary>
        /// Звук метания
        /// </summary>
        public string ThrowSoundType { get; set; }

        /// <summary>
        /// Звук попадания
        /// </summary>
        public string ThrowInSoundType { get; set; }

        /// <summary>
        /// Звук промаха
        /// </summary>
        public string ThrowOutSoundType { get; set; }

        /// <summary>
        ///     Копирует текущую сущность в новый экземпляр
        ///     ---
        ///     
        /// </summary>
        /// <returns>
        ///     Копия сущности
        ///     ---
        ///     
        /// </returns>
        public override IIdentity Copy()
        {
            return new EdgedWeapon()
            {
                ID = ID,
                ToolType = ToolType,
                WeaponType = WeaponType,
                Type = Type,
                Name = Name,
                Description = Description,
                Count = Count,
                StackSize = StackSize,
                Weight = Weight,
                Parts = Parts?.ToList(),
                Level = Level,
                Author = Author,

                MaxDistance = MaxDistance,
                AimRadius = AimRadius,
                Damage = Damage,
                UseAP = UseAP,

                CanThrow = CanThrow,
                ThrowAP = ThrowAP,
                ThrowDamage = ThrowDamage,
                ThrowDistance = ThrowDistance,
                ThrowAimRadius = ThrowAimRadius,
                ThrowEffectType = ThrowEffectType,

                ThrowSoundType = ThrowSoundType,
                ThrowInSoundType = ThrowInSoundType,
                ThrowOutSoundType = ThrowOutSoundType,
            };
        }
    }

}
