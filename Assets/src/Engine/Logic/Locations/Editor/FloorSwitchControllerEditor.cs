using UnityEditor;
using UnityEngine;

namespace Engine.Logic.Locations
{
    
    [CustomEditor(typeof(FloorSwitchController), true)]
    public class FloorSwitchControllerEditor : CustomEditorT<FloorSwitchController>
    {
        
        public override void OnAdditionEditor()
        {
            if (GUILayout.Button("SetFloor"))
                target.Target.SetFloor(target.Target.CurrentFloor);
        }

    }
    
}