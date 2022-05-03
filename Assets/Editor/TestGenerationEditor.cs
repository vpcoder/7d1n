using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TestGeneration), true)]
public class TestGenerationEditor : CustomEditorT<TestGeneration>
 {

    public override void OnAdditionEditor()
    {
        if(GUILayout.Button("Make"))
        {
            target.Target.Make();
        }
        if(GUILayout.Button("Clear"))
        {
            target.Target.Clear();
        }
    }

}
