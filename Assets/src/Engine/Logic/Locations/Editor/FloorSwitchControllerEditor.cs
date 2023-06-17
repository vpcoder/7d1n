using UnityEditor;
using UnityEngine;

namespace Engine.Logic.Locations
{
    
    [CustomEditor(typeof(GlobalFloorSwitchController), true)]
    public class FloorSwitchControllerEditor : CustomEditorT<GlobalFloorSwitchController>
    {
        
        public override void OnAdditionEditor()
        {
            if (GUILayout.Button("SetFloor"))
                target.Target.SetFloor(target.Target.CurrentFloor);
        }

    }
    
}