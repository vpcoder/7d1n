using Engine.Data;
using System.Collections.Generic;
using UnityEngine;
using Engine.EGUI;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Менеджер отвечающий за переход в режим битвы и за выход из битвы
    /// </summary>
    public class BattleManager : MonoBehaviour
    {

        [SerializeField] private GameObject damageHintPrefab;
        [SerializeField] private List<EnemyNpcBehaviour> enemies;

        [SerializeField] private BattleActionsController battleActionsController;

        public int EnemyEndStepCounter = 0;
        public int EnemyGroupCounter = 0;
        private object locker = new object();

        public void EnemyStepCompleted(EnemyNpcBehaviour enemy)
        {
            if (Game.Instance.Runtime.BattleContext.OrderIndex != enemy.Character.OrderGroup)
                return;

            lock(locker)
            {
                Debug.Log("enemy '" + enemy.transform.name + "' completed step...");
                EnemyEndStepCounter++;
            }
        }

        public BattleActionsController BattleActions
        {
            get
            {
                return battleActionsController;
            }
        }

        public void ShowDamageHint(string text, Vector3 pos)
        {
            UIHintMessageManager.Show(damageHintPrefab, pos, text);
        }

        /// <summary>
        ///     Вводит врага в битву
        ///     ---
        ///     
        /// </summary>
        /// <param name="enemies">Враги</param>
        public void AddEnemiesToBattle(params EnemyNpcBehaviour[] enemies)
        {
            Debug.Log("add enemies from battle...");
            this.enemies.AddRange(enemies);
        }

        /// <summary>
        ///     Выводит врага из битвы
        ///     ---
        ///     
        /// </summary>
        /// <param name="enemies">Враги</param>
        public void RemoveEnemiesFromBattle(params EnemyNpcBehaviour[] enemies)
        {
            Debug.Log("remove enemies from battle...");

            foreach (var enemy in enemies)
            {
                this.enemies.Remove(enemy);
                foreach(var another in this.enemies)
                {
                	if(another.Target == enemy.GetComponent<IDamagedObject>())
                		another.Target = null; // TODO: Подумать о том как пересчитать стратегию для тех кто еще не потратил ОД, у них свой ход, а цель уже вышла из боя
                }
            }

            if(this.enemies.Count == 0)
                ExitFromBattle();
        }

        /// <summary>
        /// Разворачиваем битву
        /// </summary>
        public void EnterToBattle()
        {
            Debug.Log("starting battle...");

            Game.Instance.Runtime.Mode = Mode.Battle;
            Game.Instance.Runtime.BattleFlag = true;

            Debug.Log("finding enemies...");
            foreach (var entry in NpcAISceneManager.Instance.GroupToNpcList)
                enemies.AddRange(entry.Value);
            Debug.Log("founded " + enemies.Count + " npc bodies");
            
            Debug.Log("creating order...");
            NpcAISceneManager.Instance.SetupOrder(); // Формируем очереди ходов
            Debug.Log("created " + Game.Instance.Runtime.BattleContext.Order.Count + " order groups");
            
            var apController = ObjectFinder.Find<BattleApController>();
            apController.Show();

            if (Game.Instance.Runtime.BattleContext.OrderIndex == OrderGroup.PlayerGroup)
                StartPlayerStep();

            Debug.Log("battle started");
        }

        /// <summary>
        /// Выходим из битвы
        /// </summary>
        public void ExitFromBattle()
        {
            Debug.Log("exit from battle");

            var apController = ObjectFinder.Find<BattleApController>();
            apController.Hide();

            var controller = ObjectFinder.Find<BattleActionsController>();
            controller.Hide();

            var endStepController = ObjectFinder.Find<EndStepController>();
            endStepController.Hide();

            Game.Instance.Runtime.Mode = Mode.Game;
            Game.Instance.Runtime.BattleFlag = false;
        }

        private void Update()
        {
            if (Game.Instance.Runtime.Mode != Mode.Battle) // Не битва
                return;

            if (Game.Instance.Runtime.BattleContext.OrderIndex == OrderGroup.PlayerGroup ||
                Game.Instance.Runtime.BattleContext.OrderIndex == OrderGroup.AnotherPlayerGroup) // ходит игрок или противник-человек, не нужно сюда лезть
                return;

            // Ходят NPC
            if (EnemyEndStepCounter >= EnemyGroupCounter)
            {
                DoNextOrder();
            }
        }

        /// <summary>
        /// Начало хода игрока
        /// </summary>
        public void StartPlayerStep()
        {
            Game.Instance.Runtime.BattleContext.CurrentCharacterAP = Game.Instance.Character.State.MaxAP; // Восстанавливаем ОД
            var endStepController = ObjectFinder.Find<EndStepController>();
            endStepController.Show();
        }

        public void DoNextOrder()
        {
            Debug.Log("next order");

            EnemyEndStepCounter = 0;

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
                StartPlayerStep();

            NpcAISceneManager.Instance.UpdateOrderList(); // Обновляем очереди ходов
            if(Game.Instance.Runtime.BattleContext.Order.Count <= 1)
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
