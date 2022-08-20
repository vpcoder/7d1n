using System;
using System.Collections.Generic;
using Engine.Logic.Locations.Generator.Markers;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace Engine.Logic.Locations.Generator.Builders
{

    public class FloorBuilder : BuilderBase<FloorMarker>
    {

        public override void Build(BuildLocationGlobalInfo context)
        {
            var currentMarks = GetMarkers<FloorMarker>(context);
            if (currentMarks == null)
                return;
            
            BuildFloor(context, currentMarks);
        }

        private void BuildFloor(BuildLocationGlobalInfo context, ICollection<IMarker> currentMarks)
        {
            foreach (var abstractMarker in currentMarks)
            {
                var marker = (FloorMarker)abstractMarker;
                var position = marker.Position;
                var rotation = Quaternion.Euler(marker.Rotation);

                var item = Object.Instantiate(context.BuildingElement.Floor, position, rotation, BuildParent);
                if (!marker.IsWalkable)
                {
                    Object.Destroy(item.GetComponent<WalkableFloor>());
                    item.GetComponent<NavMeshModifier>().area = 1;
                }
                
                BuildWall(context, marker.Tile);
            }
        }

        private void BuildWall(BuildLocationGlobalInfo context, TileItem tile)
        {
            // внутренние и специальные стены
            BuildInsideWalls(context, tile);
            
            // Внешние стены
            BuildOutsideWalls(context, tile);
        }

        private void BuildInsideWalls(BuildLocationGlobalInfo context, TileItem tile)
        {
            var edges = new[] { EdgeLayout.LeftInside, EdgeLayout.RightInside, EdgeLayout.TopInside, EdgeLayout.BottomInside };
            foreach (var layout in edges)
                BuildInsideEdge(context, tile, layout);
        }
        
        private void BuildOutsideWalls(BuildLocationGlobalInfo context, TileItem tile)
        {
            var edges = new[] { EdgeLayout.LeftOutside, EdgeLayout.RightOutside, EdgeLayout.TopOutside, EdgeLayout.BottomOutside };
            foreach (var layout in edges)
                BuildOutsideEdge(context, tile, layout);
        }
        
        private void BuildInsideEdge(BuildLocationGlobalInfo context, TileItem tile, EdgeLayout layout)
        {
            var marker = tile.Marker;
            var edge = tile.GetEdge(layout);
            if (edge != EdgeType.Empty)
                BuildInsideEdge(context, edge, marker.GetLayoutPos(layout), marker.GetLayoutRot(layout));
        }
        
        private void BuildOutsideEdge(BuildLocationGlobalInfo context, TileItem tile, EdgeLayout layout)
        {
            var marker = tile.Marker;
            var edge = tile.GetEdge(layout);
            if (edge != EdgeType.Empty && tile.GetTile(layout) == null)
                BuildOutsideEdge(context, edge, marker.GetLayoutPos(layout), marker.GetLayoutRot(layout));
        }

        private void BuildInsideEdge(BuildLocationGlobalInfo context, EdgeType type, Vector3 pos, Quaternion rot)
        {
            var prefab = GetInsideWallPrefabByEdgeType(context, type);
            if (prefab == null)
                return;

            Object.Instantiate(prefab, pos, rot, BuildParent);
        }

        private GameObject GetInsideWallPrefabByEdgeType(BuildLocationGlobalInfo context, EdgeType type)
        {
            if (type == EdgeType.Empty)
                return null;
            switch (type)
            {
                case EdgeType.Door: return context.BuildingElement.InsideWallWithDoor;
                case EdgeType.Window: return context.BuildingElement.InsideWallWithWindow;
                case EdgeType.Wall: return context.BuildingElement.InsideWall;
                default:
                    throw new NotSupportedException();
            }
        }
        
        private void BuildOutsideEdge(BuildLocationGlobalInfo context, EdgeType type, Vector3 pos, Quaternion rot)
        {
            var prefab = GetOutsideWallPrefabByEdgeType(context, type);
            if (prefab == null)
                return;

            Object.Instantiate(prefab, pos, rot, BuildParent);
        }
        
        private GameObject GetOutsideWallPrefabByEdgeType(BuildLocationGlobalInfo context, EdgeType type)
        {
            if (type == EdgeType.Empty)
                return null;
            switch (type)
            {
                case EdgeType.Door: return context.BuildingElement.OutsideWallWithDoor;
                case EdgeType.Window: return context.BuildingElement.OutsideWallWithWindow;
                case EdgeType.Wall: return context.BuildingElement.OutsideWall;
                default:
                    throw new NotSupportedException();
            }
        }
        
    }

}
