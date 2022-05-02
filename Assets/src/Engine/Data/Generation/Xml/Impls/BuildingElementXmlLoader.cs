using Engine.Data.Generation.Elements;
using Engine.Logic.Locations.Generator;
using UnityEngine;

namespace Engine.Data.Generation.Xml.Impls
{

    /// <summary>
    /// 
    /// Реализация загрузчика для элементов генерации через XML
    /// ---
    /// Implementation of the loader for generating elements via XML
    /// 
    /// </summary>
    public class BuildingElementXmlLoader : XmlLoaderBase<BuildingElement, RoomType>
    {
        protected override string[] FileNames => new[] { "Data/build_data" };

        protected override BuildingElement ReadElement()
        {
            var element = new BuildingElement();

            element.ID   = Lng("ID");
            element.Type = Enm<RoomType>("Type");

            element.Floor                  = Resources.Load<GameObject>(Str("Floor"));
            element.Door                   = Resources.Load<GameObject>(Str("Door"));

            element.InsideWall             = Resources.Load<GameObject>(Str("InsideWall"));
            element.OutsideWall            = Resources.Load<GameObject>(Str("OutsideWall"));
            element.InsideWallWithDoor     = Resources.Load<GameObject>(Str("InsideWallWithDoor"));
            element.OutsideWallWithDoor    = Resources.Load<GameObject>(Str("OutsideWallWithDoor"));
            element.InsideWallWithWindow   = Resources.Load<GameObject>(Str("InsideWallWithWindow"));
            element.OutsideWallWithWindow  = Resources.Load<GameObject>(Str("OutsideWallWithWindow"));

            return element;
        }


        

    }

}
