using Engine.Data;
using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Tutorial
{
    
    public class ExitDoorStoryCatcher : StorySelectCatcherBase
    {

        [SerializeField] private Transform door;
        [SerializeField] private Vector3 openAngles;
        [SerializeField] private Transform lookTo;
        
        [SerializeField] private CharacterNpcBehaviour securityGuardNpc;
        [SerializeField] private Transform goToPoint;
        
        public override void CreateDialog(DialogQueue dlg)
        {
            dlg.Run(() =>
            {
                Camera.main.SetState(PlayerEyePos, lookTo);
                QuestFactory.Instance.Get<TutorialQuest>().Stage++;
            });
            dlg.Text("Кажется, тут закрыто...");
            dlg.Text("- Эй! Откройте!");
            dlg.Run(() => StoryActionHelper.NpcGoTo(securityGuardNpc, goToPoint));
            dlg.Delay(2f, true);
            dlg.Run(() =>
            {
                door.transform.localRotation = Quaternion.Euler(openAngles);
                StoryActionHelper.NpcLookAt(securityGuardNpc, PlayerCharacter.transform);
            });
            dlg.Delay(1f, true);
            dlg.Run(() =>
            {
                StoryActionHelper.NpcSwitchWeapon(securityGuardNpc, 5007L);
            });
            dlg.Text("- Ёбанный в рот! Что у вас здесь происходит?");
            
            

            dlg.Text("Выходим");
        }

    }
    
}
