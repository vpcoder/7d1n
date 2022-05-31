using System.Collections.Generic;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement
{


    /// <summary>
    /// 
    /// Процессор, выполняющий расстановку доступных предметов в комнате
    /// ---
    /// Processor that arranges available items in the room
    ///     
    /// </summary>
    public interface IArrangementProcessor
    {

        /// <summary>
        ///     Тип комнаты, для которой происходит расстановка
        ///     ---
        ///     The type of room for which the arrangement is made
        /// </summary>
        RoomKindType RoomType { get; }

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
        void ArrangementProcess(GenerationRoomContext context, ICollection<IEnvironmentItem> availableItems);

    }

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
    public interface IArrangementProcessor<E> : IArrangementProcessor
                                      where E : struct
    {
        
        /// <summary>
        ///     Контекст расстановки.
        ///     Объекты, которые мы уже расставили в помещении
        ///     ---
        ///     Context of the placement.
        ///     Objects that we have already set up in the room
        /// </summary>
        ArrangementContext<E> ArrangementContext { get; }
        
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
        void ArrangementProcess(GenerationRoomContext context, ICollection<IEnvironmentItem<E>> availableItems);

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
        bool InsertItemIntoScene(GenerationRoomContext context, IEnvironmentItem<E> currentInsertItem);

    }

}
