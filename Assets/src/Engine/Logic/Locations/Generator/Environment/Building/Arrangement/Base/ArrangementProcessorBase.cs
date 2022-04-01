using System;
using System.Collections.Generic;
using System.Linq;
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
        
        /// <summary>
        ///     Контекст расстановки.
        ///     Объекты, которые мы уже расставили в помещении
        ///     ---
        ///     Context of the placement.
        ///     Objects that we have already set up in the room
        /// </summary>
        protected ArrangementContext<E> arrangementContext = new ArrangementContext<E>();

        #endregion
        
        #region Properties
        
        /// <summary>
        ///     Тип комнаты, для которой происходит расстановка
        ///     ---
        ///     The type of room for which the arrangement is made
        /// </summary>
        public abstract RoomKindType RoomType { get; }

        #endregion
        
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
            if (availableItems == null || availableItems.Count == 0)
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
            if (availableItems == null || availableItems.Count == 0)
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
        public abstract bool InsertItemIntoScene(GenerationRoomContext context, IEnvironmentItem<E> currentInsertItem);

        #endregion
        
    }

}
