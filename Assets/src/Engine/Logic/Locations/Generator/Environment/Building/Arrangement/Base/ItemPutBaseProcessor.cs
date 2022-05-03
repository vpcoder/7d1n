using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement
{
    public abstract class ItemPutBaseProcessor<E> : IItemPutProcessor<E> where E : struct
    {

        public IArrangementProcessor<E> Parent { get; set; }
        
        public abstract E Type { get; }

        public abstract bool TryPutItem(GenerationRoomContext context, IEnvironmentItem<E> currentInsertItem);
            
        /// <summary>
        ///     Ставит мебель на слой в случайный пустой сегмент из списка сегментов
        ///     ---
        ///     Places furniture on a layer in a random empty segment from the list of segments
        /// </summary>
        /// <param name="context">
        ///     Контекст операции
        ///     ---
        ///     Context of the operation
        /// </param>
        /// <param name="list">
        ///     Список сегментов, в один из которых нужно будет положить объект
        ///     ---
        ///     List of segments, one of which will need to put the object
        /// </param>
        /// <param name="layout">
        ///     Слой на который нужно размещать мебель (пол, стена, потолок)
        ///     ---
        ///     Layer on which to place the furniture (floor, wall, ceiling)
        /// </param>
        /// <param name="item">
        ///     Объект мебели, который мы хотим разместить
        ///     ---
        ///     The piece of furniture we want to place
        /// </param>
        /// <typeparam name="E">
        ///     Тип комнаты, под которым работаем со списком доступной мебели
        ///     ---
        ///     The type of room under which we work with the list of available furniture
        /// </typeparam>
        /// <returns>
        ///     true - если объект удалось успешно разместить в сцене
        ///     false - если не удалось найти подходящего свободного места, и объект не был размещён
        ///     ---
        ///     true - if the object was successfully placed in the scene
        ///     false - if no suitable free space was found and the object was not placed
        /// </returns>
        protected bool TryPutOnRandomSegment(GenerationRoomContext context, IList<TileSegmentLink> list, EdgeLayout layout, IEnvironmentItem<E> item)
        {
            if (Lists.IsEmpty(list))
                return false;
            
            var index = context.RoomRandom.Next(0, list.Count - 1);
            var randomItem = list[index];
            
            randomItem.Tile.Set(randomItem.Layout, randomItem.SegmentType, item);

            var unityGameObject = Object.Instantiate
            (
                item.ToObject, 
                randomItem.Marker.GetSegmentPos(layout, randomItem.SegmentType),
                randomItem.Marker.GetLayoutRot(randomItem.EdgeLayout),
                BuildParent
            );
            
            Parent.ArrangementContext.Items.AddInToList(item.Type, new ArrangementItemContext<E>()
            {
                Context = randomItem,
                Item = item,
                ToObject = unityGameObject,
            });
            
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