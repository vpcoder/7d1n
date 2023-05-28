using Engine.Story.Editor;
using UnityEngine;

namespace UnityEditor.Menu
{
    
    public static class StoryEditorMenuItem
    {

        [MenuItem("7d1n/Editors/Story Editor")]
        public static void ReloadDbAction()
        {
            var window = EditorWindow.CreateWindow<StorySelectEditor>();
            window.titleContent = new GUIContent("Истории|Stories");
            window.Show();
        }
        
    }
    
}