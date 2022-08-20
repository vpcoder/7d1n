using Engine;
using Engine.Components;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace src.Engine.I18n
{
    
    [CustomEditor(typeof(I18nText), true)]
    public class I18nTextEditor : CustomEditorT<I18nText>
    {

        private bool isCheckMode = false;
        
        public override void OnAdditionEditor()
        {
            var text = target.Target.gameObject.GetComponent<Text>();
            var key = target.Target.TextKey;
            
            if (GUILayout.Button("Check | Проверка"))
                isCheckMode = !isCheckMode;

            var localizedText = Localization.Instance.Get(key);
            localizedText = localizedText != null ? localizedText : "???";
            text.text = isCheckMode ? localizedText : "#" + key;
        }
        
    }
    
}