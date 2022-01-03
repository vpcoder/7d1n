using Engine.Data;
using UnityEngine;

namespace Engine.Logic.Locations
{

    public class BattleActionAttackContext
    {
        public HandActionType Action { get; set; }
        public IDamagedObject Target { get; set; }
        public IWeapon Weapon { get; set; }
        public GameObject AttackMarker { get; set; }
    }

}
