using UnityEditor;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement
{
    
    [CustomEditor(typeof(SurfaceLocalAnchorBehaviour), true)]
    public class SurfaceLocalAnchorBehaviourEditor : CustomEditorT<SurfaceLocalAnchorBehaviour>
    {

        private const string ANCHOR_POS_NAME = "anchorPos";
        private static Quaternion rotation = Quaternion.Euler(0f,180f,0f);
        
        public void OnSceneGUI()
        {
            var item = target.Target;
            var pos = target.GetFieldValue<Vector3>(ANCHOR_POS_NAME);
            pos = Handles.PositionHandle(pos, rotation);
            
            target.SetFieldValue(ANCHOR_POS_NAME, pos);
            Handles.color = Color.yellow;
            Handles.Label(pos, "Origin of local coordinates\nНачало локальных координат");

            var bounds = item.Bounds;

            Handles.color = Color.green;
            var posTop = item.TopSurfaceCenterPos;
            Handles.DrawWireCube(posTop, new Vector3(bounds.size.x, 0.01f, bounds.size.z));
            Handles.Label(posTop, "surface for objects\nповерхность для объектов");
            
            Handles.color = Color.yellow;
            var posBottom = item.BottomSurfaceCenterPos;
            Handles.DrawWireCube(posBottom, new Vector3(bounds.size.x, 0.01f, bounds.size.z));
            Handles.Label(posBottom, "bottom pos\nнижняя позиция");
        }

    }
    
}