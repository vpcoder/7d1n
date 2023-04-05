using UnityEditor;
using UnityEngine;

namespace Engine.Story.Editor
{
    
    [CustomEditor(typeof(StoryBase), true)]
    public class StoryEditor : CustomEditorT<StoryBase>
    {
        public override void OnAdditionEditor()
        {
            base.OnAdditionEditor();
            
            if(GUILayout.Button("Execute Dialog"))
                target.Target.RunDialog();
        }
    }
    
}