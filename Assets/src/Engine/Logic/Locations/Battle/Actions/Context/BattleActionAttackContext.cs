using Engine.Data;
using UnityEngine;

namespace Engine.Logic.Locations.Battle.Actions
{

    public class BattleActionAttackContext : BattleActionContext
    {
        public HandActionType Action { get; set; }
        public IDamagedObject Target { get; set; }
        public IWeapon Weapon { get; set; }
        public GameObject AttackMarker { get; set; }
        public Vector3 WeaponPointPos { get; set; }
    }

}
