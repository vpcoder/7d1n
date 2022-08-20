using Engine.Data;
using UnityEngine;

namespace Engine.Logic.Locations.Battle.Actions
{

    /// <summary>
    ///
    /// Контекст для выполнения действия атаки
    /// ---
    /// Context for performing an attack action
    /// 
    /// </summary>
    public class BattleActionAttackContext : BattleActionContext
    {
        /// <summary>
        ///     Совершаемое действие атаки
        ///     ---
        ///     Attack action performed
        /// </summary>
        public HandActionType Action { get; set; }
        
        /// <summary>
        ///     Оружие, которым совершается атака
        ///     ---
        ///     Weapon used in the attack
        /// </summary>
        public IWeapon Weapon { get; set; }
        
        /// <summary>
        ///     Маркер атаки - направление по которму совершается атака
        ///     ---
        ///     Attack marker - the direction of the attack
        /// </summary>
        public GameObject AttackMarker { get; set; }
        
        /// <summary>
        ///     Точка в которой расположено оружие, совершающее атаку
        ///     ---
        ///     The point at which the weapon that performs the attack is located
        /// </summary>
        public Vector3 WeaponPointPos { get; set; }
        
    }

}
