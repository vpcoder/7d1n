using Engine.Logic.Locations.Generator.Markers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Builders
{

    /// <summary>
    /// 
    /// Билдер, который обрабатываем маркеры и превращает их в объекты
    /// ---
    /// A builder that processes markers and turns them into objects
    /// 
    /// </summary>
    /// <typeparam name="T">
    ///     Тип маркера, с которым работает билдер
    ///     ---
    ///     Type of marker the builder works with
    /// </typeparam>
    public abstract class BuilderBase<T> : IBuilder where T : IMarker
    {

        #region Properties
        
        /// <summary>
        ///     Тип маркера, с которым может работать данный билдер
        ///     ---
        ///     The type of marker this builder can work with
        /// </summary>
        public Type MarkerType { get { return typeof(T); } }

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
        
        #region Shared Methods
        
        /// <summary>
        ///     Выполняет преобразование маркеров в объекты
        ///     ---
        ///     Converts markers into objects
        /// </summary>
        /// <param name="context">
        ///     Контекст генерации, с информацией о всех маркерах в сцене
        ///     ---
        ///     Generation context, with information about all markers in the scene
        /// </param>
        public abstract void Build(BuildLocationGlobalInfo context);

        #endregion
        
        #region Utils Methods

        /// <summary>
        ///     Получает все маркеры типа "type" из контекста
        ///     ---
        ///     Gets all "type" markers from the context
        /// </summary>
        /// <param name="context">
        ///     Контекст генерации, с информацией о всех маркерах в сцене
        ///     ---
        ///     Generation context, with information about all markers in the scene
        /// </param>
        /// <param name="type">
        ///     Тип, для которого необходимо получить маркеры
        ///     ---
        ///     The type for which you want to get markers
        /// </param>
        /// <returns>
        ///     Список маркеров для типа "type"
        ///     ---
        ///     List of markers for type "type"
        /// </returns>
        protected IList<IMarker> GetMarkers<V>(BuildLocationGlobalInfo context) where V : class
        {
            return context.Markers
                .Where(marker => marker is V)
                .ToList();
        }
        
        #endregion

    }

}
