using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement
{
    
    /// <summary>
    ///
    /// Процессор размещения конкретного объекта мебели
    /// ---
    /// Processor for placing a specific piece of furniture
    /// 
    /// </summary>
    /// <typeparam name="E">
    ///     Группа мебели, которая характерна для данного типа помещения
    ///     ---
    ///     A group of furniture, which is typical for this type of room
    /// </typeparam>
    public abstract class ItemPutBaseProcessor<E> : IItemPutProcessor<E> where E : struct
    {

        #region Hidden Fields
        
        /// <summary>
        ///     Кеш для BuildParent
        ///     ---
        ///     Cache for BuildParent
        /// </summary>
        private Transform buildParent;
        
        #endregion
        
        #region Properties

        /// <summary>
        ///     Дочерний Transform в котором будут располагаться сгенерированные объекты помещения (стены, пол, окна, двери и прочее)
        ///     ---
        ///     The child Transform in which the generated room objects will be located (walls, floor, windows, doors, etc.)
        /// </summary>
        private Transform BuildParent
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
        
        /// <summary>
        ///     Ссылка на родительский процессор расстановки, из которого был запущен этот экземпляр расстановки конкретного объекта мебели
        ///     ---
        ///     Reference to the parent arrangement processor from which this instance of the arrangement of a particular piece of furniture was started
        /// </summary>
        public IArrangementProcessor<E> Parent { get; set; }
        
        /// <summary>
        ///     Тип размещаемого объекта мебели
        ///     ---
        ///     Type of furniture to be placed
        /// </summary>
        public abstract E Type { get; }
        
        #endregion

        #region Shared Methods
        
        /// <summary>
        ///     Выполняет постановку/добавления объекта в сцену
        ///     ---
        ///     Performs staging/adds an object to the scene
        /// </summary>
        /// <param name="context">
        ///     Контекст генерируемого помещения
        ///     ---
        ///     Context of the generated room
        /// </param>
        /// <param name="currentInsertItem">
        ///     Добавляемый объект
        ///     ---
        ///     Addable object
        /// </param>
        /// <returns>
        ///     true - если удалось расположить объект в сцене,
        ///     false - если объект не удалось расположить в сцене
        ///     ---
        ///     true - if the object was successfully placed in the scene,
        ///     false - if the object could not be placed in the scene
        /// </returns>
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
        /// <param name="fillFullTile">
        ///     Занять все сегменты тайла, или только те что в TileSegmentLink
        ///     если true - заполняются все сегменты тайла (помечаются как занятые)
        ///     если false - заполняется один сегмент, указанный в TileSegmentLink
        ///     ---
        ///     occupy all segments of the tile, or only those in TileSegmentLink
        ///     if true - all segments of the tile are filled (marked as occupied)
        ///     if false - only one segment specified in TileSegmentLink is occupied
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
        protected bool TryPutOnRandomSegment(GenerationRoomContext context, IList<TileSegmentLink> list, EdgeLayout layout, IEnvironmentItem<E> item, bool fillFullTile = false)
        {
            if (Lists.IsEmpty(list))
                return false;
            
            var index = context.RoomRandom.Next(0, list.Count - 1);
            var randomLink = list[index];
            
            var position = GetPositionBySegmentLink(randomLink, layout);
            var rotation = GetRotationBySegmentLink(randomLink);
            return TryPutOnGlobalPos(item, randomLink, position, rotation, fillFullTile);
        }

        protected bool TryPutOnGlobalPos(IEnvironmentItem<E> item, TileSegmentLink link, Vector3 globalPosition, Quaternion globalRotation, bool fillFullTile = false)
        {
            if (fillFullTile)
            {
                link.Tile.Set(link.Layout, TileSegmentType.S00, item);
                link.Tile.Set(link.Layout, TileSegmentType.S01, item);
                link.Tile.Set(link.Layout, TileSegmentType.S10, item);
                link.Tile.Set(link.Layout, TileSegmentType.S11, item);
            }
            else
            {
                link.Tile.Set(link.Layout, link.SegmentType, item);
            }
            
            var unityGameObject = Object.Instantiate
            (
                item.ToObject, 
                globalPosition,
                globalRotation,
                BuildParent
            );
            
            Parent.ArrangementContext.Items.AddInToList(item.Type, new ArrangementItemContext<E>()
            {
                Context  = link,
                Item     = item,
                ToObject = unityGameObject,
            });

            return true;
        }

        /// <summary>
        ///     Получает вращение относительно грани на которой расположен сегмент
        ///     ---
        ///     Gets quaternion relative to the face on which the segment is located
        /// </summary>
        /// <param name="link">
        ///     Информация о расположении сегмента
        ///     ---
        ///     Segment location information
        /// </param>
        /// <returns>
        ///     Вращение для объекта, располагаемого относительно грани на которой расположен сегмент
        ///     ---
        ///     Quaternion for an object located relative to the face on which the segment is located
        /// </returns>
        protected Quaternion GetRotationBySegmentLink(TileSegmentLink link)
        {
            return link.Marker.GetLayoutRot(link.EdgeLayout);
        }
        
        /// <summary>
        ///     Получает глобальное положение центра сегмента на слое layout.
        ///     У тайла 6 слоёв, поэтому слой layout так же учитывается.
        ///     ---
        ///     Gets the global position of the center of the segment on the layout layer.
        ///     The tile has 6 layers, so the layout layer is also taken into account.
        /// </summary>
        /// <param name="link">
        ///     Информация о расположении сегмента
        ///     ---
        ///     Segment location information
        /// </param>
        /// <param name="layout">
        ///     Слой на который нужно размещать мебель (пол, стена, потолок)
        ///     ---
        ///     Layer on which to place the furniture (floor, wall, ceiling)
        /// </param>
        /// <returns>
        ///     Глобальное положение центра сегмента, расположенного на слое layout
        ///     ---
        ///     Global position of the center of the segment located on the layout layer
        /// </returns>
        protected Vector3 GetPositionBySegmentLink(TileSegmentLink link, EdgeLayout layout)
        {
            return link.Marker.GetSegmentPos(layout, link.SegmentType);
        }
        
        /// <summary>
        ///     Выполняет размещение объекта item на поверхности объекта surfaceObject.
        ///     Таким образом можно разместить тарелку или микроволновку на поверхности стола.
        ///     ---
        ///     Places the item object on the surface of the objectInToScene.
        ///     So you can place a plate or a microwave on the surface of the table.
        /// </summary>
        /// <param name="movedObject">
        ///     Созданный GameObject мебели в сцене, который мы хотим разместить
        ///     ---
        ///     The created GameObject furniture in the scene that we want to place
        /// </param>
        /// <param name="surfaceObject">
        ///     Созданный GameObject в сцене, на поверхность которого будем размещать объект item
        ///     ---
        ///     The created GameObject in the scene, on the surface of which we will place the item object
        /// </param>
        /// <param name="alighmentType">
        ///     Способ размещения объекта на поверхности другого объекта
        ///     ---
        ///     The way of placing an object on the surface of another object
        /// </param>
        /// <returns>
        ///     true - если объект удалось успешно разместить на поверхности
        ///     false - если не удалось рассчитать коллидеры объектов, чтобы понять как их ставить друг на друга
        ///     ---
        ///     true - if the object was successfully placed on the surface
        ///     false - if it was not possible to calculate the colliders of the objects to understand how to put them on top of each other
        /// </returns>
        protected Vector3 GetPositionOnSurface(GameObject movedObject, GameObject surfaceObject, SurfacePositionAlighmentType alighmentType)
        {
            var moved   = movedObject.GetComponent<SurfaceLocalAnchorBehaviour>();
            var surface = surfaceObject.GetComponent<SurfaceLocalAnchorBehaviour>();
            
            if (moved == null || surface == null)
                return Vector3.zero;

            switch (alighmentType)
            {
                case SurfacePositionAlighmentType.CenterCenter:
                    return surface.TopSurfaceCenterPos + new Vector3(0f, moved.Bounds.extents.y, 0f);
                default:
                    throw new NotSupportedException("alighment " + alighmentType + " isn't supported!");
            }
        }
        
        #endregion

    }
}