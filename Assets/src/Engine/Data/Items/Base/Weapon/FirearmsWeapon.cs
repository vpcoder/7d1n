using System;
using System.Linq;

namespace Engine.Data
{

    /// <summary>
    /// 
    /// Огнестрельное оружие, дистанционное оружие, луки, арбалеты и т.д.
    /// ---
    /// Firearms, remote weapons, bows, crossbows, etc.
    /// 
    /// </summary>
    [Serializable]
    public class FirearmsWeapon : Weapon, IFirearmsWeapon
    {

        /// <summary>
        ///     Размер магазина оружия (максимальное число патронов в обойме)
        ///     ---
        ///     The size of the gun magazine (maximum number of rounds in a clip)
        /// </summary>
        public long AmmoStackSize { get; set; }

        /// <summary>
        ///     Текущее количество патронов в магазине
        ///     ---
        ///     Current number of cartridges in the magazine
        /// </summary>
        public long AmmoCount { get; set; }

        /// <summary>
        ///     Тип патронов, подходящих для этого оружия
        ///     ---
        ///     The type of ammunition suitable for this weapon
        /// </summary>
        public long AmmoID { get; set; }

        /// <summary>
        ///     Как выглядит снаряд при выстреле
        ///     ---
        ///     What the projectile looks like when fired
        /// </summary>
        public string AmmoEffectType { get; set; }

        /// <summary>
        ///     Эффект огня из ствола
        ///     ---
        ///     The effect of a gunshot
        /// </summary>
        public string ShootEffectType { get; set; }

        /// <summary>
        ///     Звук выстрела
        ///     ---
        ///     The sound of a gunshot
        /// </summary>
        public string ShootSoundType { get; set; }

        /// <summary>
        ///     Цена перезарядки ОД
        ///     ---
        ///     The cost of reloading AP
        /// </summary>
        public int ReloadAP { get; set; }

        /// <summary>
        ///     Звук перезарядки
        ///     ---
        ///     Reload sound
        /// </summary>
        public string ReloadSoundType { get; set; }

        /// <summary>
        ///     Звук клина оружия
        ///     ---
        ///     Sound of jamming weapons
        /// </summary>
        public string JammingSoundType { get; set; }

        /// <summary>
        ///     Коэффициент пробития объектов (от 0 до 100)
        ///     ---
        ///     Object penetration rate (0 to 100)
        /// </summary>
        public byte Penetration { get; set; }

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
            return new FirearmsWeapon()
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

                Damage = Damage,
                UseAP = UseAP,
                MaxDistance = MaxDistance,
                AimRadius = AimRadius,

                AmmoID = AmmoID,
                AmmoStackSize = AmmoStackSize,
                AmmoCount = AmmoCount,
                ReloadAP = ReloadAP,
                Penetration = Penetration,

                AmmoEffectType = AmmoEffectType,
                ShootEffectType = ShootEffectType,

                ShootSoundType = ShootSoundType,
                ReloadSoundType = ReloadSoundType,
                JammingSoundType = JammingSoundType,
            };
        }

    }

}
