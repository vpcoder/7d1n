using Engine.Data;
using Engine.Data.Factories;
using UnityEditor;
using UnityEngine;

namespace Engine.Logic.Locations.Char.Impls
{
    
    [CustomEditor(typeof(CharacterNpcBehaviour), true)]
    public class CustomizableNpcEditor : CustomEditorT<CharacterNpcBehaviour>
    {

        public override void OnAdditionEditor()
        {
            var npc = target.Target;
            var group = npc.CharacterBody?.Data.orderGroup ?? OrderGroup.ZombieGroup;
            var fractionIcon = FractionFactory.Instance.Get(group);
            
            if(fractionIcon != null)
                GUILayout.Label(fractionIcon.texture, GUIStyle.none);
            
            GUILayout.Label("Здоровье | Health: " + npc.Character.Health);
        }

    }
    
}
