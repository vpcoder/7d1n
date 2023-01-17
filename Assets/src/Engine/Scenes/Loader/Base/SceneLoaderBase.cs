using Engine.Data;
using UnityEngine;

namespace Engine.Scenes.Loader
{
    
    /// <summary>
    ///
    /// Загрузчик, выполняющий специфическую логику загрузки для конкретной сцены
    /// ---
    /// A loader that performs scene-specific loading logic
    /// 
    /// </summary>
    public abstract class SceneLoaderBase : ISceneLoader
    {

        #region Abstract Methods

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
        protected virtual void OnPreLoad(LoadContext context) { /* empty */ }
        
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
        protected virtual void OnLoad(LoadContext context) { /* empty */ }
        
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
        protected virtual void OnPostLoad(LoadContext context) { /* empty */ }

        #endregion
        
        #region Proxy Methods
        
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
        public void PreLoad(LoadContext context)
        {
            Debug.Log("preload scene '" + Game.Instance.Runtime.Scene + "'...");
            OnPreLoad(context);
        }

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
        public void Load(LoadContext context)
        {
            Debug.Log("load scene '" + Game.Instance.Runtime.Scene + "'...");
            OnLoad(context);
        }

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
        public void PostLoad(LoadContext context)
        {
            Debug.Log("postload scene '" + Game.Instance.Runtime.Scene + "'...");
            OnPostLoad(context);
        }
        
        #endregion
        
    }
    
}
