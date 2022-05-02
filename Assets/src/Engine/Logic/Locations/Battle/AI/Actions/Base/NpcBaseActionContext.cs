using System;
using System.Collections.Generic;
using Engine.Data;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Базовый контекст действия ИИ существа.
    /// Если тип действия перемещения - контекст будет содержать информацию необходимую длля совершения перемещения,
    /// если тип действия атака - в контексте будет информация, необходимая для совершения атаки,
    /// и т.д.
    /// ---
    /// The basic context of the creature's AI action.
    /// If the action type is move - the context will contain the information needed to perform the move,
    /// If the action type is attack - the context will contain information necessary to perform an attack,
    /// etc.
    /// 
    /// </summary>
    [Serializable]
    public abstract class NpcBaseActionContext
    {

        /// <summary>
        /// Тип текущего действия
        /// ---
        /// Type of current action
        /// </summary>
        [SerializeField] public NpcActionType Action;

    }

}
