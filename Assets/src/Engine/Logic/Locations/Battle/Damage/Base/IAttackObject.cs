using Engine.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Engine.Logic
{

    /// <summary>
    /// 
    /// Интерфейс существа или объекта, выполняющего атаку
    /// ---
    /// The interface of the creature or object performing the attack
    /// 
    /// </summary>
    public interface IAttackObject
    {

        #region Properties

        /// <summary>
        ///     Агент выполняющий навигацию для этого атакующего
        ///     ---
        ///     The agent performing navigation for this attacker
        /// </summary>
        NavMeshAgent Agent { get; }

        /// <summary>
        ///     Оружие которое находится в руках у этого атакующего
        ///     ---
        ///     The weapon in the hands of this attacker
        /// </summary>
        IWeapon Weapon { get; }

        /// <summary>
        ///     Целевая точка, куда этот атакующий совершает атаку
        ///     ---
        ///     The target point where this attacker makes the attack
        /// </summary>
        Vector3 TargetAttackPos { get; }

        /// <summary>
        ///     Источник звука этого атакующего
        ///     ---
        ///     The source of the sound of this attacker
        /// </summary>
        AudioSource AttackAudioSource { get; }

        /// <summary>
        ///     Объект тела этого атакующего
        ///     ---
        ///     The object of this attacker's body
        /// </summary>
        EnemyBody EnemyBody { get; }

        /// <summary>
        ///     Ссылка на объект атакующего
        ///     ---
        ///     Reference to the attacker's object
        /// </summary>
        GameObject AttackCharacterObject { get; }

        /// <summary>
        ///     Объект оружия, который атакующий сейчас держит в руке
        ///     ---
        ///     The object of the weapon that the attacker now holds in his hand
        /// </summary>
        GameObject WeaponObject { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     Выполняет передачу опыта этому объекту
        ///     ---
        ///     Performs the transfer of experience to this object
        /// </summary>
        /// <param name="value">
        ///     Количество опыта, который получил объект
        ///     ---
        ///     The amount of experience the object has received
        /// </param>
        void AddBattleExp(long value);

        #endregion

    }

}
