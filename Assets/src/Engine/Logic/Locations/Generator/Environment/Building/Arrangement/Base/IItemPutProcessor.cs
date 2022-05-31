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
    public interface IItemPutProcessor<E> where E : struct
    {

        #region Properties
        
        /// <summary>
        ///     Ссылка на родительский процессор расстановки, из которого был запущен этот экземпляр расстановки конкретного объекта мебели
        ///     ---
        ///     Reference to the parent arrangement processor from which this instance of the arrangement of a particular piece of furniture was started
        /// </summary>
        IArrangementProcessor<E> Parent { get; set; }
        
        /// <summary>
        ///     Тип размещаемого объекта мебели
        ///     ---
        ///     Type of furniture to be placed
        /// </summary>
        E Type { get; }
        
        #endregion
        
        #region Methods

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
        bool TryPutItem(GenerationRoomContext context, IEnvironmentItem<E> currentInsertItem);

        #endregion
        
    }
    
}