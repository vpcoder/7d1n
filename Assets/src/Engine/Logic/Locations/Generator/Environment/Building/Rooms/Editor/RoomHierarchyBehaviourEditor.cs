using Engine.Logic.Locations.Generator.Markers;
using UnityEditor;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building
{

    [CustomEditor(typeof(RoomHierarchyBehaviour), true)]
    public class RoomHierarchyBehaviourEditor : CustomEditorT<RoomHierarchyBehaviour>
    {

        private const string MARKERS_FIELD_NAME = "markers";
        
        public override void OnAdditionEditor()
        {
            if (GUILayout.Button("Markers from childs"))
            {
                var markers = target.Transform.GetChildComponents<MarkerBase>();
                foreach (var marker in markers)
                    (marker as FloorMarker).RoomHierarchy = target.Target;
                target.SetFieldValue(MARKERS_FIELD_NAME, markers);
            }
        }

    }
    
}
