using UnityEngine;

namespace Engine.Logic.Locations.Generator.Markers
{

    public class WallMarker : MarkerBase
    {

        public override Vector3 Bounds { get { return LocationGenerateContex.WALL_TILE_SIZE; } }

        #region Editor

#if UNITY_EDITOR && DEBUG

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            var bounds = Bounds;

            float newX = transform.position.x - (transform.position.x % LocationGenerateContex.EDITOR_WEB_SIZE.x);
            float newY = transform.position.y - (transform.position.y % LocationGenerateContex.EDITOR_WEB_SIZE.y);
            float newZ = transform.position.z - (transform.position.z % LocationGenerateContex.EDITOR_WEB_SIZE.z);
            transform.position = new Vector3(newX, newY, newZ);

            var q = transform.rotation;
            Gizmos.DrawLine(Position, Position + q * new Vector3(-bounds.x, 0f, 0f));
            Gizmos.DrawLine(Position, Position + q * new Vector3(0f, 0f, bounds.z));
            Gizmos.DrawLine(Position + q * new Vector3(-bounds.x, 0f, 0f), Position + q * new Vector3(-bounds.x, 0f, bounds.z));
            Gizmos.DrawLine(Position + q * new Vector3(0f, 0f, bounds.z), Position + q * new Vector3(-bounds.x, 0f, bounds.z));
            Gizmos.DrawLine(Position, Position + q * new Vector3(-bounds.x, 0f, bounds.z));

            Gizmos.DrawLine(Position + q * new Vector3(0f, bounds.y, 0f), Position + q * new Vector3(-bounds.x, bounds.y, 0f));
            Gizmos.DrawLine(Position + q * new Vector3(0f, bounds.y, 0f), Position + q * new Vector3(0f, bounds.y, bounds.z));
            Gizmos.DrawLine(Position + q * new Vector3(-bounds.x, bounds.y, 0f), Position + q * new Vector3(-bounds.x, bounds.y, bounds.z));
            Gizmos.DrawLine(Position + q * new Vector3(0f, bounds.y, bounds.z), Position + q * new Vector3(-bounds.x, bounds.y, bounds.z));
            Gizmos.DrawLine(Position + q * new Vector3(0f, bounds.y, 0f), Position + q * new Vector3(-bounds.x, bounds.y, bounds.z));

            Gizmos.DrawLine(Position + q * new Vector3(-bounds.x, 0f, 0f), Position + q * new Vector3(-bounds.x, bounds.y, 0f));
            Gizmos.DrawLine(Position + q * new Vector3(0f, 0f, bounds.z), Position + q * new Vector3(0f, bounds.y, bounds.z));
            Gizmos.DrawLine(Position + q * new Vector3(-bounds.x, 0f, bounds.z), Position + q * new Vector3(-bounds.x, bounds.y, bounds.z));
            Gizmos.DrawLine(Position, Position + q * new Vector3(0f, bounds.y, 0f));

            if (!IsTwoSide)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawWireCube(Position + q * (new Vector3(-bounds.x * 0.5f, bounds.y * 0.5f, -bounds.z)), Vector3.one * 0.1f);

                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(Position + q * (new Vector3(-bounds.x * 0.5f, bounds.y * 0.5f, bounds.z)), Vector3.one * 0.2f);
            }
        }

#endif

        #endregion

    }

}
