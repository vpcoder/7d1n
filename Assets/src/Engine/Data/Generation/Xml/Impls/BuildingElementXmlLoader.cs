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
        public override string[] FileNames => new[] { "Data/build_data" };

        public override string RootDirectory => "Locations/Builds/";

        protected override BuildingElement ReadElement()
        {
            var element = new BuildingElement();

            element.ID   = Lng("ID");
            element.Type = Enm<RoomType>("Type");

            element.Floor                  = Resources.Load<GameObject>(GetResourcePath(element, "Floor"));
            element.InsideWall             = Resources.Load<GameObject>(GetResourcePath(element, "InsideWall"));
            element.OutsideWall            = Resources.Load<GameObject>(GetResourcePath(element, "OutsideWall"));
            element.InsideWallWithDoor     = Resources.Load<GameObject>(GetResourcePath(element, "InsideWallWithDoor"));
            element.OutsideWallWithDoor    = Resources.Load<GameObject>(GetResourcePath(element, "OutsideWallWithDoor"));
            element.InsideWallWithWindow   = Resources.Load<GameObject>(GetResourcePath(element, "InsideWallWithWindow"));
            element.OutsideWallWithWindow  = Resources.Load<GameObject>(GetResourcePath(element, "OutsideWallWithWindow"));
            element.Window                 = Resources.Load<GameObject>(GetResourcePath(element, "Window"));
            element.Door                   = Resources.Load<GameObject>(GetResourcePath(element, "Door"));

            return element;
        }


        

    }

}
