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
        ///     Радиус нанесения урона
        ///     В указанной сфере от оружия будет рассчитываться достало ли оно по цели или нет
        ///     ---
        ///     The damage radius
        ///     In the specified area from the weapon will be calculated whether it has reached the target or not
        /// </summary>
        public float DamageRadius { get; set; }
        
        /// <summary>
        ///     Можно метать во врага
        ///     ---
        ///     You can throw it at the character
        /// </summary>
        public bool CanThrow { get; set; }

        /// <summary>
        ///     Сколько стоит ОД для метания
        ///     ---
        ///     How much is the AP for throwing
        /// </summary>
        public int ThrowAP { get; set; }

        /// <summary>
        ///     Наносимый урон от метания
        ///     ---
        ///     Damage inflicted by throwing
        /// </summary>
        public int ThrowDamage { get; set; }

        /// <summary>
        ///     Дистанция метания в игровых метрах
        ///     ---
        ///     Throwing distance in game meters
        /// </summary>
        public float ThrowDistance { get; set; }

        /// <summary>
        ///     Дистанция прицеливания при метании ножа
        ///     ---
        ///     Aiming distance when throwing a edged
        /// </summary>
        public float ThrowAimRadius { get; set; }

        /// <summary>
        ///     Как выглядит снаряд при метании
        ///     ---
        ///     What the projectile looks like when thrown
        /// </summary>
        public string ThrowBulletObject { get; set; }

        /// <summary>
        ///     Звук метания
        ///     ---
        ///     Throwing sound
        /// </summary>
        public string ThrowSound { get; set; }

        /// <summary>
        ///     Звук попадания в тело
        ///     ---
        ///     The sound of hitting the body
        /// </summary>
        public string ThrowHitSound { get; set; }

        /// <summary>
        ///     Звук промаха, попадания в стену или объект
        ///     ---
        ///     Sound of missing, hitting a wall or object
        /// </summary>
        public string ThrowMissSound { get; set; }

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
                ToolType = ToolType?.ToSet(),
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

                MaxDistance = MaxDistance,
                DamageRadius = DamageRadius,
                AimRadius = AimRadius,
                Damage = Damage,
                UseAP = UseAP,

                CanThrow = CanThrow,
                ThrowAP = ThrowAP,
                ThrowDamage = ThrowDamage,
                ThrowDistance = ThrowDistance,
                ThrowAimRadius = ThrowAimRadius,
                ThrowBulletObject = ThrowBulletObject,

                ThrowSound = ThrowSound,
                ThrowHitSound = ThrowHitSound,
                ThrowMissSound = ThrowMissSound,
            };
        }
    }

}
