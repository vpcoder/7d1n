using System;
using System.Collections.Generic;
using Engine.Scenes;
using Engine.Scenes.Loader;
using UnityEngine;

namespace src.Engine.Scenes.Loader
{
    
    /// <summary>
    /// 
    ///     Фабрика загрузчиков сцен.
    ///     Загрузчики выполняют определённую логику загрузки для каждой из сцен
    ///     ---
    ///     Scene Loader Factory.
    ///     Loaders perform certain loading logic for each scene
    /// 
    /// </summary>
    public class LoadFactory
    {
        
        #region Singleton
        
        private static Lazy<LoadFactory> instance = new Lazy<LoadFactory>(() => new LoadFactory());

        public static LoadFactory Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private LoadFactory()
        {
            foreach(var loader in AssembliesHandler.CreateImplementations<ISceneLoader>())
                loaders.Add(loader.Scene, loader);
        }

        
        #endregion

        #region Hidden Fields
        
        /// <summary>
        ///     Словарь загрузчиков.
        ///     Имя сцены -> Загрузчик
        ///     ---
        ///     Loader Dictionary.
        ///     Scene Name -> Loader
        /// </summary>
        private IDictionary<SceneName, ISceneLoader> loaders = new Dictionary<SceneName, ISceneLoader>();

        #endregion
        
        #region Shared Methods
        
        /// <summary>
        ///     Выполняет поиск загрузчика по названию сцены.
        ///     ---
        ///     It searches for the loader by scene name.
        /// </summary>
        /// <param name="scene">
        ///     Название сцены, для которой необходимо выполнить специфическую загрузку
        ///     ---
        ///     Name of the scene for which you want to perform a specific load
        /// </param>
        /// <returns>
        ///     Если по сцене не нашлось загрузчика, вернёт null
        ///     ---
        ///     If no loader is found for the scene, it returns null
        /// </returns>
        public ISceneLoader Get(SceneName scene)
        {
            loaders.TryGetValue(scene, out var loader);
            return loader;
        }

        #endregion
        
    }
    
}