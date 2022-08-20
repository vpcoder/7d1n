using Engine.Logic.Locations.Generator.Environment.Building.Xml;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building.Impls
{

    /// <summary>
    /// 
    /// Реализация загрузчика для объектов в комнате
    /// ---
    /// Implementation of the loader for objects in a room
    /// 
    /// </summary>
    public abstract class RoomObjectsXmlLoader<E> : XmlLoaderBase<E> where E : struct
    {

        protected override IEnvironmentItem<E> ReadEnvironmentItem()
        {
            var item = new EnvironmentItem<E>();
            item.ToObject = Resources.Load<GameObject>(Str("Path"));
            item.Type = Enm<E>("Type");
            return item;
        }

    }

}
