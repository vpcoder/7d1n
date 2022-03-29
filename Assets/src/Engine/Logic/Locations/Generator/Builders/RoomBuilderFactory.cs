using System;
using System.Collections.Generic;
using UnityEngine;
using Engine.Data.Generation.Factories;
using Engine.Logic.Locations.Generator.Builders;
using Engine.Logic.Locations.Generator.Environment.Building.Arrangement;
using Engine.Logic.Locations.Generator.Environment.Building;
using src.Engine.Logic.Locations.Generator.Furniture;

namespace Engine.Logic.Locations.Generator
{

    /// <summary>
    /// 
    /// Фабрика выполняющая построение помещений по маркерам
    /// ---
    /// A factory that performs construction of rooms by markers
    /// 
    /// </summary>
    public class RoomBuilderFactory
    {

        #region Singleton

        private static Lazy<RoomBuilderFactory> instance = new Lazy<RoomBuilderFactory>(() => new RoomBuilderFactory());

        public static RoomBuilderFactory Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private RoomBuilderFactory()
        {
            foreach(var builder in AssembliesHandler.CreateImplementations<IBuilder>())
                builders.Add(builder.MarkerType, builder);
        }

        #endregion

        /// <summary>
        ///     Коллекция билдеров, которые умеют работать с маркерами
        ///     ---
        ///     A collection of builders who know how to work with markers
        /// </summary>
        private IDictionary<Type, IBuilder> builders = new Dictionary<Type, IBuilder>();

        /// <summary>
        ///     Выполняет построение помещения по маркерам
        ///     ---
        ///     Performs construction of a room by markers
        /// </summary>
        /// <param name="context">
        ///     Контекст помещения
        ///     ---
        ///     Room Context
        /// </param>
        public void BuildRoom(GenerationRoomContext context)
        {
            var elementFactory = LocationTypeSuperFactory.Instance.Get(context.BuildInfo.LocationType);
            var buildType = context.BuildInfo.RoomType;
            var variationCount = elementFactory.GetCount(buildType);
            var buildingVariationID = new System.Random((int)context.BuildInfo.BuildID).Next(1, variationCount);
            context.BuildingElement = elementFactory.Get(buildType, buildingVariationID);

            foreach (var builder in builders)
                builder.Value.Build(context);
        }

        /// <summary>
        ///     Выполняет генерацию и заполнение помещения объектами
        /// </summary>
        /// <param name="context">
        ///     Контекст помещения
        ///     ---
        ///     Room Context
        /// </param>
        public void BuildRoomObjects(GenerationRoomContext context)
        {
            // Формируем мебель и объекты интерьера в помещении
            context.FurnitureInfo.FurnitureItems = BuildFurnitureInfo(context);
            
            // Выполняем заполнение помещения
            DoArrangementFurniture(context);
        }

        /// <summary>
        ///     Выполняет генерацию мебели и заполнение ею помещения
        ///     ---
        ///     A collection of generated furniture for this room
        /// </summary>
        /// <param name="context">
        ///     Контекст генерации помещения
        ///     ---
        ///     Room Generation Context
        /// </param>
        private void DoArrangementFurniture(GenerationRoomContext context)
        {
            var list = ArrangementSuperFactory.Instance.Get(context.RoomKindType);
            if (list == null || list.Count == 0)
                return;
            
            var currentArrangementIndex = new System.Random((int)(context.BuildInfo.BuildID + context.BuildInfo.CurrentFloor)).Next(0, list.Count);
            var processor = list[currentArrangementIndex];
            
            processor.ArrangementProcess(context, context.FurnitureInfo.FurnitureItems);
        }
        
        /// <summary>
        ///     Выполняет генерацию мебели для помещения
        ///     ---
        ///     Furniture factory for a given room
        /// </summary>
        /// <param name="context">
        ///     Контекст генерации помещения
        ///     ---
        ///     Room Generation Context
        /// </param>
        /// <returns>
        ///     Коллекция сгенерированной мебели для этого помещения
        ///     ---
        ///     A collection of generated furniture for this room
        /// </returns>
        private ICollection<IEnvironmentItem> BuildFurnitureInfo(GenerationRoomContext context)
        {
            var list = FurnitureProcessorSuperFactory.Instance.Get(context.RoomKindType);
            if (list == null || list.Count == 0)
                return new List<IEnvironmentItem>();
            
            var currentFurnitureProcessorIndex = new System.Random((int)(context.BuildInfo.BuildID + context.BuildInfo.CurrentFloor)).Next(0, list.Count);
            var processor = list[currentFurnitureProcessorIndex];

            return processor.Create(context);
        }

    }

}
