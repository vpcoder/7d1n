using Engine.Data.Factories;
using Engine.Logic.Locations.Objects;
using UnityEditor;
using UnityEngine;

namespace Engine.Logic.Locations.Editor
{
    
    [CustomEditor(typeof(LocationDroppedItemBehaviour), true)]
    public class LocationDroppedItemBehaviourEditor : CustomEditorT<LocationDroppedItemBehaviour>
    {

        public override void OnAdditionEditor()
        {
            var item = target.Target.Item;

            GUILayout.Space(30f);

            if (item == null)
            {
                APILayout.WarningBox("Item info by id " + target.Target.ItemInfo.ID + " not founded, try check resources.\nИнформация о предмете по ИД " + target.Target.ItemInfo.ID + " не найдена, проверьте ресурсы.");
                
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