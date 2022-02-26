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

        public void Drop(Vector3 worldPosition, IItem item)
        {
            if (item == null)
                return;

            Drop(worldPosition, ItemSerializator.Convert(item));
        }

        public void Drop(Vector3 worldPosition, IEnumerable<IItem> items)
        {
            if (items == null)
                return;

            Drop(worldPosition, items.ToArray());
        }

        public void Drop(Vector3 worldPosition, params IItem[] items)
        {
            if (items == null || items.Length == 0)
                return;

            foreach(var item in items)
                Drop(worldPosition, item);
        }

        public void Drop(Vector3 worldPosition, ItemInfo itemInfo)
        {
            if (itemInfo == null)
                return;

            // Получаем предмет, чтобы вытащить его префаб
            IItem item = ItemFactory.Instance.Get(itemInfo.ID);
            // Выкидываем предмет на карту
            var dropped = GameObject.Instantiate<GameObject>(item.Prefab);
            // Достаём информацию о выкинутом предмете, и инициализируем предмет
            var droppedBehaviour = dropped.GetComponent<LocationDroppedItemBehaviour>();
            droppedBehaviour.Init(itemInfo, worldPosition);
        }

        public void Drop(Vector3 worldPosition, IEnumerable<ItemInfo> itemsInfo)
        {
            if (itemsInfo == null)
                return;

            Drop(worldPosition, itemsInfo.ToArray());
        }

        public void Drop(Vector3 worldPosition, params ItemInfo[] itemsInfo)
        {
            if (itemsInfo == null || itemsInfo.Length == 0)
                return;

            foreach (var itemInfo in itemsInfo)
                Drop(worldPosition, itemInfo);
        }

    }

}
