using UnityEditor;
using UnityEngine;

namespace Engine.Logic.Editor
{
    
    [CustomEditor(typeof(CharacterMeshSwitcher), true)]
    public class CharacterMeshSwitcherEditor : CustomEditorT<CharacterMeshSwitcher>
    {

        public override void OnAdditionEditor()
        {
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button("<"))
            {
                target.Target.MeshIndex--;
            }
            
            if (GUILayout.Button(">"))
            {
                target.Target.MeshIndex++;
            }
            
            GUILayout.EndHorizontal();
        }

        public override string GetDescription() {
            return "Селектор меша персонажа";
        }
        
    }
    
}