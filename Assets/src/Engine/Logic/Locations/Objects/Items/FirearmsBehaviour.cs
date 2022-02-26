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
    public class FirearmsBehaviour : MonoBehaviour, IFirearmsBehaviour
    {

        [SerializeField] private Transform shotPoint;

        /// <summary>
        ///     Точка откуда происходит появление снаряда
        ///     ---
        ///     The point where the projectile comes from
        /// </summary>
        public Vector3 ShotPosition { get { return shotPoint.position; } }

    }

}
