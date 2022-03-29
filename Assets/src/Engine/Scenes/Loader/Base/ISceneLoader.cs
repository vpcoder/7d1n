namespace Engine.Scenes.Loader
{
    
    /// <summary>
    ///
    /// Загрузчик, выполняющий специфическую логику загрузки для конкретной сцены
    /// ---
    /// A loader that performs scene-specific loading logic
    /// 
    /// </summary>
    public interface ISceneLoader
    {
        
        #region Properties
        
        /// <summary>
        ///     Название сцены, для которой будет выполнена специфическая логика загрузки
        ///     ---
        ///     Name of the scene for which the specific loading logic will be executed
        /// </summary>
        SceneName Scene { get; }

        #endregion
        
        #region Methods
        
        /// <summary>
        ///     Выполняется в самую первую очередь, до инициализации канваса сразу на старте сцены
        ///     ---
        ///     Is performed in the first place, before the initialization of the canvas at the start of the scene
        /// </summary>
        /// <param name="context">
        ///     Контекст загрузки.
        ///     Содержит в себе всю необходимую информацию для работы всех загрузчиков
        ///     ---
        ///     Boot Context.
        ///     Contains all the necessary information for the operation of all loaders
        /// </param>
        void PreLoad(LoadContext context);
        
        /// <summary>
        ///     Выполняется в обычный для загрузки момент времени
        ///     ---
        ///     Executed at the usual time for loading
        /// </summary>
        /// <param name="context">
        ///     Контекст загрузки.
        ///     Содержит в себе всю необходимую информацию для работы всех загрузчиков
        ///     ---
        ///     Boot Context.
        ///     Contains all the necessary information for the operation of all loaders
        /// </param>
        void Load(LoadContext context);

        /// <summary>
        ///     Выполняется в самом конце, когда уже всё загружено
        ///     ---
        ///     Executed at the very end, when everything is already loaded
        /// </summary>
        /// <param name="context">
        ///     Контекст загрузки.
        ///     Содержит в себе всю необходимую информацию для работы всех загрузчиков
        ///     ---
        ///     Boot Context.
        ///     Contains all the necessary information for the operation of all loaders
        /// </param>
        void PostLoad(LoadContext context);
        
        #endregion

    }
    
}
