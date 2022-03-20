using Engine.Data.Generation;
using Engine.Data.Generation.Factories;
using Engine.Generator;
using Engine.Logic.Locations.Generator.Builders;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations.Generator
{

    /// <summary>
    /// 
    /// 
    /// ---
    /// 
    /// 
    /// </summary>
    public class BuilderBuildFactory
    {

        #region Singleton

        private static Lazy<BuilderBuildFactory> instance = new Lazy<BuilderBuildFactory>(() => new BuilderBuildFactory());

        public static BuilderBuildFactory Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private BuilderBuildFactory()
        {
            foreach(var builder in AssembliesHandler.CreateImplementations<IBuilder>())
                builders.Add(builder.MarkerType, builder);
        }

        #endregion

        private IDictionary<Type, IBuilder> builders = new Dictionary<Type, IBuilder>();

        public void Build(GenerationBuildContext context)
        {
            var elementFactory = LocationTypeSuperFactory.Instance.Get(context.BuildInfo.LocationType);
            var buildType = context.BuildInfo.RoomType;
            var variationCount = elementFactory.GetCount(buildType);
            var buildingVariationID = new System.Random((int)context.BuildInfo.BuildID).Next(1, variationCount);
            context.BuildingElement = elementFactory.Get(buildType, buildingVariationID);

            foreach (var builder in builders)
            {
                builder.Value.Build(context);
            }
        }


    }

}
