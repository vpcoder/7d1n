using Engine.Logic.Locations.Generator.Markers;
using System;
using System.Collections.Generic;

namespace Engine.Logic.Locations.Generator.Builders
{

    /// <summary>
    /// 
    /// Билдер, который обрабатываем маркеры и превращает их в объекты
    /// ---
    /// 
    /// 
    /// </summary>
    public interface IBuilder
    {

        /// <summary>
        ///     Тип маркера, с которым может работать данный билдер
        ///     ---
        ///     
        /// </summary>
        Type MarkerType { get; }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="context"></param>
        void Build(GenerationBuildContext context);

    }

}
