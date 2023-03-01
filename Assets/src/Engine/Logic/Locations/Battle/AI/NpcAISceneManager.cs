using Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Engine.Logic.Locations
{

    public class NpcAISceneManager
    {

        #region Singleton

        private static readonly Lazy<NpcAISceneManager> instance = new Lazy<NpcAISceneManager>(() => new NpcAISceneManager());
        public static NpcAISceneManager Instance { get { return instance.Value; } }
        private NpcAISceneManager() { }

        #endregion

        public IDictionary<OrderGroup, List<EnemyNpcBehaviour>> GroupToNpcList
        {
            get
            {
                var data = new Dictionary<OrderGroup, List<EnemyNpcBehaviour>>();
                foreach (var enemy in GameObject.FindObjectsOfType<EnemyNpcBehaviour>())
                {
                    if (enemy.NpcContext.Status.IsDead) // Не берём в расчёт мёртвых
                        continue;
                    
                    var group = enemy.Character.OrderGroup;
                    data.AddInToList(group, enemy);
                }
                return data;
            }
        }

        /// <summary>
        ///     Перед битвой определяет всех участников битвы, и формирует очередь групп-ходов
        ///     ---
        ///     Before the battle determines all the participants of the battle, and forms a queue of group moves
        /// </summary>
        /// <returns>
        ///     Группы ходов в нужной очередности
        ///     ---
        ///     Groups of moves in the right order
        /// </returns>
        public List<OrderGroup> CreateOrderList()
        {
            Debug.Log("battle create order...");

            var order = new List<OrderGroup>();
            var enemyInitiative = new Dictionary<OrderGroup, int>();
            foreach(var enemy in GameObject.FindObjectsOfType<EnemyNpcBehaviour>())
            {
                var info = enemy.Character;
                int initiative;
                if (!enemyInitiative.TryGetValue(info.OrderGroup, out initiative))
                    initiative = 0;

                enemyInitiative[info.OrderGroup] = initiative;
            }

            order.AddRange(enemyInitiative.Keys);
            
            return order;
        }

        /// <summary>
        ///     Обновляет текущую очередь ходов, исключая группы, которых уже в бою нет
        ///     ---
        ///     Updates the current turn queue, excluding groups that are no longer in combat
        /// </summary>
        public void UpdateOrderList()
        {
            Debug.Log("update order...");

            var order = Game.Instance.Runtime.BattleContext.Order;
            var newOrder = CreateOrderList();
            foreach(var index in order.ToList())
                if (!newOrder.Contains(index))
                    order.Remove(index);

            var current = Game.Instance.Runtime.BattleContext.OrderIndex;
            for (; ; )
            {
                if (order.Contains(current)) // Всё впорядке?
                    break;
                // Текущая группа хода уже не существует, меняем её
                var index = order.IndexOf(current);
                if (index++ >= order.Count)
                    index = 0;
                current = order[index];
            }
            Game.Instance.Runtime.BattleContext.OrderIndex = current;
        }

        /// <summary>
        ///     Инициализирует очередь ходов перед началом битвы
        ///     ---
        ///     Initializes the turn queue before the battle begins
        /// </summary>
        public void SetupOrder()
        {
            Game.Instance.Runtime.BattleContext.CurrentCharacterAP = 0;
            Game.Instance.Runtime.BattleContext.SetOrder(CreateOrderList());
        }

    }

}
