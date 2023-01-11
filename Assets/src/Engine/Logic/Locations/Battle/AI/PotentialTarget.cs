using System.Collections.Generic;
using Engine.Data;
using UnityEngine;

namespace Engine.Logic.Locations
{
    
    public class PotentialTarget
    {

        public EnemyNpcBehaviour Target { get; set; }
        
        public List<Vector3> Path { get; set; }

        public float PathLength { get; set; }

        public IFirearmsWeapon FirearmsWeapon { get; set; }

        public IWeapon PriorityWeapon { get; set; }

        public IDamagedObject DamagedObject => Target == null ? null : Target.transform.GetComponent<IDamagedObject>();

    }
    
}