using Engine.Logic.Locations.Generator.Markers;
using UnityEngine;
using UnityEngine.AI;

namespace Engine.Logic.Locations.Generator.Builders
{

    public class FloorBuilder : BuilderBase<FloorMarker>
    {

        public override void Build(GenerationRoomContext context)
        {
            var currentMarks = GetMarkers(context);
            if (currentMarks == null)
                return;

            foreach (var abstractMarker in currentMarks) {

                var marker = abstractMarker as FloorMarker;
                var position = marker.Position;
                var rotation = Quaternion.Euler(marker.Rotation);

                var item = GameObject.Instantiate<GameObject>(context.BuildingElement.Floor, position, rotation, BuildParent);
                if (!marker.IsWalkable)
                {
                    GameObject.Destroy(item.GetComponent<WalkableFloor>());
                    item.GetComponent<NavMeshModifier>().area = 1;
                }
            }
        }

    }

}
