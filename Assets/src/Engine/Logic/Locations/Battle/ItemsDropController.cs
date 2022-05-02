using Engine.Data;
using Engine.Data.Factories;
using Engine.Logic.Locations.Objects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Сервис, позволяющий выполнять выбрасывание предметов на локацию
    /// </summary>
    public class ItemsDropController : MonoBehaviour
    {

        public void Drop(Vector3 worldPosition, bool dropWithRandomPos, IItem item)
        {
            if (item == null)
                return;

            Drop(worldPosition, dropWithRandomPos, ItemSerializator.Convert(item));
        }

        public void Drop(Vector3 worldPosition, bool dropWithRandomPos, IEnumerable < IItem> items)
        {
            if (items == null)
                return;

            Drop(worldPosition, dropWithRandomPos, items.ToArray());
        }

        public void Drop(Vector3 worldPosition, bool dropWithRandomPos, params IItem[] items)
        {
            if (items == null || items.Length == 0)
                return;

            foreach(var item in items)
                Drop(worldPosition, dropWithRandomPos, item);
        }

        public void Drop(Vector3 worldPosition, bool dropWithRandomPos, ItemInfo itemInfo)
        {
            if (itemInfo == null)
                return;

            // Получаем предмет, чтобы вытащить его префаб
            IItem item = ItemFactory.Instance.Get(itemInfo.ID);
            // Выкидываем предмет на карту
            var dropped = GameObject.Instantiate<GameObject>(item.Prefab);
            // Достаём информацию о выкинутом предмете, и инициализируем предмет
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
            if (itemsInfo == null || itemsInfo.Length == 0)
                return;

            foreach (var itemInfo in itemsInfo)
                Drop(worldPosition, dropWithRandomPos, itemInfo);
        }

    }

}
