using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Базовый контекст NPC
    /// ---
    /// Basic NPC Context
    /// 
    /// </summary>
    [Serializable]
    public class NpcContext
    {

        /// <summary>
        ///     Текущее состояние существа
        ///     ---
        ///     Current state of the creature
        /// </summary>
        [SerializeField]
        public NpcStatus Status = new NpcStatus();

        /// <summary>
        ///     Список действий, которые NPC планирует совершить в текущем ходу
        ///     ---
        ///     A list of actions the NPC plans to take in the current turn
        /// </summary>
        [SerializeField]
        public List<NpcBaseActionContext> Actions = new List<NpcBaseActionContext>();

    }

}
