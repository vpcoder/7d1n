using Engine.Logic.Locations.Generator.Markers;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Builders
{

    public class WallWithWindowBuilder : BuilderBase<WallWithWindowMarker>
    {

        public override void Build(GenerationBuildContext context)
        {
            var currentMarks = GetMarkers(context);
            if (currentMarks == null)
                return;

            var floorObject = context.BuildingElement.InsideWallWithWindow;

            foreach (var abstractMarker in currentMarks) {
                var position = abstractMarker.Position;
                var rotation = Quaternion.Euler(abstractMarker.Rotation);
                GameObject.Instantiate<GameObject>(floorObject, position, rotation, BuildParent);
            }
        }

    }

}
