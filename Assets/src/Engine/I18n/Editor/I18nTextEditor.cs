using Engine;
using Engine.Components;
using UnityEditor;
using UnityEngine.UI;

namespace src.Engine.I18n
{
    
    [CustomEditor(typeof(I18nText), true)]
    public class I18nTextEditor : CustomEditorT<I18nText>
    {
        
        public override void OnAdditionEditor()
        {
            var text = target.Target.gameObject.GetComponent<Text>();
            var key = target.Target.TextKey;
            var localizedText = Localization.Instance.Get(key);
            text.text = localizedText != null ? localizedText : "???";
        }
        
    }
    
}