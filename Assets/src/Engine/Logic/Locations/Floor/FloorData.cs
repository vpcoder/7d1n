using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations.Floor
{
    
    [Serializable]
    public class FloorData
    {

        [SerializeField]
        [Header("Floor | Этаж")]
        public int FloorIndex;
        
        [SerializeField]
        public List<GameObject> ObjectList;

    }
    
}