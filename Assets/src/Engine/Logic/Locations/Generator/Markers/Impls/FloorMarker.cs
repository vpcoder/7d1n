using System;
using System.Collections.Generic;
using Engine.Logic.Locations.Generator.Environment.Building;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Markers
{

    public class FloorMarker : MarkerBase
    {

        #region Hidden Fields
        
#if UNITY_EDITOR
        [Caption("Use NavMeshSurface")]
        [Comments("Можно ли ходить по указанному тайлу пола? | Is it possible to walk on the specified floor tile?")]
#endif
        [SerializeField] private bool isWalkable = true;

#if UNITY_EDITOR
        [Caption("Left wall")]
        [Comments("Есть ли слева от этого тайла стена? | Is there a wall to the left of this tile?")]
#endif
        [SerializeField] private EdgeType leftEdge   = EdgeType.Empty;
        
#if UNITY_EDITOR
        [Caption("Right wall")]
        [Comments("Есть ли справа от этого тайла стена? | Is there a wall to the right of this tile?")]
#endif
        [SerializeField] private EdgeType rightEdge  = EdgeType.Empty;
        
#if UNITY_EDITOR
        [Caption("Top wall")]
        [Comments("Есть ли сверху от этого тайла стена? | Is there a wall to the top of this tile?")]
#endif
        [SerializeField] private EdgeType topEdge    = EdgeType.Empty;
        
#if UNITY_EDITOR
        [Caption("Bottom wall")]
        [Comments("Есть ли снизу от этого тайла стена? | Is there a wall to the bottom of this tile?")]
#endif
        [SerializeField] private EdgeType bottomEdge = EdgeType.Empty;

        [SerializeField] private RoomHierarchyBehaviour roomHierarchy;
        
        #endregion
        
        #region Properties
        
        public bool IsWalkable { get { return isWalkable; } set { isWalkable = value; } }

        public TileItem Tile { get; set; }

        public RoomHierarchyBehaviour RoomHierarchy
        {
            get { return this.roomHierarchy; }
            set { this.roomHierarchy = value; }
        }

        public EdgeType LeftEdge
        {
            get { return leftEdge; }
            set { leftEdge = value; }
        }
        
        public EdgeType RightEdge
        {
            get { return rightEdge; }
            set { rightEdge = value; }
        }
        
        public EdgeType TopEdge
        {
            get { return topEdge; }
            set { topEdge = value; }
        }
        
        public EdgeType BottomEdge
        {
            get { return bottomEdge; }
            set { bottomEdge = value; }
        }

        public override Vector3 Bounds { get { return LocationGenerateContex.FLOOR_TILE_SIZE; } }

        public EdgeType GetEdge(EdgeLayout layout)
        {
            switch (layout)
            {
                case EdgeLayout.LeftInside:
                case EdgeLayout.LeftOutside:
                    return LeftEdge;
                case EdgeLayout.RightInside:
                case EdgeLayout.RightOutside:
                    return RightEdge;
                case EdgeLayout.TopInside:
                case EdgeLayout.TopOutside:
                    return TopEdge;
                case EdgeLayout.BottomInside:
                case EdgeLayout.BottomOutside:
                    return BottomEdge;
                default:
                    throw new NotSupportedException(layout.ToString());
            }
        }
        
        public Quaternion GetLayoutRot(EdgeLayout layout)
        {
            switch (layout)
            {
                case EdgeLayout.BottomInside:
                    return Quaternion.Euler(0f, -90f, 0f);
                case EdgeLayout.TopInside:
                    return Quaternion.Euler(0f, 90f, 0f);
                case EdgeLayout.RightInside:
                    return Quaternion.Euler(0f, 180f, 0f);
                case EdgeLayout.LeftInside:
                    return Quaternion.Euler(0f, 0f, 0f);
                
                case EdgeLayout.BottomOutside:
                    return Quaternion.Euler(0f, 90f, 0f);
                case EdgeLayout.TopOutside:
                    return Quaternion.Euler(0f, -90f, 0f);
                case EdgeLayout.RightOutside:
                    return Quaternion.Euler(0f, 0f, 0f);
                case EdgeLayout.LeftOutside:
                    return Quaternion.Euler(0f, 180f, 0f);
                
                case EdgeLayout.Floor:
                case EdgeLayout.Ceiling:
                    return Quaternion.identity;
                
                default:
                    throw new NotSupportedException(layout.ToString());
            }
        }
        
        public Vector3 GetLayoutPos(EdgeLayout layout)
        {
            switch (layout)
            {
                case EdgeLayout.BottomInside:
                    return transform.position + new Vector3(0, 0, LocationGenerateContex.WALL_TILE_SIZE.x);
                case EdgeLayout.TopInside:
                    return transform.position + new Vector3(-LocationGenerateContex.WALL_TILE_SIZE.x, 0, 0);
                case EdgeLayout.RightInside:
                    return transform.position + new Vector3(-LocationGenerateContex.WALL_TILE_SIZE.x, 0,
                        LocationGenerateContex.WALL_TILE_SIZE.x);
                case EdgeLayout.LeftInside:
                    return transform.position;
                
                case EdgeLayout.BottomOutside:
                    return transform.position;
                case EdgeLayout.TopOutside:
                    return transform.position + new Vector3(-LocationGenerateContex.WALL_TILE_SIZE.x, 0, LocationGenerateContex.WALL_TILE_SIZE.x);
                case EdgeLayout.RightOutside:
                    return transform.position + new Vector3(0, 0, LocationGenerateContex.WALL_TILE_SIZE.x);
                case EdgeLayout.LeftOutside:
                    return transform.position + new Vector3(-LocationGenerateContex.WALL_TILE_SIZE.x, 0, 0);
                
                case EdgeLayout.Floor:
                case EdgeLayout.Ceiling:
                    return transform.position;
                
                default:
                    throw new NotSupportedException(layout.ToString());
            }
        }
        
        public Vector3 GetSegmentPos(EdgeLayout layout, TileSegmentType type)
        {
            switch (type)
            {
                case TileSegmentType.S00:
                    return transform.position + new Vector3(-Bounds.x, 0, Bounds.z) * 0.25f;
                case TileSegmentType.S01:
                    return transform.position + new Vector3(-Bounds.x * 0.75f, 0, Bounds.z * 0.25f);
                case TileSegmentType.S10:
                    return transform.position + new Vector3(-Bounds.x * 0.25f, 0, Bounds.z * 0.75f);
                case TileSegmentType.S11:
                    return transform.position + new Vector3(-Bounds.x * 0.75f, 0, Bounds.z * 0.75f);
                default:
                    throw new NotSupportedException(layout.ToString());
            }
        }
        
        #endregion
        
        #region Editor

#if UNITY_EDITOR && DEBUG

        private static Vector2 LINE_SIZE = new Vector2(0.1f, 0.3f);
        
        public Color Emission { get; set; } = Color.black;

        public Dictionary<TileSegmentType, Color> Segments = new Dictionary<TileSegmentType, Color>();

        private void OnDrawGizmosSelected()
        {
            var bounds = Bounds;
            
            foreach (var segment in Segments)
            {
                Gizmos.color = segment.Value;
                Gizmos.DrawCube(GetSegmentPos(EdgeLayout.Floor, segment.Key), new Vector3(bounds.x * 0.5f, LINE_SIZE.y, bounds.z * 0.5f));
            }
            
            Gizmos.color = Color.green;
            var q = transform.rotation;
            
            Gizmos.color = GetByEdge(LeftEdge);
            Gizmos.DrawCube(Position - q * new Vector3(bounds.x * 0.5f, 0f, 0f), new Vector3(bounds.x, LINE_SIZE.y, LINE_SIZE.x));
            
            Gizmos.color = GetByEdge(BottomEdge);
            Gizmos.DrawCube(Position + q * new Vector3(0f, 0f, bounds.z * 0.5f), new Vector3(LINE_SIZE.x, LINE_SIZE.y, bounds.z));
            
            Gizmos.color = GetByEdge(RightEdge);
            Gizmos.DrawCube(Position - q * new Vector3(bounds.x * 0.5f, 0f, -bounds.z), new Vector3(bounds.x, LINE_SIZE.y, LINE_SIZE.x));
            
            Gizmos.color = GetByEdge(TopEdge);
            Gizmos.DrawCube(Position + q * new Vector3(-bounds.x, 0f, bounds.z * 0.5f), new Vector3(LINE_SIZE.x, LINE_SIZE.y, bounds.z));
            
            /*
            if (LeftEdge != EdgeType.Empty)
                DrawWall(GetByEdge(LeftEdge), LeftOutsideWallPos, LeftOutsideWallRot, LocationGenerateContex.WALL_TILE_SIZE);
            
            if (RightEdge != EdgeType.Empty)
                DrawWall(GetByEdge(RightEdge), RightOutsideWallPos, RightOutsideWallRot, LocationGenerateContex.WALL_TILE_SIZE);
            
            if (TopEdge != EdgeType.Empty)
                DrawWall(GetByEdge(TopEdge), TopOutsideWallPos, TopOutsideWallRot, LocationGenerateContex.WALL_TILE_SIZE);
            
            if (BottomEdge != EdgeType.Empty)
                DrawWall(GetByEdge(BottomEdge), BottomOutsideWallPos, BottomOutsideWallRot, LocationGenerateContex.WALL_TILE_SIZE);
            */
        }

        private Color GetByEdge(EdgeType edge)
        {
            switch (edge)
            {
                case EdgeType.Window:
                    return Color.yellow;
                case EdgeType.Wall:
                    return Color.red;
                case EdgeType.Door:
                    return Color.white;
            }
            return Color.black;
        }
        
        private void OnDrawGizmos()
        {
            DrawFloor();

            if (LeftEdge != EdgeType.Empty)
                DrawWall(GetByEdge(LeftEdge),  GetLayoutPos(EdgeLayout.LeftInside), GetLayoutRot(EdgeLayout.LeftInside), LocationGenerateContex.WALL_TILE_SIZE);
            
            if (RightEdge != EdgeType.Empty)
                DrawWall(GetByEdge(RightEdge), GetLayoutPos(EdgeLayout.RightInside), GetLayoutRot(EdgeLayout.RightInside), LocationGenerateContex.WALL_TILE_SIZE);
            
            if (TopEdge != EdgeType.Empty)
                DrawWall(GetByEdge(TopEdge), GetLayoutPos(EdgeLayout.TopInside), GetLayoutRot(EdgeLayout.TopInside), LocationGenerateContex.WALL_TILE_SIZE);
            
            if (BottomEdge != EdgeType.Empty)
                DrawWall(GetByEdge(BottomEdge), GetLayoutPos(EdgeLayout.BottomInside), GetLayoutRot(EdgeLayout.BottomInside), LocationGenerateContex.WALL_TILE_SIZE);
        }

        private void DrawFloor()
        {
            Gizmos.color = IsWalkable ? Color.blue : Color.red;

            if (Emission != Color.black)
                Gizmos.color = Emission;
            
            var bounds = Bounds;
            var pos = transform.position;
            var newX = pos.x - (pos.x % LocationGenerateContex.EDITOR_WEB_SIZE.x);
            var newY = pos.y - (pos.y % LocationGenerateContex.EDITOR_WEB_SIZE.y);
            var newZ = pos.z - (pos.z % LocationGenerateContex.EDITOR_WEB_SIZE.z);
            transform.position = pos = new Vector3(newX, newY, newZ);

            Gizmos.DrawLine(Position, Position - new Vector3(bounds.x, 0f, 0f));
            Gizmos.DrawLine(Position, Position - new Vector3(0f, 0f, -bounds.z));
            Gizmos.DrawLine(Position - new Vector3(bounds.x, 0f, 0f), Position - new Vector3(bounds.x, 0f, -bounds.z));
            Gizmos.DrawLine(Position - new Vector3(0f, 0f, -bounds.z), Position - new Vector3(bounds.x, 0f, -bounds.z));
            Gizmos.DrawLine(Position, Position - new Vector3(bounds.x, 0f, -bounds.z));

            var color = Gizmos.color;
            color.a = 0.3f;
            Gizmos.color = color;
            Gizmos.DrawCube(pos - new Vector3(bounds.x, 0f, -bounds.z) * 0.5f, new Vector3(0.5f, 0f, 0.5f));
        }

        private void DrawWall(Color color, Vector3 position, Quaternion q, Vector3 bounds)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(position, position + q * new Vector3(-bounds.x, 0f, 0f));
            Gizmos.DrawLine(position, position + q * new Vector3(0f, 0f, bounds.z));
            Gizmos.DrawLine(position + q * new Vector3(-bounds.x, 0f, 0f), position + q * new Vector3(-bounds.x, 0f, bounds.z));
            Gizmos.DrawLine(position + q * new Vector3(0f, 0f, bounds.z), position + q * new Vector3(-bounds.x, 0f, bounds.z));
            Gizmos.DrawLine(position, position + q * new Vector3(-bounds.x, 0f, bounds.z));

            Gizmos.DrawLine(position + q * new Vector3(0f, bounds.y, 0f), position + q * new Vector3(-bounds.x, bounds.y, 0f));
            Gizmos.DrawLine(position + q * new Vector3(0f, bounds.y, 0f), position + q * new Vector3(0f, bounds.y, bounds.z));
            Gizmos.DrawLine(position + q * new Vector3(-bounds.x, bounds.y, 0f), position + q * new Vector3(-bounds.x, bounds.y, bounds.z));
            Gizmos.DrawLine(position + q * new Vector3(0f, bounds.y, bounds.z), position + q * new Vector3(-bounds.x, bounds.y, bounds.z));
            Gizmos.DrawLine(position + q * new Vector3(0f, bounds.y, 0f), position + q * new Vector3(-bounds.x, bounds.y, bounds.z));

            Gizmos.DrawLine(position + q * new Vector3(-bounds.x, 0f, 0f), position + q * new Vector3(-bounds.x, bounds.y, 0f));
            Gizmos.DrawLine(position + q * new Vector3(0f, 0f, bounds.z), position + q * new Vector3(0f, bounds.y, bounds.z));
            Gizmos.DrawLine(position + q * new Vector3(-bounds.x, 0f, bounds.z), position + q * new Vector3(-bounds.x, bounds.y, bounds.z));
            Gizmos.DrawLine(position, position + q * new Vector3(0f, bounds.y, 0f));
        }

#endif

        #endregion

    }

}
