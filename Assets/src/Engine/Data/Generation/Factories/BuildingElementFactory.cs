using Engine.Data.Generation.Xml;
using Engine.Logic.Locations.Generator;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data.Generation
{


    [Serializable]
    public class BuildingInfo : IElementIdentity
    {

        public string ID { get; set; }

        public RoomType RoomType { get; set; }


        public GameObject Floor { get; set; }

        public GameObject InsideWall { get; set; }

        public GameObject OutsideWall { get; set; }

        public GameObject InsideWallWithDoor { get; set; }

        public GameObject OutsideWallWithDoor { get; set; }

        public GameObject InsideWallWithWindow { get; set; }

        public GameObject OutsideWallWithWindow { get; set; }

    }

    public class BuildingElementFactory : XmlLoaderBase<BuildingInfo>
    {

        #region Singleton

        private static readonly Lazy<BuildingElementFactory> instance = new Lazy<BuildingElementFactory>(() => new BuildingElementFactory());
        public static BuildingElementFactory Instance { get { return instance.Value; } }

        private BuildingElementFactory()
        {
            FileNames = new[] {
                "Data/build_data",
            };
        }

        #endregion

        private IDictionary<RoomType, BuildingInfo> elementsCache = new Dictionary<RoomType, BuildingInfo>();

        protected override BuildingInfo ReadElement()
        {
            var element = new BuildingInfo();
            element.ID = Str("ID");
            element.RoomType = Enm<RoomType>("RoomType");
            element.InsideWall            = Resources.Load<GameObject>(Str("InsideWall"));
            element.Floor                 = Resources.Load<GameObject>(Str("Floor"));
            element.OutsideWall           = Resources.Load<GameObject>(Str("OutsideWall"));
            element.InsideWallWithDoor    = Resources.Load<GameObject>(Str("InsideWallWithDoor"));
            element.OutsideWallWithDoor   = Resources.Load<GameObject>(Str("OutsideWallWithDoor"));
            element.InsideWallWithWindow  = Resources.Load<GameObject>(Str("InsideWallWithWindow"));
            element.OutsideWallWithWindow = Resources.Load<GameObject>(Str("OutsideWallWithWindow"));
            return element;
        }

    }

}
