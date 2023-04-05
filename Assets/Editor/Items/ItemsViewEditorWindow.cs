using System.Collections.Generic;
using System.Linq;
using Engine.Data;
using Engine.Data.Factories;
using GitIntegration.Items.Data;
using UnityEditor;
using UnityEngine;

namespace GitIntegration.Items
{

    public static class ItemsEditorWindowHelper
    {
        [MenuItem("7d1n/Editors/Item Editor")]
        public static void ShowItemsEditor()
        {
            EditorWindow.GetWindow<ItemsViewEditorWindow>("Редактор предметов | Item Editor").Show();
        }
    }
    
    public class ItemsViewEditorWindow : EditorWindow
    {

        private Vector2 iconSize = new Vector2(64f, 64f);
        private Vector2 iconIncellSize = new Vector2(40f, 20f);
        private Vector2 currentScrollPos;

        private List<ItemRow> ItemsWithFilter
        {
            get
            {
                if (selectedGroupType == 0 && string.IsNullOrWhiteSpace(txtNameFilter))
                {
                    return ItemsEditorFactory.Instance.Items;
                }

                GroupType? typeFilter = null;
                if(selectedGroupType != 0)
                    typeFilter = Enums<GroupType>.GetValuesArray()[selectedGroupType - 1];
                return ItemsEditorFactory.Instance.Items.Where(item => (typeFilter == null || item.Type == typeFilter) && item.Name.ToLower().Contains(txtNameFilter.ToLower())).ToList();
            }
        }

        private static string[] Types
        {
            get
            {
                if (typesCache == null)
                {
                    var list = new List<string>();
                    list.Add("Any Types");
                    list.AddRange(Enums<GroupType>.Names);
                    typesCache = list.ToArray();
                }
                return typesCache;
            }
        }

        private int selectedGroupType = 0;
        private string txtNameFilter = string.Empty;
        private static string[] typesCache;
        
        private void OnGUI()
        {
            if(GUILayout.Button("Перезагрузить фабрику | Reload Factory"))
                ItemsEditorFactory.Instance.ReloadData();

            var items = ItemsWithFilter;
            var height = (((float)items.Count / MaxCountX) + 1) * (iconSize.y + iconIncellSize.y);
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("найдено предметов | items count: " + items.Count);
            
            GUIStyle style = new GUIStyle(GUI.skin.label) {alignment = TextAnchor.MiddleRight};
            EditorGUILayout.LabelField("фильтр по имени | name filter: ", style, GUILayout.ExpandWidth(true));
            txtNameFilter = GUILayout.TextField(txtNameFilter);
            GUILayout.EndHorizontal();
            selectedGroupType = GUILayout.SelectionGrid(selectedGroupType, Types, 6);
            
            currentScrollPos = GUI.BeginScrollView(new Rect()
            {
                x = 0,
                y = 110,
                width = position.width,
                height = position.height - 140,
            }, currentScrollPos, new Rect()
            {
                x = 0,
                y = 0,
                width = position.width,
                height = height,
            });
            
            var index = 0;
            foreach (var item in items)
                AddItem(item, index++);
            
            GUI.EndScrollView();
            
            GUILayout.Space(position.height - 130);
            if(GUILayout.Button("Создать новый предмет | Add new item"))
            {
                GetWindow<AddNewItemEditorWindow>("Создать предмет | Create item").Show();
            }
        }

        private int MaxCountX
        {
            get
            {
                return (int)(position.width / (iconSize.x + iconIncellSize.x));
            }
        }
        
        private int MaxCountY
        {
            get
            {
                return (int)(position.width / (iconSize.x + iconIncellSize.x));
            }
        }

        private void AddItem(ItemRow item, int index)
        {
            var maxCountX = MaxCountX;
            var indexX = (index % maxCountX);
            var indexY = (index / maxCountX); // countX - это не опечатка

            var pos = new Vector2(indexX * iconSize.x + indexX * iconIncellSize.x,
                indexY * iconSize.y + indexY * iconIncellSize.y);
            
            var sprite = SpriteFactory.Instance.Get(item.ID);
            if (sprite != null)
            {
                GUI.DrawTexture(new Rect()
                {
                    x = pos.x,
                    y = pos.y,
                    width = iconSize.x,
                    height = iconSize.y,
                }, sprite.texture);
            }

            GUI.Label(new Rect()
            {
                x = pos.x,
                y = pos.y + iconSize.y * 0.5f,
                width = iconSize.x * 1.5f,
                height = iconSize.y,
            }, item.ID + "\n" + item.Name);

            if (GUI.Button(new Rect()
                {
                    x = pos.x + iconSize.y,
                    y = pos.y,
                    width = 20,
                    height = 16,
                }, "..."))
            {
                var window = GetWindow<EditItemEditorWindow>("Создать предмет | Create item");
                window.Load(item);
                window.ShowModal();
            }
            
        }

    }
    
}
