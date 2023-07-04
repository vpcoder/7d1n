using System.Collections.Generic;
using Engine.Data;
using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic;
using Engine.Logic.Dialog;
using Engine.Logic.Dialog.Action.Impls;
using Engine.Logic.Locations;
using Engine.Logic.Locations.Animation;
using Engine.Logic.Map;
using UnityEngine;

namespace Engine.Story.Chagegrad
{
    
    public class CreateCharacterStoryCatcher : StoryBase
    {
        public override string StoryID => "main.chagegrad1.create_character";

        [SerializeField] private CharacterMeshSwitcher meshSwitch;
        [SerializeField] private CharacterNpcBehaviour zombie;

        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform startPoint2;
        [SerializeField] private Transform characterEyes;
        [SerializeField] private Transform characterBody;
        
        [SerializeField] private GameObject selectCharacterInterface;
        [SerializeField] private GameObject changeCharacterNameInterface;

        public override void Init()
        {
            ObjectFinder.Find<LocationCameraController>().UpdateCameraPos();
            base.Init();
        }
        
        private int startFloor;
        private float startFov;
        private TransformPair startTransformPair;

        public override void CreateDialog(DialogQueue dlg)
        {
            var background = ObjectFinder.SceneViewImage;
            var camera = Camera.main;

            var startSelectBodyPoint = SelectVariant.Point;
            var completeSelectBodyPoint = SelectVariant.Point;
            var list1 = new List<SelectVariant>
            {
                SelectVariant.New("Ещё нет", startSelectBodyPoint),
                SelectVariant.New("Выбрал", completeSelectBodyPoint),
            };
            var startChangeNamePoint = SelectVariant.Point;
            var completeChangeNamePoint = SelectVariant.Point;
            var list2 = new List<SelectVariant>
            {
                SelectVariant.New("Ещё нет", startChangeNamePoint),
                SelectVariant.New("Написал", completeChangeNamePoint),
            };
            
            dlg.Run(() =>
            {
                // Начинаем первый квест
                // Beginning the first quest
                var quest = QuestFactory.Instance.Get<ChagegradStartQuest>();
                quest.Start();
                
                // Фейково "убиваем" зомби-девушку, чтобы лежала на кровати, "как будто умерла"
                // Fake "kill" a zombie girl to lie on the bed "as if dead"
                zombie.Animator.SetCharacterDeadType(DeatType.FakeDead);
                zombie.Damaged.CanTakeDamage = false;
                camera.SetState(startPoint, characterEyes.transform);
                background.color = Color.white;
            });

            dlg.Music("dead_lain");
            
            dlg.Text("[Нажми здесь, чтобы продолжить]");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, Color.white, Color.clear, 0.8f));
            });
            dlg.Text("[Перед тобой персонаж]");
            dlg.Text("[Он изрядно побит, довольно вонюч, а ещё у него очень плохой характер]");
            
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.LookAtAndMove(camera, characterBody.transform, startPoint2.position));
            });

            dlg.Text("[Возможно всё это никуда не годится]");
            dlg.Text("[Дам тебе выбор]");
            dlg.Run(() =>
            {
                selectCharacterInterface.SetActive(true);
            });
            dlg.Text("[Листай влево или вправо, чтобы определиться с внешним видом]");
            dlg.Point(startSelectBodyPoint);
            dlg.Text("[Выбрал?]");
            dlg.Select(list1, "[Точно?]");
            
            dlg.Point(completeSelectBodyPoint);
            dlg.Run(() =>
            {
                selectCharacterInterface.Destroy();
            });
            dlg.Text("[Хорошо, давай определимся с тем как его зовут]");
            
            dlg.Text("[Придумай ему какое-нибудь имя]");
            dlg.Run(() =>
            {
                changeCharacterNameInterface.SetActive(true);
            });
            dlg.Text("[Напиши что нибудь унизительное]");
            dlg.Point(startChangeNamePoint);
            dlg.Text("[Написал?]");
            dlg.Set("characterName", () => Game.Instance.Character.Account.Name);
            dlg.Select(list2, "['<color=\"green\">${characterName}</color>'? Точно?]");
            dlg.Text("[" + Game.Instance.Character.Account.Name + "? Отлично. Не знаю за что ты так не любишь этого персонажа, раз выбрал такое имя...]");
            
            dlg.Set("characterName", () => Game.Instance.Character.Account.Name);
            dlg.Point(completeChangeNamePoint);
            dlg.Run(() =>
            {
                changeCharacterNameInterface.Destroy();
            });

            dlg.Text("[Так, определились с внешностью]");
            dlg.Text("[Выбрали имя <color=\"green\">${characterName}</color>]");
            dlg.Text("[Пора начинать]");
            dlg.Run(() => AudioController.Instance.StopMusic());
            dlg.Text("[Кхм...]");
            dlg.Text("[Приготовились...]");
            
            dlg.Run(() =>
            {
                Game.Instance.Character.Account.SpriteID = meshSwitch.MeshIndex;
                // Не добавляем RuntimeObjectList, чтобы скрипт доиграл до конца гарантированно
                // Do not add RuntimeObjectList, so that the script is guaranteed to finish
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, Color.clear, Color.white, 0.8f));
            });
            
            dlg.Delay(2f);
        }

        /// <summary>
        ///     Если история выполнялась, никогда не выполняем её более 1 раза
        ///     ---
        ///     If story has been run, never run it more than once
        /// </summary>
        public override bool SecondInit()
        {
            var quest = QuestFactory.Instance.Get<ChagegradStartQuest>();
            if (zombie != null && quest.ContainsTag(ChagegradStartQuest.CheckPointKillZombie))
            {
                zombie.Animator.SetCharacterDeadType(DeatType.Dead);
                zombie.Agent.enabled = true;
            }
            else
            {
                if (WakeUpZombieStoryCatcher.Condition())
                {
                    AudioController.Instance.PlayMusic("mortal_kombat");
                    
                    var blinker = ObjectFinder.Find<WTLedBlinker>();
                    if(blinker != null)
                        blinker.Blink = true;
                    zombie.Animator.SetCharacterDeadType(DeatType.Alive);
                    zombie.Damaged.CanTakeDamage = true;
                    zombie.CharacterContext.Status.State = CharacterStateType.Fighting;
                    zombie.DeadEvent += () =>
                    {
                        var quest = QuestFactory.Instance.Get<ChagegradStartQuest>();
                        quest.AddTag(ChagegradStartQuest.CheckPointKillZombie);
                        quest.Stage = 1;
                    
                        var story = ObjectFinder.Find<WTOffStoryCatcher>();
                        if (story != null)
                            story.SetActiveAndSave();
                    };
                    zombie.Agent.enabled = true;

                    ObjectFinder.BattleManager.EnterToBattle();
                }
                else
                {
                    // Фейково "убиваем" зомби-девушку, чтобы лежала на кровати, "как будто умерла"
                    // Fake "kill" a zombie girl to lie on the bed "as if dead"
                    zombie.Animator.SetCharacterDeadType(DeatType.FakeDead);
                    zombie.Damaged.CanTakeDamage = false;
                }
            }
            
            if(quest.NotContainsTag(ChagegradStartQuest.CheckPointCharacterWakeup))
                ObjectFinder.Find<StartInTheBedStoryCatcher>().RunDialog();
            
            return false;
        }
        
    }
    
}