using Engine.Data;
using Engine.Data.Factories;
using Engine.Logic.Locations.Objects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Сервис, позволяющий выполнять выбрасывание предметов на локацию
    /// ---
    /// A service that allows you to throw items on a location
    /// 
    /// </summary>
    public class ItemsDropController : MonoBehaviour
    {

        public void Drop(Vector3 worldPosition, bool dropWithRandomPos, IItem item)
        {
            if (item == null)
                return;

            Drop(worldPosition, dropWithRandomPos, ItemSerializator.Convert(item));
        }

        public void Drop(Vector3 worldPosition, bool dropWithRandomPos, IEnumerable<IItem> items)
        {
            if (items == null)
                return;

            Drop(worldPosition, dropWithRandomPos, items.ToArray());
        }

        public void Drop(Vector3 worldPosition, bool dropWithRandomPos, params IItem[] items)
        {
            if (Lists.IsEmpty(items))
                return;

            foreach(var item in items)
                Drop(worldPosition, dropWithRandomPos, item);
        }

        public void Drop(Vector3 worldPosition, bool dropWithRandomPos, ItemInfo itemInfo)
        {
            // К нам могли прийти ошибочные данные, запрещаем NPE и выкидывание системных объектов на локацию
            // We may have received erroneous data, prohibit NPE and throwing system objects on the location
            if (itemInfo == null || DataDictionary.Items.IsSystemItem(itemInfo.ID))
                return;

            // Получаем предмет, чтобы вытащить его префаб
            // Getting an item to take out its prefab
            IItem item = ItemFactory.Instance.Get(itemInfo.ID);
            // Выкидываем предмет на карту
            // Throw the item on the map
            var dropped = GameObject.Instantiate(item.Prefab);
            // Достаём информацию о выкинутом предмете, и инициализируем предмет
            // Retrieve information about the discarded item, and initialize the item
            var droppedBehaviour = dropped.GetComponent<LocationDroppedItemBehaviour>();
            droppedBehaviour.Init(itemInfo, worldPosition, dropWithRandomPos);
        }

        public void Drop(Vector3 worldPosition, bool dropWithRandomPos, IEnumerable<ItemInfo> itemsInfo)
        {
            if (itemsInfo == null)
                return;

            Drop(worldPosition, dropWithRandomPos, itemsInfo.ToArray());
        }

        public void Drop(Vector3 worldPosition, bool dropWithRandomPos, params ItemInfo[] itemsInfo)
        {
            if (Lists.IsEmpty(itemsInfo))
                return;

            foreach (var itemInfo in itemsInfo)
                Drop(worldPosition, dropWithRandomPos, itemInfo);
        }

    }

}
