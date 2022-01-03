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

        GameObject ToObject { get; }

        void AddBattleExp(long value);

    }

}
