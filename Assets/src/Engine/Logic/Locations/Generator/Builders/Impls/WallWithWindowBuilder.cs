using Engine.Logic.Locations.Generator.Markers;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Builders
{

    public class WallWithWindowBuilder : BuilderBase<WallWithWindowMarker>
    {

        public override void Build(GenerationBuildContext context)
        {
            var currentMarks = context.MarkersByType[MarkerType];
            if (currentMarks == null)
                return;

            var floorObject = Resources.Load<GameObject>("Locations/Builds/Window/Window01");
            var wall = GameObject.Find("Wall").transform;

            foreach (var abstractMarker in currentMarks) {
                var position = abstractMarker.Position;
                var rotation = Quaternion.Euler(abstractMarker.Rotation);
                GameObject.Instantiate<GameObject>(floorObject, position, rotation, wall);
            }
        }

    }

}
