using Engine.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Предиктор, выполняющий рассчёт стратегий поведения NPC в разных ситуациях
    /// ---
    /// Predictor that calculates strategies for NPC behavior in different situations
    /// 
    /// </summary>
    public class NpcAIPredictor
    {

        #region Singleton

        private static readonly Lazy<NpcAIPredictor> instance = new Lazy<NpcAIPredictor>(() => new NpcAIPredictor());
        public static NpcAIPredictor Instance { get { return instance.Value; } }
        
        private NpcAIPredictor()
        {
            foreach (var predictor in AssembliesHandler.CreateImplementations<IPredictor>())
            {
                if (predictorsByName.ContainsKey(predictor.Name))
                {
                    Debug.LogError("the predictor name '" + predictor.Name + "' must be unique!");
                    throw new ArgumentException("predictor name '" + predictor.Name +"' isn't unique");
                }
                predictorsByName.Add(predictor.Name, predictor);
            }
        }

        #endregion

        private IDictionary<string, IPredictor> predictorsByName = new Dictionary<string, IPredictor>();

        /// <summary>
        ///     Находит предиктора по его уникальному наименованию
        ///     ---
        ///     Finds a predictor by its unique name
        /// </summary>
        /// <param name="name">
        ///     Наименование предиктора для AI
        ///     ---
        ///     Predictor name for AI
        /// </param>
        /// <returns>
        ///     Найденный предиктор по наименованию,
        ///     null - если ничего не удалось найти
        ///     ---
        ///     Found predictor by name,
        ///     null - if nothing could be found
        /// </returns>
        public IPredictor Get(string name)
        {
            if (predictorsByName.TryGetValue(name, out var predictor))
                return predictor;
            
            Debug.LogWarning("predictor '" + name + "' is empty");
            return null;
        }
        
        /// <summary>
        ///     Единая точка рассчитывающая стратегию группы AI
        ///     ---
        ///     Single point calculating AI group strategy
        /// </summary>
        public void CreateStrategyForAllNpc()
        {
            BattleManager battle = ObjectFinder.Find<BattleManager>();

            Debug.Log("create strategy...");

            var currentGroup = Game.Instance.Runtime.BattleContext.OrderIndex;
            var allAiItems = NpcAISceneManager.Instance.GroupToNpcList; // Все НПС в своих группах
            var currentNpcList = allAiItems[currentGroup]; // Группа, которая сейчас ходит

            battle.EnemyGroupCounter = currentNpcList.Count;

            var context = new PredictorContext();
            context.OrderGroup = currentGroup;
            context.CurrentGroupNpc = currentNpcList;
            
            foreach (var npc in currentNpcList)
            {
                context.Npc = npc;
                var predictor = npc.TryFindPredictor();
                predictor.CreateStrategyForNpc(context);
            }
        }
        
    }

}
