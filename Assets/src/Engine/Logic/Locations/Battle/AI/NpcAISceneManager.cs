using Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Engine.Logic.Locations
{

    public class NpcAISceneManager
    {

        #region Singleton

        private static readonly Lazy<NpcAISceneManager> instance = new Lazy<NpcAISceneManager>(() => new NpcAISceneManager());
        public static NpcAISceneManager Instance { get { return instance.Value; } }
        private NpcAISceneManager() { }

        #endregion

        public IDictionary<OrderGroup, List<CharacterNpcBehaviour>> GroupToNpcList
        {
            get
            {
                var data = new Dictionary<OrderGroup, List<CharacterNpcBehaviour>>();
                foreach (var character in Object.FindObjectsOfType<CharacterNpcBehaviour>())
                {
                    if (character.CharacterContext.Status.IsDead) // Не берём в расчёт мёртвых
                        continue;
                    
                    var group = character.Character.OrderGroup;
                    data.AddInToList(group, character);
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
            var characterInitiative = new Dictionary<OrderGroup, int>();
            foreach(var character in GameObject.FindObjectsOfType<CharacterNpcBehaviour>())
            {
                var info = character.Character;
                int initiative;
                if (!characterInitiative.TryGetValue(info.OrderGroup, out initiative))
                    initiative = 0;

                characterInitiative[info.OrderGroup] = initiative;
            }

            order.AddRange(characterInitiative.Keys);
            
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
