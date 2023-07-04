using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Scenes;
using Engine.Scenes.Loader;
using src.Engine.Scenes.Loader.Impls;
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
            foreach (var loader in AssembliesHandler.CreateImplementations<ISceneLoader>())
                loaders.Add(loader.GetType(), loader);
            
            var tmp = new Dictionary<SceneName, List<Type>>();
            InitLoadLists(tmp);
            
            foreach (var entry in tmp)
            {
                var sceneName = entry.Key;
                var listLoaders = entry.Value.Select(type => loaders[type]).ToList();
                sceneLoaders.Add(sceneName, listLoaders);
            }
        }

        private void InitLoadLists(IDictionary<SceneName, List<Type>> tmp)
        {
            tmp.AddInToList(SceneName.Menu,
                typeof(MenuSceneLoader)
            );
            tmp.AddInToList(SceneName.Map,
                typeof(MapSceneLoader)
            );
            tmp.AddInToList(SceneName.Build,
                typeof(SceneGuiLoader)
            );
            
            tmp.AddInToList(SceneName.Chagegrad1,
                typeof(SceneGuiLoader),
                typeof(ChagegradSceneLoader)
            );
        }
        
        private List<Type> InitDefaultList()
        {
            return new List<Type>()
            {
                typeof(SceneGuiLoader)
            };
        } 

        #endregion
        
        public event Action Complete;
        
        #region Hidden Fields

        /// <summary>
        ///     Все известные загрузчики
        ///     ---
        ///     All known loaders
        /// </summary>
        private IDictionary<Type, ISceneLoader> loaders = new Dictionary<Type, ISceneLoader>();
        private IList<ISceneLoader> defaultLoaders;

        /// <summary>
        ///     Словарь загрузчиков.
        ///     Имя сцены -> Загрузчики
        ///     ---
        ///     Loader Dictionary.
        ///     Scene Name -> Loaders
        /// </summary>
        private IDictionary<SceneName, IList<ISceneLoader>> sceneLoaders = new Dictionary<SceneName, IList<ISceneLoader>>();

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
        ///     Если по сцене не нашлось загрузчика, вернёт дефолтный список загрузчиков
        ///     ---
        ///     If no loader is found for the scene, it will return the default list of loaders
        /// </returns>
        public ICollection<ISceneLoader> Get(SceneName scene)
        {
            sceneLoaders.TryGetValue(scene, out var currentLoaders);
            if (currentLoaders == null)
                return CreateDefaultSceneLoaders();
            return currentLoaders;
        }

        private ICollection<ISceneLoader> CreateDefaultSceneLoaders()
        {
            List<ISceneLoader> list = new List<ISceneLoader>();
            foreach (var type in InitDefaultList())
            {
                if(type == null)
                    continue;
                var loader = loaders[type];
                if(loader == null)
                    continue;
                list.Add(loader);
            }
            return list;
        }

        public void Load(SceneName scene, LoadContext context)
        {
            foreach (var loader in Get(scene))
                loader.Load(context);
        }
        
        public void PreLoad(SceneName scene, LoadContext context)
        {
            foreach (var loader in Get(scene))
                loader.PreLoad(context);
        }
        
        public void PostLoad(SceneName scene, LoadContext context)
        {
            foreach (var loader in Get(scene))
                loader.PostLoad(context);
        }

        public void DoComplete()
        {
            if(Complete == null)
                return;
            
            Complete.Invoke();
            
            foreach (var link in Complete.GetInvocationList())
                Complete -= (Action)link;
        }
        
        #endregion
        
    }
    
}