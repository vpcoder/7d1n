using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations.Battle.Actions
{

    public class BattleActionMoveContext : BattleActionContext
    {
        public List<Vector3> Points { get; set; }
    }

}
