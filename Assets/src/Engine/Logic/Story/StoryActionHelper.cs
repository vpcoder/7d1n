using Engine.Data;
using Engine.Logic.Locations;
using Engine.Story.Actions;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Story
{
    
    public static class StoryActionHelper
    {

        private static T Create<T>(GameObject source) where T : MonoBehaviour
        {
            var prev = source.GetComponent<T>();
            if (prev != null)
                prev.Destroy();
            
            return source.AddComponent<T>();
        }
        
        public static LookAtAndMoveStoryAction LookAtAndMove(Transform source, Transform target, Vector3 moveToPoint, float speed = 1f)
        {
            var obj = Create<LookAtAndMoveStoryAction>(source.gameObject);
            obj.Init(source.transform, target, moveToPoint, speed);
            return obj;
        }
        
        public static LookAtAndMoveStoryAction LookAtAndMove(Camera source, Transform target, Vector3 moveToPoint, float speed = 1f)
        {
            return LookAtAndMove(source.transform, target, moveToPoint, speed);
        }
        
        public static LookAtStoryAction LookAt(Transform source, Transform target, float speed = 1f)
        {
            var obj = Create<LookAtStoryAction>(source.gameObject);
            obj.Init(source.transform, target, speed);
            return obj;
        }
        
        public static LookAtStoryAction LookAt(Camera source, Transform target, float speed = 1f)
        {
            return LookAt(source.transform, target, speed);
        }
        
        public static LookAtStoryAction LookAt(GameObject source, Transform target, float speed = 1f)
        {
            return LookAt(source.transform, target, speed);
        }

        public static BackgroundFaderStoryAction Fade(Image image, Color from, Color to, float speed = 1f)
        {
            var obj = Create<BackgroundFaderStoryAction>(image.gameObject);
            obj.Init(image, from, to, speed);
            return obj;
        }

        
        public static NpcLookAtStoryAction NpcLookAt(CharacterNpcBehaviour npc, Transform target, bool needResetAnotherActions = true, float speed = 1f)
        {
            var action = Create<NpcLookAtStoryAction>(npc.gameObject);
            action.Init(npc, target, needResetAnotherActions, speed);
            return action;
        }
        
        public static NpcGoToStoryAction NpcGoTo(CharacterNpcBehaviour npc, Transform target, bool needResetAnotherActions = true, float speed = 1f)
        {
            var action = Create<NpcGoToStoryAction>(npc.gameObject);
            action.Init(npc, target, needResetAnotherActions, speed);
            return action;
        }
        
        public static NpcAttackStoryAction NpcAttack(CharacterNpcBehaviour npc, bool needResetAnotherActions = true)
        {
            var action = Create<NpcAttackStoryAction>(npc.gameObject);
            action.Init(npc, needResetAnotherActions);
            return action;
        }

        
        public static NpcSwitchWeaponStoryAction NpcSwitchWeapon(CharacterNpcBehaviour npc, long weaponID, bool needResetAnotherActions = true)
        {
            var action = Create<NpcSwitchWeaponStoryAction>(npc.gameObject);
            action.Init(npc, weaponID, needResetAnotherActions);
            return action;
        }
        
        public static NpcSwitchWeaponStoryAction NpcSwitchWeapon(CharacterNpcBehaviour npc, IWeapon weapon, bool needResetAnotherActions = true)
        {
            var action = Create<NpcSwitchWeaponStoryAction>(npc.gameObject);
            action.Init(npc, weapon, needResetAnotherActions);
            return action;
        }
        
    }
    
}