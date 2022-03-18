using UnityEngine;

namespace Engine.Logic.Locations.Generator.Markers
{

    public class FloorMarker : MarkerBase
    {

        [SerializeField] private bool isWalkable = true;

        public bool IsWalkable { get { return isWalkable; } set { isWalkable = value; } }



        public override Vector3 Bounds { get { return LocationGenerateContex.FLOOR_TILE_SIZE; } }

        #region Editor

#if UNITY_EDITOR && DEBUG

        private void OnDrawGizmos()
        {
            Gizmos.color = IsWalkable ? Color.blue : Color.red;
            var bounds = Bounds;

            float newX = transform.position.x - (transform.position.x % LocationGenerateContex.EDITOR_WEB_SIZE.x);
            float newY = transform.position.y - (transform.position.y % LocationGenerateContex.EDITOR_WEB_SIZE.y);
            float newZ = transform.position.z - (transform.position.z % LocationGenerateContex.EDITOR_WEB_SIZE.z);
            transform.position = new Vector3(newX, newY, newZ);

            Gizmos.DrawLine(Position, Position - new Vector3(bounds.x, 0f, 0f));
            Gizmos.DrawLine(Position, Position - new Vector3(0f, 0f, -bounds.z));
            Gizmos.DrawLine(Position - new Vector3(bounds.x, 0f, 0f), Position - new Vector3(bounds.x, 0f, -bounds.z));
            Gizmos.DrawLine(Position - new Vector3(0f, 0f, -bounds.z), Position - new Vector3(bounds.x, 0f, -bounds.z));
            Gizmos.DrawLine(Position, Position - new Vector3(bounds.x, 0f, -bounds.z));

            var color = Gizmos.color;
            color.a = 0.3f;
            Gizmos.color = color;
            Gizmos.DrawCube(transform.position - new Vector3(bounds.x, 0f, -bounds.z) * 0.5f, new Vector3(0.5f, 0f, 0.5f));
        }

#endif

        #endregion

    }

}
