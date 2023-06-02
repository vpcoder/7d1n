namespace Engine.Story
{
    
    public interface IStoryActive
    {
        
        /// <summary>
        ///     Управление активностью истории.
        ///     Если true - это означает что история "работает" и может быть запущена в любой момент.
        ///     Если false - это означает что история недоступна. История не сможет запуститься,
        ///     даже если игрок будет пытаться с ней взаимодействовать.
        ///     ---
        ///     Control the activity of the history.
        ///     If true - it means that the history is "working" and can be started at any time.
        ///     If false - it means that the history is unavailable. The history will not be able to start,
        ///     even if the player will try to interact with it.
        /// </summary>
        bool IsActive { get; set; }
        
        /// <summary>
        ///     Управление способом запуска истории
        ///     Если true - это означает что историю надо пытаться запускать сразу на старте сцены
        ///     Если false - это означает что историю запустят вручную (например нажатием на историю, или из кода)
        ///     ---
        ///     Control how to start the story
        ///     If true - it means that the history will be tried immediately at the scene start
        ///     If false - it means that the story will be run manually (for example, by pressing on the story, or from the code)
        /// </summary>
        bool IsNeedRunOnStart { get; set; }

    }
    
}