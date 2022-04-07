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
        
        #endregion
        
        #region Properties
        
        public bool IsWalkable { get { return isWalkable; } set { isWalkable = value; } }

        public TileItem Tile { get; set; }

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

        public Vector3 LeftOutsideWallPos
        {
            get
            {
                return transform.position + new Vector3(-LocationGenerateContex.WALL_TILE_SIZE.x, 0, 0);
            }
        }

        public Quaternion LeftOutsideWallRot
        {
            get
            {
                return Quaternion.Euler(0f, 180f, 0f);
            }
        }

        public Vector3 RightOutsideWallPos
        {
            get
            {
                return transform.position + new Vector3(0, 0, LocationGenerateContex.WALL_TILE_SIZE.x);
            }
        }

        public Quaternion RightOutsideWallRot
        {
            get
            {
                return Quaternion.Euler(0f, 0f, 0f);
            }
        }

        public Vector3 TopOutsideWallPos
        {
            get
            {
                return transform.position + new Vector3(-LocationGenerateContex.WALL_TILE_SIZE.x, 0, LocationGenerateContex.WALL_TILE_SIZE.x);
            }
        }

        public Quaternion TopOutsideWallRot
        {
            get
            {
                return Quaternion.Euler(0f, -90f, 0f);
            }
        }

        public Vector3 BottomOutsideWallPos
        {
            get
            {
                return transform.position;
            }
        }
        
        public Quaternion BottomOutsideWallRot
        {
            get
            {
                return Quaternion.Euler(0f, 90f, 0f);
            }
        }
        
        public Vector3 LeftInsideWallPos
        {
            get
            {
                return transform.position;
            }
        }

        public Quaternion LeftInsideWallRot
        {
            get
            {
                return Quaternion.Euler(0f, 0f, 0f);
            }
        }

        public Vector3 RightInsideWallPos
        {
            get
            {
                return transform.position + new Vector3(-LocationGenerateContex.WALL_TILE_SIZE.x, 0,
                    LocationGenerateContex.WALL_TILE_SIZE.x);
            }
        }

        public Quaternion RightInsideWallRot
        {
            get
            {
                return Quaternion.Euler(0f, 180f, 0f);
            }
        }

        public Vector3 TopInsideWallPos
        {
            get
            {
                return transform.position + new Vector3(-LocationGenerateContex.WALL_TILE_SIZE.x, 0, 0);
            }
        }

        public Quaternion TopInsideWallRot
        {
            get
            {
                return Quaternion.Euler(0f, 90f, 0f);
            }
        }

        public Vector3 BottomInsideWallPos
        {
            get
            {
                return transform.position + new Vector3(0, 0, LocationGenerateContex.WALL_TILE_SIZE.x);
            }
        }
        
        public Quaternion BottomInsideWallRot
        {
            get
            {
                return Quaternion.Euler(0f, -90f, 0f);
            }
        }

        #endregion
        
        #region Editor

#if UNITY_EDITOR && DEBUG

        private static Vector2 LINE_SIZE = new Vector2(0.1f, 0.3f);
        
        public Color Emission { get; set; } = Color.black;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            var bounds = Bounds;
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
                DrawWall(GetByEdge(LeftEdge), LeftInsideWallPos, LeftInsideWallRot, LocationGenerateContex.WALL_TILE_SIZE);
            
            if (RightEdge != EdgeType.Empty)
                DrawWall(GetByEdge(RightEdge), RightInsideWallPos, RightInsideWallRot, LocationGenerateContex.WALL_TILE_SIZE);
            
            if (TopEdge != EdgeType.Empty)
                DrawWall(GetByEdge(TopEdge), TopInsideWallPos, TopInsideWallRot, LocationGenerateContex.WALL_TILE_SIZE);
            
            if (BottomEdge != EdgeType.Empty)
                DrawWall(GetByEdge(BottomEdge), BottomInsideWallPos, BottomInsideWallRot, LocationGenerateContex.WALL_TILE_SIZE);
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
