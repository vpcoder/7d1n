using System;
using System.Linq;

namespace Engine.Data
{

    /// <summary>
    /// Огнестрельное оружие
    /// </summary>
    [Serializable]
    public class GrenadeWeapon : Weapon, IGrenadeWeapon
    {

        /// <summary>
        /// Радиус взрыва
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        /// Эффект метания гранаты
        /// </summary>
        public string GrenadeEffectType { get; set; }

        /// <summary>
        /// Звук метания гранаты
        /// </summary>
        public string ThrowSoundType { get; set; }

        /// <summary>
        /// Эффект взрыва гранаты
        /// </summary>
        public string ExplodeEffectType {get; set; }

        /// <summary>
        /// Звук взрыва
        /// </summary>
        public string ExplodeSoundType { get; set; }

        /// <summary>
        ///     Копирует текущую сущность в новый экземпляр
        ///     ---
        ///     Copies the current entity into a new instance
        /// </summary>
        /// <returns>
        ///     Копия сущности
        ///     ---
        ///     Entity Copy
        /// </returns>
        public override IIdentity Copy()
        {
            return new GrenadeWeapon()
            {
                ID = ID,
                ToolType = ToolType,
                WeaponType = WeaponType,
                Type = Type,
                Name = Name,
                Description = Description,
                Count = Count,
                StackSize = StackSize,
                StaticWeight = StaticWeight,
                Weight = Weight,
                Parts = Parts?.ToList(),
                Level = Level,
                Author = Author,

                Damage = Damage,
                UseAP = UseAP,
                MaxDistance = MaxDistance,
                AimRadius = AimRadius,

                Radius = Radius,
                GrenadeEffectType = GrenadeEffectType,
                ExplodeEffectType = ExplodeEffectType,

                ThrowSoundType = ThrowSoundType,
                ExplodeSoundType = ExplodeSoundType,
            };
        }

    }

}
