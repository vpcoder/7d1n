using System.Collections.Generic;
using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using Engine.Logic.Dialog.Action.Impls;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Tutorial
{
    
    public class ExitDoorStoryCatcher : StorySelectCatcherBase
    {
        public override string StoryID => "main.chagedrad.start_exit_door";

        [SerializeField] private Transform door;
        [SerializeField] private Vector3 openAngles;
        [SerializeField] private Transform lookTo;
        
        [SerializeField] private Transform extCameraPoint;
        [SerializeField] private Transform extCameraLookAtPoint;
        
        [SerializeField] private CharacterNpcBehaviour laberiusNpc;
        [SerializeField] private Transform goToPoint;

        [SerializeField] private Transform playerSetPos;
        [SerializeField] private Transform securityGuardSetPos;
        [SerializeField] private Transform zombieSetPos;
        [SerializeField] private CharacterNpcBehaviour zombie;
        
        [SerializeField] private Transform insideCameraPos;
        
        public override void CreateDialog(DialogQueue dlg)
        {
            var background = ObjectFinder.SceneViewImage;
            var laberius = laberiusNpc.Character;
            
            dlg.Run(() =>
            {
                Camera.main.SetState(PlayerEyePos, lookTo);
                QuestFactory.Instance.Get<TutorialQuest>().Stage++;
            });
            dlg.Text("Кажется, тут закрыто...");
            dlg.TextPlayer("- Эй! Откройте!");
            dlg.Run(() =>
            {
                Camera.main.SetState(extCameraPoint, extCameraLookAtPoint);
                StoryActionHelper.NpcGoTo(laberiusNpc, goToPoint);
            });
            dlg.Delay(1.5f);
            dlg.Sound("quests/tutorial/door_open");
            dlg.Delay(1f);
            dlg.Run(() =>
            {
                door.transform.localRotation = Quaternion.Euler(openAngles);
                StoryActionHelper.NpcLookAt(laberiusNpc, PlayerCharacter.transform);
            });
            dlg.Delay(1f);
            dlg.TextAnother("- Ёшкин кот, что у вас здесь происходит?!", laberius);
            dlg.Run(() =>
            {
                Camera.main.SetState(PlayerEyePos, lookTo);
            });
            dlg.Text("Этот дед выглядит напуганным");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, Color.clear, Color.white, 0.5f));
            });
            dlg.Delay(0.6f);
            dlg.Run(() =>
            {
                PlayerCharacter.gameObject.SetActive(true);
                PlayerCharacter.transform.SetState(playerSetPos);
                laberiusNpc.transform.SetState(securityGuardSetPos);
                zombie.transform.SetState(zombieSetPos);

                Camera.main.SetState(insideCameraPos, zombie.transform);
                
                StoryActionHelper.NpcLookAt(laberiusNpc, zombie.transform);
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, Color.white, Color.clear, 0.5f));
            });
            dlg.TextAnother("- Ебучий случай...", laberius);
            
            var accidentPoint = SelectVariant.Point;
            var selfdefensePoint = SelectVariant.Point;
            var nextPoint = SelectVariant.Point;
            var list = new List<SelectVariant>()
            {
                SelectVariant.New("Это была случайность!", accidentPoint),
                SelectVariant.New("Это была самооборона!", selfdefensePoint),
            };
            dlg.Select(list);

            dlg.Point(accidentPoint);
            dlg.TextPlayer("- Это была случайность!");
            dlg.TextAnother("- Ничего себе случайность...", laberius);
            dlg.Sound("quests/tutorial/zombie_talk", zombie.AttackAudioSource);
            dlg.TextPlayer("- Дед, смотри, оно двинулось! Ебошь!");
            dlg.Run(() =>
            {
                StoryActionHelper.NpcSwitchWeapon(laberiusNpc, 5007L);
            });
            dlg.TextAnother("- Тваю мать!", laberius);
            dlg.Run(() =>
            {
                laberiusNpc.Target = zombie.Damaged;
                StoryActionHelper.NpcAttack(laberiusNpc);
            });
            dlg.Delay(2f);
            dlg.TextAnother("- Ты это видел?! Видел?!!", laberius);
            dlg.TextPlayer("- Дед, а я тебе про что говорил!");
            dlg.TextAnother("- Так, стоять на месте! А я позову остальных, с вами со всеми надо что то делать...", laberius);
            dlg.TextAnother("- Развели тут... ходячих мертвецов, понимаешь...", laberius);
            dlg.GoTo(nextPoint);
            
            dlg.Point(selfdefensePoint);
            dlg.TextPlayer("- Это была самооборона!");
            dlg.TextAnother("- Ты по что девчонке череп проломил?", laberius);
            dlg.TextAnother("- Какая самооборона?!", laberius);
            dlg.TextPlayer("- Дед, она сама на меня набросилась!");
            dlg.TextPlayer("- Ты посмотри на неё, у неё вид как у девочки из колодца.");
            dlg.TextPlayer("- А рожа? Да она как будто летучих мышей сожрала!");
            dlg.TextPlayer("- У этого вообще дыра в груди!");
            dlg.TextPlayer("- Дед, тут два варианта, либо этот мужик из уэко мунде сбежал, либо его эта девица оприходовала.");
            dlg.TextPlayer("- И знаешь, когда я проснулся, никаких шинигами рядом не было, так что вариант, по факту, один...");
            dlg.TextAnother("- Так, молчать.", laberius);
            dlg.TextAnother("- Чё то у вас тут какая то хуйня происходит, позову остальных...", laberius);
            dlg.TextAnother("- Развели тут... хуякомунды, понимаешь...", laberius);

            dlg.Point(nextPoint);
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, Color.clear, Color.white, 0.5f));
            });
            dlg.Delay(0.6f);
            
            
            dlg.Text("конец...");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, Color.white, Color.clear, 0.5f));
            });
            dlg.End();
        }
        
        /// <summary>
        ///     Если история выполнялась, никогда не выполняем её более 1 раза
        ///     ---
        ///     If story has been run, never run it more than once
        /// </summary>
        public override bool SecondInit() { return false; }

    }
    
}
