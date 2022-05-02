using UnityEngine;

namespace Engine.Logic.Locations.Objects
{

    /// <summary>
    /// 
    /// Оружие дальнего боя
    /// ---
    /// Ranged Weapon
    /// 
    /// </summary>
    public interface IFirearmsBehaviour
    {

        /// <summary>
        ///     Точка откуда происходит появление снаряда
        ///     ---
        ///     The point where the projectile comes from
        /// </summary>
        Vector3 ShotPosition { get; }

    }

}
