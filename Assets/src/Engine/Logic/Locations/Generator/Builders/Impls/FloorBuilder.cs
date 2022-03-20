using Engine.Data.Generation;
using Engine.Logic.Locations.Generator.Markers;
using UnityEngine;
using UnityEngine.AI;

namespace Engine.Logic.Locations.Generator.Builders
{

    public class FloorBuilder : BuilderBase<FloorMarker>
    {

        public override void Build(GenerationBuildContext context)
        {
            var currentMarks = GetMarkers(context);
            if (currentMarks == null)
                return;

            var floorObject = context.BuildingElement.Floor;

            foreach (var abstractMarker in currentMarks) {

                var marker = abstractMarker as FloorMarker;
                var position = marker.Position;
                var rotation = Quaternion.Euler(marker.Rotation);

                var item = GameObject.Instantiate<GameObject>(floorObject, position, rotation, BuildParent);
                if (!marker.IsWalkable)
                {
                    GameObject.Destroy(item.GetComponent<WalkableFloor>());
                    item.GetComponent<NavMeshModifier>().area = 1;
                }
            }
        }

    }

}
