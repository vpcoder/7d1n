using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Data;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement
{

    /// <summary>
    /// 
    /// Процессор, выполняющий расстановку доступных предметов в комнате
    /// ---
    /// Processor that arranges available items in the room
    ///     
    /// </summary>
    /// <typeparam name="E">
    ///     Енум доступных предметов в комнате
    ///     ---
    ///     Enum of available items in the room
    /// </typeparam>
    public abstract class ArrangementProcessorBase<E> : IArrangementProcessor<E>
                                              where E : struct
    {

        #region Hidden Fields
        
        private readonly IDictionary<E, IItemPutProcessor<E>> putProcessorsData;

        /// <summary>
        ///     Контекст расстановки.
        ///     Объекты, которые мы уже расставили в помещении
        ///     ---
        ///     Context of the placement.
        ///     Objects that we have already set up in the room
        /// </summary>
        private readonly ArrangementContext<E> arrangementContext = new ArrangementContext<E>();

        #endregion
        
        #region Properties

        /// <summary>
        ///     Контекст расстановки.
        ///     Объекты, которые мы уже расставили в помещении
        ///     ---
        ///     Context of the placement.
        ///     Objects that we have already set up in the room
        /// </summary>
        public ArrangementContext<E> ArrangementContext => arrangementContext;

        /// <summary>
        ///     Тип комнаты, для которой происходит расстановка
        ///     ---
        ///     The type of room for which the arrangement is made
        /// </summary>
        public abstract RoomKindType RoomType { get; }

        #endregion
        
        #region Ctor

        protected ArrangementProcessorBase()
        {
            putProcessorsData = new Dictionary<E, IItemPutProcessor<E>>();
            foreach (var processor in CreateProcessors())
            {
                processor.Parent = this;
                putProcessorsData.Add(processor.Type, processor);
            }
        }
        
        #endregion

        private ICollection<IItemPutProcessor<E>> CreateProcessors()
        {
            return AssembliesHandler.CreateImplementations<IItemPutProcessor<E>>();
        }
        
        #region Shared Methods
        
        /// <summary>
        ///     Процесс расстановки.
        ///     Выполняет расстановку и развешивание availableItems объектов в помещении (контекст генерации contex)
        ///     ---
        ///     Arrangement process.
        ///     Performs the arrangement and hanging of availableItems objects in the room (contex generation context)
        /// </summary>
        /// <param name="context">
        ///     Контекст генерируемого помещения
        ///     ---
        ///     Context of the generated room
        /// </param>
        /// <param name="availableItems">
        ///     Список доступных для расстановки предметов
        ///     ---
        ///     The list of available items for arranging
        /// </param>
        public void ArrangementProcess(GenerationRoomContext context, ICollection<IEnvironmentItem<E>> availableItems)
        {
            if (Lists.IsEmpty(availableItems))
                return; // Ничего не делаем, если нечего расставлять
            
            var remaingItems = availableItems.ToList();
            this.arrangementContext.AvailableItems = availableItems.ToList();
            this.arrangementContext.RemainingItems = remaingItems;

            for (; ; )
            {
                if (remaingItems.Count == 0)
                    break;

                IEnvironmentItem<E> item = remaingItems[0];

                try
                {
                    InsertItemIntoScene(context, item);
                } catch (Exception ex)
                {
                    Debug.LogException(ex);
                }

                remaingItems.RemoveAt(0);
            }

            this.arrangementContext.RemainingItems.Clear();
            this.arrangementContext.RemainingItems = null;
            this.arrangementContext.AvailableItems.Clear();
            this.arrangementContext.AvailableItems = null;
            this.arrangementContext.Items.Clear();

            FillEnemyInfoByEmptySegments(context);
        }

        private void FillEnemyInfoByEmptySegments(GenerationRoomContext context)
        {
            var enemyInfo = Game.Instance.Runtime.GenerationInfo.EnemyInfo;
            foreach (var emptySegmentLink in TileService.GetFurnitureOnTheLayoutByTiles(TileLayoutType.Floor, context.TilesInfo.TilesData, TileService.EmptyEnvironmentItemFilter))
            {
                enemyInfo.EnemyStartPoints.Add(new EnemyPointInfo()
                {
                    Position = emptySegmentLink.Marker.GetSegmentPos(EdgeLayout.Floor, emptySegmentLink.SegmentType),
                });
            }
        }

        /// <summary>
        ///     Процесс расстановки.
        ///     Выполняет расстановку и развешивание availableItems объектов в помещении (контекст генерации contex)
        ///     ---
        ///     Arrangement process.
        ///     Performs the arrangement and hanging of availableItems objects in the room (contex generation context)
        /// </summary>
        /// <param name="context">
        ///     Контекст генерируемого помещения
        ///     ---
        ///     Context of the generated room
        /// </param>
        /// <param name="availableItems">
        ///     Список доступных для расстановки предметов
        ///     ---
        ///     The list of available items for arranging
        /// </param>
        public void ArrangementProcess(GenerationRoomContext context, ICollection<IEnvironmentItem> availableItems)
        {
            if (Lists.IsEmpty(availableItems))
                return;
            ArrangementProcess(context, availableItems.Select(item => (IEnvironmentItem<E>)item).ToList());
        }

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
        public virtual bool InsertItemIntoScene(GenerationRoomContext context, IEnvironmentItem<E> currentInsertItem)
        {
            // Добавление мебели через процессоры
            putProcessorsData.TryGetValue(currentInsertItem.Type, out var processor);
            return processor?.TryPutItem(context, currentInsertItem) ?? false;
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
        
        #endregion
        
    }

}
