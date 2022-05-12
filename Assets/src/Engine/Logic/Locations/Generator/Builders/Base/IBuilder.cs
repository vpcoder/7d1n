using Engine.Logic.Locations.Generator.Markers;
using System;
using System.Collections.Generic;

namespace Engine.Logic.Locations.Generator.Builders
{

    /// <summary>
    /// 
    /// Билдер, который обрабатываем маркеры и превращает их в объекты
    /// ---
    /// A builder that processes markers and turns them into objects
    /// 
    /// </summary>
    public interface IBuilder
    {

        /// <summary>
        ///     Тип маркера, с которым может работать данный билдер
        ///     ---
        ///     The type of marker this builder can work with
        /// </summary>
        Type MarkerType { get; }

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
        void Build(BuildLocationGlobalInfo context);

    }

}
