using System.Collections.Generic;
using Engine;
using Engine.Data;
using Engine.Data.Factories;
using UnityEditor;
using UnityEditor.Sdon.Controls;
using UnityEngine;

namespace GitIntegration.Items.Data
{
    
    public class ToolAdapter
    {

        private Vector2 scroll;

        private ISet<ToolType> tools = new HashSet<ToolType>();
        public ISet<ToolType> Tools => tools;
        public bool IsEmpty => Sets.IsEmpty(tools);
        public bool IsNotEmpty => !IsEmpty;

        public void SetData(ISet<ToolType> tools)
        {
            this.tools = tools;
        }

        private int maxToolSize = 48;
        private int minToolSize = 32;
        private int minToolCount = 2;
        private int toolCount;

        public ToolAdapter()
        { }
        
        public ToolAdapter(Part part)
        {
            if (part.NeededTools == null)
                part.NeededTools = new HashSet<ToolType>();
            SetData(part.NeededTools);
        }
        
        public void DrawTools(Rect position)
        {
            toolCount = minToolCount;
            float toolSize = position.width / toolCount;
            if (toolSize > maxToolSize)
                toolCount = (int)(position.width / maxToolSize);
            if (toolSize < minToolSize)
                toolCount = minToolCount;
            toolSize = position.width / toolCount;
            
            scroll = GUI.BeginScrollView(new Rect()
            {
                x = position.x,
                y = position.y,
                width = position.width + 2,
                height = position.height,
            }, scroll, new Rect()
            {
                x = position.x,
                y = position.y,
                width = position.width + 2,
                height = ((Enums<ToolType>.Count / toolCount) + 1) * toolSize,
            });
            DrawTools(tools, position);
            GUI.EndScrollView();
        }

        private void DrawTools(ISet<ToolType> tools, Rect rect)
        {
            int index = 0;
            foreach (var tool in Enums<ToolType>.GetValuesArray())
            {
                var indexX = index % toolCount;
                var indexY = index / toolCount;
                DrawTool(tool, tools.Contains(tool), rect, new Vector2Int(indexX, indexY), tools);
                index++;
            }
        }

        private void DrawTool(ToolType tool, bool enabled, Rect rect, Vector2Int pos, ISet<ToolType> tools)
        {
            var sprite = ToolFactory.Instance.Get(tool);
            var size = (rect.width - 13) / toolCount;

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
                    tools.Remove(tool);
                else
                    tools.Add(tool);
            }
            
            GUI.backgroundColor = oldColor;
        }
        
    }
    
}