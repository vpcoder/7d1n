using Engine.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations
{

    public class NpcAIPredictor
    {

        #region Singleton

        private static readonly Lazy<NpcAIPredictor> instance = new Lazy<NpcAIPredictor>(() => new NpcAIPredictor());
        public static NpcAIPredictor Instance { get { return instance.Value; } }
        
        private NpcAIPredictor()
        {
            foreach (var predictor in AssembliesHandler.CreateImplementations<IPredictor>())
                stateToPredictor.Add(predictor.State, predictor);
        }

        #endregion

        private IDictionary<NpcStateType, IPredictor> stateToPredictor = new Dictionary<NpcStateType, IPredictor>();
        private BattleManager battle;

        /// <summary>
        ///     Единая точка рассчитывающая стратегию группы AI
        ///     ---
        ///     Single point calculating AI group strategy
        /// </summary>
        public void CreateStrategy()
        {
            this.battle = ObjectFinder.Find<BattleManager>();

            Debug.Log("create strategy...");

            var currentGroup = Game.Instance.Runtime.BattleContext.OrderIndex;
            var allAiItems = NpcAISceneManager.Instance.GroupToNpcList; // Все НПС в своих группах
            var currentNpcList = allAiItems[currentGroup]; // Группа, которая сейчас ходит
            
            battle.EnemyGroupCounter = currentNpcList.Count;

            var context = new PredictorContext();
            context.EnemyGroup = currentGroup;
            context.CurrentGroupNpc = currentNpcList;
            foreach (var npc in currentNpcList)
            {
                context.Npc = npc;
                var npcContext = npc.NpcContext;
                var predictor = TryFindPredictor(npcContext.Status.State);
                predictor.CreateStrategyForNpc(context);
            }
        }

        private IPredictor TryFindPredictor(NpcStateType npcStateType)
        {
            if (!stateToPredictor.TryGetValue(npcStateType, out var predictor))
                throw new NotSupportedException("npc state '" + npcStateType + "' isn't supported!");
            return predictor;
        }
        
    }

}
