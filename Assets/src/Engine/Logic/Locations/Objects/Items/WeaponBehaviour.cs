using UnityEngine;

namespace Engine.Logic.Locations.Objects
{

    /// <summary>
    /// 
    /// Скрипт поведения оружия.
    /// Необходим для рассчётов смещения оружия в руках игрока.
    /// ---
    /// Weapon behavior script.
    /// Necessary for calculating the displacement of the weapon in the player's hands.
    /// 
    /// </summary>
    public class WeaponBehaviour : MonoBehaviour, IWeaponBehaviour
    {

        #region Hidden Fields

        /// <summary>
        ///     Смещение положения оружия в руке персонажа
        ///     ---
        ///     Shifting the position of the weapon in the character's hand
        /// </summary>
        [SerializeField] private Vector3 positionOffset;

        /// <summary>
        ///     Смещение вращения оружия в руке персонажа
        ///     ---
        ///     Shifting the rotation of the weapon in the character's hand
        /// </summary>
        [SerializeField] private Vector3 rotationOffset;

        #endregion

        #region Properties

        /// <summary>
        ///     Смещение позиции оружие в правой руке
        ///     ---
        ///     Offset the position of the weapon in the right hand
        /// </summary>
        public Vector3 PositionOffset => positionOffset;

        /// <summary>
        ///     Смещение поворота оружия в правой руке
        ///     ---
        ///     Offset in the rotation of the weapon in the right hand
        /// </summary>
        public Vector3 RotationOffset => rotationOffset;

        #endregion

    }

}
