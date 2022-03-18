using Engine.Logic.Locations.Generator.Markers;
using UnityEngine;
using UnityEngine.AI;

namespace Engine.Logic.Locations.Generator.Builders
{

    public class FloorBuilder : BuilderBase<FloorMarker>
    {

        public override void Build(GenerationBuildContext context)
        {
            var currentMarks = context.MarkersByType[MarkerType];
            if (currentMarks == null)
                return;

            var floorObject = Resources.Load<GameObject>("Locations/Builds/Floor/Floor01");
            var floor = GameObject.Find("Floor").transform;

            foreach (var abstractMarker in currentMarks) {

                var marker = abstractMarker as FloorMarker;
                var position = marker.Position;
                var rotation = Quaternion.Euler(marker.Rotation);

                var item = GameObject.Instantiate<GameObject>(floorObject, position, rotation, floor);
                if (!marker.IsWalkable)
                {
                    GameObject.Destroy(item.GetComponent<WalkableFloor>());
                    item.GetComponent<NavMeshModifier>().area = 1;
                }
            }
        }

    }

}
