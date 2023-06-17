using System.Collections.Generic;
using Engine.Data;
using Engine.Data.Repositories;
using UnityEditor;
using UnityEngine;

namespace Engine.Story
{

    public class StorySelectEditor : EditorWindow
    {

        private static bool showHandlers = true;
        private Vector2 scrollOffset;
        private static readonly List<StoryBase> stories = new List<StoryBase>();
        
        private void OnEnable()
        {
            stories.Clear();
            foreach(StoryBase item in Resources.FindObjectsOfTypeAll(typeof(StoryBase)))
                stories.Add(item);
        }

        [DrawGizmo(GizmoType.NotInSelectionHierarchy | GizmoType.InSelectionHierarchy)]
        public static void RenderCustomGizmo(Transform item, GizmoType gizmoType)
        {
            if (!showHandlers)
                return;
            
            var story = item.GetComponent<StoryBase>();
            if (story == null)
                return;

            GUI.color = IsStoryActive(story) ? Color.green : Color.grey;
            Handles.Label(story.transform.position, story.GetType().Name + ((gizmoType & GizmoType.Selected) != 0 ? "*" : ""));
        }

        private static bool IsStoryActive(IStory story)
        {
            return story.IsActive || story.IsNeedRunOnStart;
        }
        
        private void OnGUI()
        {
            var needRefresh = false;

            scrollOffset = GUILayout.BeginScrollView(scrollOffset);
            showHandlers = GUILayout.Toggle(showHandlers, "Отображать истории в сцене | Show stories on the scene");
            GUILayout.Label("В сцене | In scene: " + stories.Count);
            GUILayout.Space(10f);
            
            if (GUILayout.Button("Save"))
            {
                CharacterRepository.Instance.SaveAll(Game.Instance.Character);
            }
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Refresh"))
            {
                OnEnable();
            }
            if (GUILayout.Button("Active All"))
            {
                foreach (var story in stories)
                    story.gameObject.SetActive(true);
            }
            if (GUILayout.Button("Inactive All"))
            {
                foreach (var story in stories)
                    story.gameObject.SetActive(false);
            }
            GUILayout.EndHorizontal();
            
            GUILayout.Space(10f);
            
            foreach (var story in stories)
            {
                // Ссылка может протухнуть, например кто-то работает с редактором в рантайме, и персонаж выполняет квесты
                if (story == null)
                {
                    needRefresh = true;
                    break;
                }
                
                GUILayout.BeginHorizontal();
                GUI.color = Color.white;
                DrawStoryRow(story);
                GUI.color = Color.white;
                GUILayout.EndHorizontal();
            }

            GUILayout.Space(10f);
            APILayout.CommentBox("Run - Принудительно выполняет историю (доступно только при запуске).\n" +
                                 "Кнопка с именем истории - выделяет объект истории в иерархии.\n" +
                                 "Active - Активность GameObject (выключает объект).\n" +
                                 "ON/OFF - Включение/Выключение истории. Если история StoryOnStart - регулировать параметр невозможно, так как история запустится автоматически на старте сцены.");
            
            GUILayout.EndScrollView();
            
            // Если обнаружены протухшие ссылки, выполняем актуализацию списка историй которые находятся в сцене
            if (needRefresh)
                OnEnable();
        }

        private void DrawStoryRow(StoryBase story)
        {
            var activeWidth = position.width - 170f;

            GUI.enabled = Application.isPlaying;
            if (GUILayout.Button("Run", GUILayout.Width(40f)))
            {
                story.RunDialog();
            }
            GUI.enabled = true;
            
            if (GUILayout.Button(story.StoryID, GUILayout.Width(activeWidth)))
            {
                Selection.activeTransform = story.transform;
            }
            
            story.gameObject.SetActive(GUILayout.Toggle(story.gameObject.activeInHierarchy, "Active", GUILayout.Width(60f)));
            
            if (story.IsNeedRunOnStart)
            {
                GUI.color = Color.yellow;
                GUILayout.Toggle(story.gameObject.activeInHierarchy, story.gameObject.activeInHierarchy ? "ON" : "OFF", GUILayout.Width(40f));
            }
            else
            {
                GUI.color = story.IsActive ? Color.green : Color.red;
                story.IsActive = GUILayout.Toggle(story.IsActive, story.IsActive ? "ON" : "OFF", GUILayout.Width(40f));
            }
        }
        
    }
    
}