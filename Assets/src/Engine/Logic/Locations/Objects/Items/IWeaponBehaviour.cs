using UnityEngine;

namespace Engine.Logic.Locations.Objects
{

    /// <summary>
    /// 
    /// Скрипт поведения оружия на локации
    /// ---
    /// Weapon behavior script on location
    /// 
    /// </summary>
    public interface IWeaponBehaviour
    {

        /// <summary>
        ///     Смещение позиции оружие в правой руке
        ///     ---
        ///     Offset the position of the weapon in the right hand
        /// </summary>
        Vector3 PositionOffset { get; }

        /// <summary>
        ///     Смещение поворота оружия в правой руке
        ///     ---
        ///     Offset in the rotation of the weapon in the right hand
        /// </summary>
        Vector3 RotationOffset { get; }

    }

}
