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
            foreach(var builder in builders)
            {
                var currentMarkers = context.MarkersByType[builder.Key];
                builder.Value.Build(context);
            }
        }

    }

}
