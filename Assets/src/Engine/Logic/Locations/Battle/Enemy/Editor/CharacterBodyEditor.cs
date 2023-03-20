using Engine.Data;
using Engine.Data.Factories;
using UnityEditor;
using UnityEngine;

namespace Engine.Logic.Locations.Char.Impls
{
    
    [CustomEditor(typeof(CharacterBody), true)]
    public class CharacterBodyEditor : CustomEditorT<CharacterBody>
    {

        public override void OnAdditionEditor()
        {
            var item = target.Target;
            var group = item.Data.orderGroup;
            var fractionIcon = FractionFactory.Instance.Get(group);
            
            if(fractionIcon != null)
                GUILayout.Label(fractionIcon.texture, GUIStyle.none);
        }

    }
    
}
