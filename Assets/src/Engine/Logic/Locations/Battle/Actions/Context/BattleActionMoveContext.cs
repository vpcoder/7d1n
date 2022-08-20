using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations.Battle.Actions
{

    /// <summary>
    ///
    /// Контекст для выполнения действия перемещения
    /// ---
    /// Context for the move action
    /// 
    /// </summary>
    public class BattleActionMoveContext : BattleActionContext
    {
        
        /// <summary>
        ///     Точки пути по которым персонаж должен двигаться
        ///     ---
        ///     Waypoints on which the character should move
        /// </summary>
        public List<Vector3> Points { get; set; }
    }

}
