using System.Collections.Generic;
using Engine;
using Engine.Data;
using Engine.Data.Factories;
using UnityEditor;
using UnityEditor.Sdon.Controls;
using UnityEngine;

namespace GitIntegration.Items.Data
{
    
    public class PartListAdapter : ListAdapter<Part>
    {

        private Dictionary<Part, Vector2> scrolls = new Dictionary<Part, Vector2>();

        public override Part ConstructItem()
        {
            return new Part();
        }

        public override float GetItemHeight(Part part) {
            return 96f;
        }
        
        public override void DrawItem(Rect position, Part part)
        {
            if(!scrolls.ContainsKey(part))
                scrolls.Add(part, Vector2.zero);
            
            var item = ItemFactory.Instance.Get(part.ResourceID);
            
            GUI.DrawTexture(new Rect()
            {
                x = position.x,
                y = position.y + 18,
                width  = 62,
                height = 62,
            }, item.Sprite.texture);

            position.width -= 64;
            position.x += 75;

            var blockWidth = position.width / 2;
            
            GUI.Label(position, Localization.Instance.Get(item.Name));
            GUI.Label(new Rect()
            {
                x = position.x,
                y = position.y + 20,
                width = blockWidth,
                height = 20,
            }, Localization.Instance.Get(item.Description));
             
            long.TryParse(GUI.TextField(new Rect()
            {
                x = position.x,
                y = position.y + 40,
                width = blockWidth * 0.5f,
                height = 21,
            }, part.ResourceID.ToString()), out part.ResourceID);

            if(GUI.Button(new Rect()
               {
                   x = position.x + blockWidth * 0.5f + 8,
                   y = position.y + 40,
                   width = blockWidth * 0.5f - 8,
                   height = 21,
               }, "select"))
            {
                var window = EditorWindow.GetWindow<ResourceSelectEditorWindow>("Выбор ресурса | Resource selector");
                window.Selected = part.ResourceID;
                window.ShowModal();
                part.ResourceID = window.Selected;
            }
            
            position.x += blockWidth + 8;
            position.width -= blockWidth + 8;

            blockWidth = position.width / 2f;

            GUI.Label(new Rect()
            {
                x = position.x,
                y = position.y,
                width = blockWidth,
                height = 20,
            }, "Количество | Count:");
            long.TryParse(GUI.TextField(new Rect()
            {
                x = position.x,
                y = position.y + 20,
                width = blockWidth,
                height = 21,
            }, part.ResourceCount.ToString()), out part.ResourceCount);
            
            GUI.Label(new Rect()
            {
                x = position.x,
                y = position.y + 40,
                width = blockWidth,
                height = 20,
            }, "Сложность | Difficulty:");
            long.TryParse(GUI.TextField(new Rect()
            {
                x = position.x,
                y = position.y + 60,
                width = blockWidth,
                height = 21,
            }, part.Difficulty.ToString()), out part.Difficulty);
            
            position.x += blockWidth + 8;
            position.width -= blockWidth + 20;

            scrolls[part] = GUI.BeginScrollView(new Rect()
            {
                x = position.x,
                y = position.y,
                width = position.width + 2,
                height = position.height,
            }, scrolls[part], new Rect()
            {
                x = position.x,
                y = position.y,
                width = position.width + 2,
                height = (Enums<ToolType>.Count / 5) * (position.width / 5),
            });

            DrawTools(part, position);
            
            GUI.EndScrollView();
        }

        private void DrawTools(Part part, Rect rect)
        {
            ISet<ToolType> tools = part.NeededTools;
            if (tools == null)
            {
                part.NeededTools = new HashSet<ToolType>();
                tools = part.NeededTools;
            }
            
            int index = 0;
            foreach (var tool in Enums<ToolType>.GetValuesArray())
            {
                var indexX = index % 5;
                var indexY = index / 5;
                DrawTool(tool, tools.Contains(tool), rect, new Vector2Int(indexX, indexY), part);
                index++;
            }
        }

        private void DrawTool(ToolType tool, bool enabled, Rect rect, Vector2Int pos, Part part)
        {
            var sprite = ToolFactory.Instance.Get(tool);
            var size = (rect.width - 13) / 5;

            var oldColor = GUI.backgroundColor;
            GUI.backgroundColor = enabled ? Color.green : Color.red;
            
            if (GUI.Button(new Rect()
                {
                    x = rect.x + pos.x * size + 2,
                    y = rect.y + pos.y * size,
                    width = size,
                    height = size,
                }, sprite.texture))
            {
                if (enabled)
                    part.NeededTools.Remove(tool);
                else
                    part.NeededTools.Add(tool);
            }
            
            GUI.backgroundColor = oldColor;
        }
        
    }
    
}