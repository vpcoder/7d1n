using Engine.Logic.Locations.Generator;
using System;
using UnityEngine;

namespace Engine.Data.Generation.Elements
{

    /// <summary>
    /// 
    /// Элемент генерации стиля для помещений
    /// ---
    /// Style generation element for rooms
    /// 
    /// </summary>
    [Serializable]
    public class BuildingElement : ElementBase<RoomType>
    {

        public GameObject Floor { get; set; }
        public GameObject InsideWall { get; set; }
        public GameObject OutsideWall { get; set; }
        public GameObject InsideWallWithDoor { get; set; }
        public GameObject OutsideWallWithDoor { get; set; }
        public GameObject InsideWallWithWindow { get; set; }
        public GameObject OutsideWallWithWindow { get; set; }
        public GameObject Door { get; set; }

    }

}
