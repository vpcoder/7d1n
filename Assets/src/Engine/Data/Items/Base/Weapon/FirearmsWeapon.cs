using System;
using System.Linq;

namespace Engine.Data
{

    /// <summary>
    /// Огнестрельное оружие
    /// </summary>
    [Serializable]
    public class FirearmsWeapon : Weapon, IFirearmsWeapon
    {

        /// <summary>
        /// Размер магазина
        /// </summary>
        public long AmmoStackSize { get; set; }

        /// <summary>
        /// Текущее количество патронов в магазине
        /// </summary>
        public long AmmoCount { get; set; }

        /// <summary>
        /// Тип патронов
        /// </summary>
        public long AmmoID { get; set; }

        /// <summary>
        /// Как выглядит снаряд при выстреле
        /// </summary>
        public string AmmoEffectType { get; set; }

        /// <summary>
        /// Эффект огня из ствола
        /// </summary>
        public string ShootEffectType { get; set; }

        /// <summary>
        /// Звук выстрела
        /// </summary>
        public string ShootSoundType { get; set; }

        /// <summary>
        /// Цена перезарядки
        /// </summary>
        public int ReloadAP { get; set; }

        /// <summary>
        /// Звук перезарядки
        /// </summary>
        public string ReloadSoundType { get; set; }

        /// <summary>
        /// Звук клина оружия
        /// </summary>
        public string JammingSoundType { get; set; }

        /// <summary>
        /// Коэффициент пробития (от 0 до 100)
        /// </summary>
        public byte Penetration { get; set; }

        /// <summary>
        /// Копирует текущую сущность в новый экземпляр
        /// </summary>
        /// <returns>Копия сущности</returns>
        public override IIdentity Copy()
        {
            return new FirearmsWeapon()
            {
                ID = ID,
                ToolType = ToolType,
                Type = Type,
                Name = Name,
                Description = Description,
                Count = Count,
                StackSize = StackSize,
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
