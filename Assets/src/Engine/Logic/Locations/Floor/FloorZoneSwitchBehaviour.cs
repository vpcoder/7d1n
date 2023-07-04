using System;
using Engine.Logic.Locations;
using UnityEditor.U2D;
using UnityEngine;

namespace src.Engine.Logic.Locations.Floor
{
    
    /// <summary>
    /// 
    /// Зона-переключатель.
    /// Если персонаж находится в нескольких зонах одновременно, применяется зона с наименьшим значением этажа.
    /// ---
    /// Zone Switch.
    /// If a character is in more than one zone at the same time, the zone with the lowest floor value is applied.
    /// 
    /// </summary>
    [RequireComponent(typeof(BoxCollider))]
    public class FloorZoneSwitchBehaviour : MonoBehaviour
    {

        [SerializeField] private int enterFloorIndex = 1;

        /// <summary>
        ///     Этаж, на который необходимо переключиться
        ///     ---
        ///     Floor to be switched to
        /// </summary>
        public int EnterFloorIndex
        {
            get { return enterFloorIndex; }
            set { enterFloorIndex = value; }
        }
        
    }
    
}