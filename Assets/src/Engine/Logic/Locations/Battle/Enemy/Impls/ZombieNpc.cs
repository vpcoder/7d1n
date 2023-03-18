using System.Collections.Generic;
using Engine.Data;

namespace Engine.Logic.Locations.Enemy.Impls
{
    
    public class ZombieNpc : EnemyNpcBehaviour
    {

        protected override void UpdateBody()
        {
            LoadPredictors(new Dictionary<NpcStateType, string>()
            {
                { NpcStateType.Unsuspecting,    "basic.human." },
                { NpcStateType.Normal,          "basic.human." },
                { NpcStateType.Nervous,         "basic.human." },
                { NpcStateType.Searching,       "basic.human." },
                { NpcStateType.Fighting,        "basic.human.fighting" },
            });
            base.UpdateBody();
        }
        
        public void LoadPredictors(IDictionary<NpcStateType, string> stateToPredictorName)
        {
            PredictorByState?.Clear();
            PredictorByState = new Dictionary<NpcStateType, IPredictor>();
            foreach (var entry in stateToPredictorName)
                PredictorByState[entry.Key] = NpcAIPredictor.Instance.Get(entry.Value);
        }

    }
    
}
