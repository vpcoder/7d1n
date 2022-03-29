using Engine.Logic.Locations.Generator.Markers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Builders
{

    public class WallBuilder : BuilderBase<WallMarker>
    {

        public override void Build(GenerationRoomContext context)
        {
            
            var currentMarks = GetMarkers(context);
            var floorMarkers = GetMarkers(context, typeof(FloorMarker));
            if (floorMarkers == null && currentMarks == null)
                return;

            var wallPrefab = context.BuildingElement.InsideWall;
            var outsidePrefab = context.BuildingElement.OutsideWall;

            if (currentMarks != null)
            {
                foreach (var abstractMarker in currentMarks)
                {
                    var marker = abstractMarker as WallMarker;
                    var position = marker.Position;
                    var rotation = Quaternion.Euler(marker.Rotation);

                    GameObject.Instantiate<GameObject>(wallPrefab, position, rotation, BuildParent);

                    if (marker.IsTwoSide)
                    {
                        GameObject.Instantiate<GameObject>(wallPrefab, position - new Vector3(LocationGenerateContex.FLOOR_TILE_SIZE.x, 0, 0), Quaternion.Euler(rotation.eulerAngles + new Vector3(0, 180, 0)), BuildParent);
                    }
                }
            }

            ISet<Vector3> existsFloor = new HashSet<Vector3>();
            ISet<Vector3> existsWall = new HashSet<Vector3>();
            foreach (var markerItem in context.AllMarkers)
            {
                if (markerItem is WallMarker || markerItem is WallWithDoorMarker || markerItem is WallWithWindowMarker) {
                    existsWall.Add(markerItem.Position);
                    continue;
                }
                if (markerItem is FloorMarker)
                    existsFloor.Add(markerItem.Position);
            }

            var floorPoints = floorMarkers.Select(floor => (FloorMarker)floor).ToDictionary(marker => marker.Position);
            var floorWithoutWall = floorPoints.Values.Where(floor => !hasItemIn(floorPoints, floor, 0, 1)
                                                                  || !hasItemIn(floorPoints, floor, 0, -1)
                                                                  || !hasItemIn(floorPoints, floor, 1, 0)
                                                                  || !hasItemIn(floorPoints, floor, -1, 0)).ToList();
            foreach (var floor in floorWithoutWall)
            {
                Quaternion rotation = Quaternion.Euler(0, -90, 0);
                if (!hasItemIn(floorPoints, floor, 1, 0))
                {
                    var position = floor.Position + new Vector3(0, 0, LocationGenerateContex.FLOOR_TILE_SIZE.z);
                    if (!existsWall.Contains(position))
                    {
                        GameObject.Instantiate<GameObject>(wallPrefab, position, rotation, BuildParent);
                        position = position - new Vector3(0, 0, LocationGenerateContex.FLOOR_TILE_SIZE.z);
                        GameObject.Instantiate<GameObject>(outsidePrefab, position, Quaternion.Euler(rotation.eulerAngles + new Vector3(0, 180, 0)), BuildParent);
                    }
                }
                rotation = Quaternion.Euler(0, 90, 0);
                if (!hasItemIn(floorPoints, floor, -1, 0))
                {
                    var position = floor.Position - new Vector3(LocationGenerateContex.FLOOR_TILE_SIZE.x, 0, 0);
                    if (!existsWall.Contains(position))
                    {
                        GameObject.Instantiate<GameObject>(wallPrefab, position, rotation, BuildParent);
                        position = position + new Vector3(0, 0, LocationGenerateContex.FLOOR_TILE_SIZE.z);
                        GameObject.Instantiate<GameObject>(outsidePrefab, position, Quaternion.Euler(rotation.eulerAngles + new Vector3(0, 180, 0)), BuildParent);
                    }
                }
                rotation = Quaternion.Euler(0, 180, 0);
                if (!hasItemIn(floorPoints, floor, 0, 1))
                {
                    var position = floor.Position + new Vector3(-LocationGenerateContex.FLOOR_TILE_SIZE.x, 0, LocationGenerateContex.FLOOR_TILE_SIZE.z);
                    if (!existsWall.Contains(position))
                    {
                        GameObject.Instantiate<GameObject>(wallPrefab, position, rotation, BuildParent);
                        position = position + new Vector3(LocationGenerateContex.FLOOR_TILE_SIZE.x, 0, 0);
                        GameObject.Instantiate<GameObject>(outsidePrefab, position, Quaternion.Euler(rotation.eulerAngles + new Vector3(0, 180, 0)), BuildParent);
                    }
                }
                rotation = Quaternion.Euler(0, 0, 0);
                if (!hasItemIn(floorPoints, floor, 0, -1))
                {
                    var position = floor.Position;
                    if (!existsWall.Contains(position))
                    {
                        GameObject.Instantiate<GameObject>(wallPrefab, position, rotation, BuildParent);
                        position = position - new Vector3(LocationGenerateContex.FLOOR_TILE_SIZE.x, 0, 0);
                        GameObject.Instantiate<GameObject>(outsidePrefab, position, Quaternion.Euler(rotation.eulerAngles + new Vector3(0, 180, 0)), BuildParent);
                    }
                }
            }
        }
        
        private bool hasItemIn(IDictionary<Vector3, FloorMarker> data, FloorMarker floor, int offsetX, int offsetZ)
        {
            Vector3 nextPos = floor.Position + new Vector3(offsetX * LocationGenerateContex.FLOOR_TILE_SIZE.x, 0f, offsetZ * LocationGenerateContex.FLOOR_TILE_SIZE.z);
            return data.ContainsKey(nextPos);
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
            switch (direction)
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
