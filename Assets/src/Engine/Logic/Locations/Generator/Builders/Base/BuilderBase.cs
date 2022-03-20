using Engine.Logic.Locations.Generator.Markers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Builders
{

    public abstract class BuilderBase<T> : IBuilder where T : IMarker
    {

        public Type MarkerType { get { return typeof(T); } }

        public abstract void Build(GenerationBuildContext context);

        private Transform buildParent;

        protected IList<IMarker> GetMarkers(GenerationBuildContext context)
        {
            return GetMarkers(context, MarkerType);
        }

        protected IList<IMarker> GetMarkers(GenerationBuildContext context, Type type)
        {
            if (!context.MarkersByType.TryGetValue(type, out var currentMarkers))
                return null;
            return currentMarkers;
        }

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

    }

}
