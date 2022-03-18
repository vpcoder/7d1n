using Engine.Logic.Locations.Generator.Markers;
using System;
using System.Collections.Generic;

namespace Engine.Logic.Locations.Generator.Builders
{

    public abstract class BuilderBase<T> : IBuilder where T : IMarker
    {

        public Type MarkerType { get { return typeof(T); } }

        public abstract void Build(GenerationBuildContext context);

    }

}
