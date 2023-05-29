using Engine.Data;
using System.Collections.Generic;
using UnityEngine;
using Engine.EGUI;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Менеджер отвечающий за переход в режим битвы и за выход из битвы
    /// ---
    /// Manager responsible for entering battle mode and for quitting the battle
    /// 
    /// </summary>
    public class BattleManager : MonoBehaviour
    {

        /// <summary>
        ///     Персонажи, которые учавствуют в битве
        ///     ---
        ///     Characters who participate in the battle
        /// </summary>
        [SerializeField] private List<CharacterNpcBehaviour> characters;

        public int NpcEndTurnCounter { get; set; } = 0;
        public int NpcGroupCounter { get; set; } = 0;
        private object locker = new object();
        
        /// <summary>
        ///     Контроллер совершения действий игроком в свой ход
        ///     ---
        ///     Controller of the player's actions in his turn
        /// </summary>
        [SerializeField] private BattleActionsController battleActionsController;
        public BattleActionsController BattleActions
        {
            get
            {
                return battleActionsController;
            }
        }

        /// <summary>
        ///     Вызывается когда NPC заканчивает свой ход
        ///     ---
        ///     Called when an NPC finishes his turn
        /// </summary>
        /// <param name="npc">
        ///     NPC который завершил свой ход
        ///     ---
        ///     NPC who has completed his turn
        /// </param>
        public void NpcTurnCompleted(CharacterNpcBehaviour npc)
        {
            if (Game.Instance.Runtime.BattleContext.OrderIndex != npc.Character.OrderGroup)
                return;

            lock(locker)
            {
#if UNITY_EDITOR && BATTLE_DEBUG
                Debug.Log("character '" + npc.transform.name + "' completed turn...");
#endif
                NpcEndTurnCounter++;
            }
        }
        
        /// <summary>
        ///     Вводит персонажей в битву
        ///     ---
        ///     Introduces the characters to the battle
        /// </summary>
        /// <param name="npcs">
        ///     Персонажи, которые были вовлечены в битву
        ///     ---
        ///     Characters who were involved in the battle
        /// </param>
        public void AddEnemiesToBattle(params CharacterNpcBehaviour[] npcs)
        {
            Debug.Log("add enemies from battle...");
            this.characters.AddRange(npcs);
        }

        /// <summary>
        ///     Выводит врага из битвы
        ///     ---
        ///     
        /// </summary>
        /// <param name="enemies">Враги</param>
        public void RemoveEnemiesFromBattle(params CharacterNpcBehaviour[] enemies)
        {
            Debug.Log("remove enemies from battle...");

            foreach (var character in enemies)
            {
                this.characters.Remove(character);
                foreach(var another in this.characters)
                {
                	if(another.Target == character.GetComponent<IDamagedObject>())
                		another.Target = null; // TODO: Подумать о том как пересчитать стратегию для тех кто еще не потратил ОД, у них свой ход, а цель уже вышла из боя
                }
            }

            if(IsNeedExitBattle())
                ExitFromBattle();
        }

        /// <summary>
        ///     Определяет, нужно ли прервать битву?
        ///     ---
        ///     Determines whether the battle should be cut short?
        /// </summary>
        /// <returns>
        ///     Если никого, вовлечённого в битву кроме игрока не осталось,
        ///     или, если все оставшиеся вовлечённые в битву сущности не конфликтуют между собой - вернёт true,
        ///     в остальных случаях - вернёт false
        ///     ---
        ///     If there are no other entities involved in the battle besides the player,
        ///     or if all remaining entities involved in the battle don't conflict with each other - it returns true,
        ///     in other cases - will return false
        /// </returns>
        public bool IsNeedExitBattle()
        {
            var ctx = Game.Instance.Runtime.BattleContext;
            if (Lists.IsEmpty(characters) || ctx.Order.Count <= 1)
                return true;

            foreach (var e1 in characters)
            {
                foreach (var e2 in characters)
                {
                    if(e1 == e2)
                        break;

                    // Если есть враждующие группы, битва не окончена!
                    // If there are opposing groups, the battle is not over!
                    if (ctx.Relations.IsEnemies(e1.Character.OrderGroup, e2.Character.OrderGroup))
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        ///     Все вокруг входят в пошаговый режим, разворачивается битва.
        ///     ---
        ///     All around enter turn-based mode, the battle unfolds.
        /// </summary>
        public void EnterToBattle()
        {
            Debug.Log("starting battle...");

            Game.Instance.Runtime.Mode = Mode.Battle;
            Game.Instance.Runtime.BattleFlag = true;

            Debug.Log("finding enemies...");
            foreach (var entry in NpcAISceneManager.Instance.GroupToNpcList)
                characters.AddRange(entry.Value);
            Debug.Log("founded " + characters.Count + " npc bodies");
            
            Debug.Log("creating order...");
            NpcAISceneManager.Instance.SetupOrder(); // Формируем очереди ходов
            Debug.Log("created " + Game.Instance.Runtime.BattleContext.Order.Count + " order groups");
            
            var apController = ObjectFinder.Find<BattleApController>();
            apController.Show();

            if (Game.Instance.Runtime.BattleContext.OrderIndex == OrderGroup.PlayerGroup)
                StartPlayerTurn();

            Debug.Log("battle started");
        }

        /// <summary>
        ///     Выходим из битвы.
        ///     Завершается битва, все меняют своё состояние на нормальное.
        ///     ---
        ///     Exit the battle.
        ///     The battle ends, everyone changes their state to normal.
        /// </summary>
        public void ExitFromBattle()
        {
            Debug.Log("exit from battle");

            // Закрываем интерфейсы битвы
            // Closing the battle interfaces
            ObjectFinder.Find<BattleApController>().Hide();
            ObjectFinder.Find<BattleActionsController>().Hide();
            ObjectFinder.Find<EndTurnController>().Hide();

            // Сброс режима битвы
            // Resetting battle mode
            Game.Instance.Runtime.Mode = Mode.Game;
            Game.Instance.Runtime.BattleFlag = false;

            // Всех уцелевших возвращаем в нормальное состояние
            // All survivors return to normal
            foreach (var character in characters)
            {
                character.CharacterContext.Status.State = CharacterStateType.Normal;
            }
            
            // Сбрасываем список участников битвы
            characters.Clear();
        }

        private void Update()
        {
            // Если нет битвы, нет смысла что то анализировать
            // If there is no battle, there is no point in analyzing anything.
            if (Game.Instance.Runtime.Mode != Mode.Battle) 
                return;

            // Если сейчас ходит игрок или противник-человек, не нужно сюда лезть, люди сами разберутся когда они закончат свой ход
            // If a human player or opponent moves now, there's no need to get involved, people will figure it out themselves when they finish their turn
            if (Game.Instance.Runtime.BattleContext.OrderIndex == OrderGroup.PlayerGroup ||
                Game.Instance.Runtime.BattleContext.OrderIndex == OrderGroup.AnotherPlayerGroup)
                return;

            // Если ещё остались персонажи, которые не завершили свой ход
            // If there are still characters who have not completed their turn
            if (NpcEndTurnCounter < NpcGroupCounter)
                return;
            
            // Все персонажи в рамках текущей группы хода завершили свой ход, необходимо передать ход следующей группе
            // All characters within the current turn group have completed their turn, it is necessary to pass the turn to the next group
            DoNextGroupTurn();
        }

        /// <summary>
        ///     Начало хода игрока
        ///     ---
        ///     Beginning of a player's turn
        /// </summary>
        public void StartPlayerTurn()
        {
            // Восстанавливаем ОД, так как начинаем новый ход
            // Restore the AP, as we begin a new turn
            Game.Instance.Runtime.BattleContext.CurrentCharacterAP = Game.Instance.Character.State.MaxAP;
            
            // Отображаем интерфейс окончания хода
            // Displaying the turn end interface
            var endTurnController = ObjectFinder.Find<EndTurnController>();
            endTurnController.Show();
        }

        public void DoNextGroupTurn()
        {
            Debug.Log("next group turn");

            NpcEndTurnCounter = 0;

            var hands = ObjectFinder.Find<HandsController>();
            hands.DoResetSelectedCell(hands.Selected);

            var order = Game.Instance.Runtime.BattleContext.Order;
            if (order == null || order.Count == 1)
                return;

            var current = Game.Instance.Runtime.BattleContext.OrderIndex;
            var index   = order.IndexOf(current);

            if (++index >= order.Count)
                index = 0;

            Debug.Log(index);

            Game.Instance.Runtime.BattleContext.OrderIndex = order[index];
            if(Game.Instance.Runtime.BattleContext.OrderIndex == OrderGroup.PlayerGroup) // Ходит игрок
                StartPlayerTurn();

            NpcAISceneManager.Instance.UpdateOrderList(); // Обновляем очереди ходов
            if(IsNeedExitBattle())
            {
                ExitFromBattle();
                return;
            }

            if (Game.Instance.Runtime.BattleContext.OrderIndex != OrderGroup.PlayerGroup && Game.Instance.Runtime.BattleContext.OrderIndex != OrderGroup.AnotherPlayerGroup)
			{
                NpcAIPredictor.Instance.CreateStrategyForAllNpc();
			}       
        }

    }

}
