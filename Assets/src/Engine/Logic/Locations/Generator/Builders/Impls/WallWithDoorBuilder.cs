using Engine.Logic.Locations.Generator.Markers;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Builders
{

    public class WallWithDoorBuilder : BuilderBase<WallWithDoorMarker>
    {

        public override void Build(GenerationBuildContext context)
        {
            var currentMarks = context.MarkersByType[MarkerType];
            if (currentMarks == null)
                return;

            var wallWithDoorObject = Resources.Load<GameObject>("Locations/Builds/Door/WallDoor01");

            var wall = GameObject.Find("Wall").transform;

            foreach (var abstractMarker in currentMarks) {
                var position = abstractMarker.Position;
                var rotation = Quaternion.Euler(abstractMarker.Rotation);
                GameObject.Instantiate<GameObject>(wallWithDoorObject, position, rotation, wall);

                if (abstractMarker.IsTwoSide)
                {
                    rotation = Quaternion.Euler(rotation.eulerAngles + new Vector3(0, 180, 0));
                    var direction = CalcDirection(rotation);
                    switch(direction)
                    {
                        case Direction.Top:
                            position = position + new Vector3(LocationGenerateContex.FLOOR_TILE_SIZE.x, 0, 0);
                            break;
                        case Direction.Bottom:
                            position = position - new Vector3(LocationGenerateContex.FLOOR_TILE_SIZE.x, 0, 0);
                            break;
                        case Direction.Right:
                            position = position - new Vector3(0, 0, LocationGenerateContex.FLOOR_TILE_SIZE.z);
                            break;
                        case Direction.Left:
                            position = position + new Vector3(0, 0, LocationGenerateContex.FLOOR_TILE_SIZE.z);
                            break;
                    }
                    GameObject.Instantiate<GameObject>(wallWithDoorObject, position, rotation);
                }
            }
        }

        private Direction CalcDirection(Quaternion rotation)
        {
            int direction = Mathf.RoundToInt(rotation.eulerAngles.y);
            if (Mathf.Abs(90 - direction) < 10)
                direction = 90;
            if (Mathf.Abs(270 - direction) < 10)
                direction = -90;
            if (Mathf.Abs(180 - direction) < 10)
                direction = 180;
            if (Mathf.Abs(direction) < 10)
                direction = 0;
            switch(direction)
            {
                case -90: return Direction.Left;
                case 90: return Direction.Right;
                case 180: return Direction.Bottom;
                default: return Direction.Top;
            }
        }

        enum Direction
        {
            Top,
            Bottom,
            Right,
            Left,
        }

    }

}
