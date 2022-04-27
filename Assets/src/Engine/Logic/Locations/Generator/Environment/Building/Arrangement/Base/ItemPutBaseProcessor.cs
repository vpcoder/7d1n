using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement
{
    public abstract class ItemPutBaseProcessor<E> : IItemPutProcessor<E> where E : struct
    {

        public abstract E Type { get; }

        public abstract bool TryPutItem(GenerationRoomContext context, IEnvironmentItem<E> currentInsertItem);
            
        /// <summary>
        ///     Ставит мебель на слой в случайный пустой сегмент из списка сегментов
        ///     ---
        ///     
        /// </summary>
        /// <param name="context">
        ///     Контекст операции
        ///     ---
        ///     
        /// </param>
        /// <param name="list">
        ///     Список сегментов, в один из которых нужно будет положить объект
        ///     ---
        ///     
        /// </param>
        /// <param name="layout">
        ///     Слой на который нужно размещать мебель (пол, стена, потолок)
        ///     ---
        ///     
        /// </param>
        /// <param name="item">
        ///     Объект мебели, который мы хотим разместить
        ///     ---
        ///     
        /// </param>
        /// <typeparam name="E">
        ///     Тип комнаты, под которым работаем со списком доступной мебели
        ///     ---
        ///     
        /// </typeparam>
        /// <returns>
        ///     true - если объект удалось успешно разместить в сцене
        ///     false - если не удалось найти подходящего свободного места, и объект не был размещён
        ///     ---
        ///     
        /// </returns>
        protected bool TryPutOnRandomSegment(GenerationRoomContext context, IList<TileSegmentLink> list, EdgeLayout layout, IEnvironmentItem<E> item)
        {
            if (list.Count == 0)
                return false;
            
            var index = context.RoomRandom.Next(0, list.Count - 1);
            var randomItem = list[index];
            randomItem.Tile.Set(randomItem.Layout, randomItem.SegmentType, item);
            
            // FIXME: Для отладки
            randomItem.Marker.Segments[randomItem.SegmentType] = Color.yellow;
            foreach (var test in list)
                test.Marker.Segments[test.SegmentType] = Color.green;

            Object.Instantiate
            (
                item.ToObject, 
                randomItem.Marker.GetSegmentPos(layout, randomItem.SegmentType),
                randomItem.Marker.GetLayoutRot(randomItem.EdgeLayout),
                BuildParent
            );
            
            return true;
        }
        
        /// <summary>
        ///     Кеш для BuildParent
        ///     ---
        ///     Cache for BuildParent
        /// </summary>
        private Transform buildParent;
        
        /// <summary>
        ///     Дочерний Transform в котором будут располагаться сгенерированные объекты помещения (стены, пол, окна, двери и прочее)
        ///     ---
        ///     The child Transform in which the generated room objects will be located (walls, floor, windows, doors, etc.)
        /// </summary>
        protected Transform BuildParent
        {
            get
            {
                if(buildParent != null)
                {
                    return buildParent;
                }
                var parent = GameObject.Find("BuildData");
                if(parent == null)
                {
                    parent = new GameObject("BuildData");
                }
                buildParent = parent.transform;
                return buildParent;
            }
        }
        
    }
}