using Engine.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Engine.Logic
{

    public interface IAttackCharacter
    {

        NavMeshAgent Agent { get; }

        IWeapon Weapon { get; }

        Vector3 TargetAttackPos { get; }

        AudioSource AttackAudioSource { get; }

        EnemyBody EnemyBody { get; }

        GameObject ToObject { get; }

        GameObject WeaponGameObject { get; }

        void AddBattleExp(long value);

    }

}
