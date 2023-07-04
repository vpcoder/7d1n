using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations.Floor
{
 
    /// <summary>
    ///
    /// Данные с информацией об этаже
    /// ---
    /// Data with floor information
    /// 
    /// </summary>
    [Serializable]
    public class FloorData
    {

        /// <summary>
        ///     Этаж, который описывается этим блоком
        ///     ---
        ///     The floor that is described by this block
        /// </summary>
        [SerializeField]
        public int FloorIndex;
        
        /// <summary>
        ///     Список объектов, которые формируют этаж (крыши, полы и т.д.)
        ///     ---
        ///     List of objects that form the floor (roofs, floors, etc.)
        /// </summary>
        [SerializeField]
        public List<GameObject> ObjectList;

    }
    
}