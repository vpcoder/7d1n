using System.Collections.Generic;
using Engine.Data;
using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using Engine.Logic.Dialog.Action.Impls;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Nyasevsk
{
    
    public class ExitDoorStoryCatcher : StorySelectCatcherBase
    {
        public override string StoryID => "main.nyasevsk1.start_exit_door";

        [SerializeField] private Transform door;
        [SerializeField] private Vector3 openAngles;
        [SerializeField] private Transform lookTo;
        
        [SerializeField] private Transform extCameraPoint;
        [SerializeField] private Transform extCameraLookAtPoint;
        
        [SerializeField] private CharacterNpcBehaviour laberius;
        [SerializeField] private Transform goToPoint;

        [SerializeField] private Transform playerSetPos;
        [SerializeField] private Transform securityGuardSetPos;
        [SerializeField] private Transform zombieSetPos;
        [SerializeField] private CharacterNpcBehaviour zombie;
        
        [SerializeField] private Transform insideCameraPos;
        
        public override void CreateDialog(DialogQueue dlg)
        {
            var background = ObjectFinder.SceneViewImage;
            
            dlg.Run(() =>
            {
                Camera.main.SetState(PlayerEyePos, lookTo);
                QuestFactory.Instance.Get<NyasevskStartQuest>().Stage++;
            });
            dlg.Text("Кажется, тут закрыто...");
            dlg.TextPlayer("- Эй! Откройте!");
            dlg.Run(() =>
            {
                Camera.main.SetState(extCameraPoint, extCameraLookAtPoint);
                StoryActionHelper.NpcGoTo(this.laberius, goToPoint);
            });
            dlg.Delay(1.5f);
            dlg.Sound("quests/tutorial/door_open");
            dlg.Delay(1f);
            dlg.Run(() =>
            {
                door.transform.localRotation = Quaternion.Euler(openAngles);
                StoryActionHelper.NpcLookAt(this.laberius, ObjectFinder.Character.Eye);
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
                ObjectFinder.Character.transform.SetState(playerSetPos);
                this.laberius.transform.SetState(securityGuardSetPos);
                zombie.transform.SetState(zombieSetPos);

                Camera.main.SetState(insideCameraPos, zombie.transform);
                
                StoryActionHelper.NpcLookAt(this.laberius, zombie.transform);
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
                StoryActionHelper.NpcSwitchWeapon(this.laberius, 5007L);
            });
            dlg.TextAnother("- Тваю мать!", laberius);
            dlg.Run(() =>
            {
                this.laberius.Target = zombie.Damaged;
                StoryActionHelper.NpcAttack(this.laberius);
            });
            dlg.Delay(2f);
            if (Game.Instance.Character.Account.Gender == GenderType.Male)
            {
                dlg.TextPlayer("- Ты это видел?! Видел?!!");
                dlg.TextPlayer("- Дед, а я тебе про что говорил!");
            }
            else
            {
                dlg.TextPlayer("- Ты это видела?! Видела?!!");
                dlg.TextPlayer("- Дед, а я тебе про что говорила!");
            }
            dlg.TextAnother("- Так, стоять на месте! А я позову остальных, с вами со всеми надо что то делать...", laberius);
            dlg.TextAnother("- Развели тут... ходячих мертвецов, понимаешь...", laberius);
            dlg.GoTo(nextPoint);
            
            dlg.Point(selfdefensePoint);
            dlg.TextPlayer("- Это была самооборона!");
            dlg.TextAnother("- Какая самооборона?!", laberius);
            dlg.TextPlayer("- Дед, она сама на меня набросилась!");
            dlg.TextPlayer("- Ты посмотри на неё, она как будто летучих мышей сожрала!");
            dlg.TextPlayer("- У этого вообще дыра в груди!");
            dlg.TextPlayer("- Дед, тут два варианта, либо этот мужик из уэко мунде сбежал, либо его эта девица оприходовала.");
            if (Game.Instance.Character.Account.Gender == GenderType.Male)
                dlg.TextPlayer("- И знаешь, когда я проснулся, никаких шинигами рядом не было, так что вариант, по факту, один...");
            else
                dlg.TextPlayer("- И знаешь, когда я проснулась, никаких шинигами рядом не было, так что вариант, по факту, один...");
            dlg.TextAnother("- Так, молчать.", laberius);
            dlg.TextAnother("- Чё то у вас тут какая то фигня происходит, позову остальных...", laberius);
            dlg.TextAnother("- Развели тут... хуякомунды, понимаешь...", laberius);

            dlg.Point(nextPoint);
        }

        /// <summary>
        ///     Если история выполнялась, никогда не выполняем её более 1 раза
        ///     ---
        ///     If story has been run, never run it more than once
        /// </summary>
        public override bool SecondInit()
        {
            door.transform.localRotation = Quaternion.Euler(openAngles);
            return false;
        }

    }
    
}
