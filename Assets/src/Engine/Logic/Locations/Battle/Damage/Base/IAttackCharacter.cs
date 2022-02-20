using Engine.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Engine.Logic
{

    /// <summary>
    /// 
    /// Интерфейс существа или объекта, выполняющего атаку
    /// ---
    /// 
    /// 
    /// </summary>
    public interface IAttackCharacter
    {

        NavMeshAgent Agent { get; }

        IWeapon Weapon { get; }

        Vector3 TargetAttackPos { get; }

        AudioSource AttackAudioSource { get; }

        EnemyBody EnemyBody { get; }

        GameObject AttackCharacterObject { get; }

        GameObject WeaponObject { get; }

        void AddBattleExp(long value);

    }

}
