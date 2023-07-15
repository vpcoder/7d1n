using UnityEditor;
using UnityEngine;

namespace Engine.Story
{
    
    [CustomEditor(typeof(StoryBase), true)]
    public class StoryEditor : CustomEditorT<StoryBase>
    {

        private bool defaultFade;
        
        public override void OnDefaultEditor()
        {
            var story = target.Target;
            
            GUILayout.BeginHorizontal();
            if (story.IsNeedRunOnStart)
            {
                GUI.color = Color.yellow;
                GUILayout.Toggle(story.gameObject.activeInHierarchy, story.gameObject.activeInHierarchy ? "ON" : "OFF", GUILayout.Width(40f));
            }
            else
            {
                GUI.color = story.IsActive ? Color.green : Color.red;
                bool active = GUILayout.Toggle(story.IsActive, story.IsActive ? "ON" : "OFF", GUILayout.Width(40f));
                if (active != story.IsActive)
                    story.IsActive = active;
            }
            GUI.color = Color.white;
            
            story.IsNeedRunOnStart = GUILayout.Toggle(story.IsNeedRunOnStart, "Run story on Start", GUI.skin.button);
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUI.color = story.IsUseShowFade ? Color.green : Color.white;
            story.IsUseShowFade = GUILayout.Toggle(story.IsUseShowFade, "Use Fade", GUI.skin.button);
            GUI.color = story.IsHidePlayer ? Color.green : Color.white;
            story.IsHidePlayer = GUILayout.Toggle(story.IsHidePlayer, "Hide Player", GUI.skin.button);
            GUI.color = story.IsHideTopPanel ? Color.green : Color.white;
            story.IsHideTopPanel = GUILayout.Toggle(story.IsHideTopPanel, "Hide Top Panel", GUI.skin.button);
            GUI.color = story.IsDestroyGameObject ? Color.yellow : Color.white;
            story.IsDestroyGameObject = GUILayout.Toggle(story.IsDestroyGameObject, "Destroy Object", GUI.skin.button);
            GUILayout.EndHorizontal();

            GUI.color = Color.white;
            if(GUILayout.Button("Execute Dialog"))
                target.Target.RunDialog();
        }

        public override void OnAdditionEditor()
        {
            defaultFade = GUILayout.Toggle(defaultFade, "defaul editor");
            if(defaultFade)
                base.OnDefaultEditor();
        }

    }
    
}