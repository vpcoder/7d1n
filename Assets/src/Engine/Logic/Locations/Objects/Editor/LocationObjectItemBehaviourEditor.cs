using Engine.Data.Factories;
using GluonGui.WorkspaceWindow.Views.WorkspaceExplorer.Explorer;
using UnityEditor;
using UnityEngine;

namespace Engine.Logic.Locations
{
    
    [CustomEditor(typeof(LocationObjectItemBehaviour), true)]
    public class LocationObjectItemBehaviourEditor : CustomEditorT<LocationObjectItemBehaviour>
    {

        public override void OnAdditionEditor()
        {
            var itemInfo = target.Target.ItemInfo;
            var item = ItemFactory.Instance.Get(itemInfo.ID);

            GUILayout.Space(30f);

            if (item == null)
            {
                APILayout.WarningBox("Item info by id " + itemInfo.ID + " not founded, try check resources.\nИнформация о предмете по ИД " + itemInfo.ID + " не найдена, проверьте ресурсы.");
                
                GUILayout.Space(30f);
                if (GUILayout.Button("Reload item factory"))
                    ItemFactory.Instance.ReloadFactory();
                
                return;
            }
            
            GUILayout.Label("name: " + item.Name + ", " + Localization.Instance.Get(item.Name));
            GUILayout.Label("desctiption: " + item.Description  + ", " + Localization.Instance.Get(item.Description));
            GUILayout.Label("weight: " + WeightCalculationService.GetWeightFormat(item.Weight));
            Icon = item.Sprite?.texture;
            
            GUILayout.Space(30f);
            if (GUILayout.Button("Reload item factory"))
                ItemFactory.Instance.ReloadFactory();
        }
        
    }
    
}