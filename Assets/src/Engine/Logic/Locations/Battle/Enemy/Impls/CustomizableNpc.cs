using System.Collections.Generic;
using Engine.Data;
using UnityEngine;

namespace Engine.Logic.Locations.Char.Impls
{
    
    public class CustomizableNpc : CharacterNpcBehaviour
    {

        [SerializeField] private string unsuspectingPredictorID = "basic.human.";
        [SerializeField] private string normalPredictorID = "basic.human.";
        [SerializeField] private string nervousPredictorID = "basic.human.";
        [SerializeField] private string searchingPredictorID = "basic.human.";
        [SerializeField] private string fightingPredictorID = "basic.human.fighting";

        protected override void UpdateBody()
        {
            PredictorByState?.Clear();
            PredictorByState = new Dictionary<CharacterStateType, IPredictor>();
            
            PredictorByState[CharacterStateType.Unsuspecting] = NpcAIPredictor.Instance.Get(unsuspectingPredictorID);
            PredictorByState[CharacterStateType.Normal]       = NpcAIPredictor.Instance.Get(normalPredictorID);
            PredictorByState[CharacterStateType.Nervous]      = NpcAIPredictor.Instance.Get(nervousPredictorID);
            PredictorByState[CharacterStateType.Searching]    = NpcAIPredictor.Instance.Get(searchingPredictorID);
            PredictorByState[CharacterStateType.Fighting]     = NpcAIPredictor.Instance.Get(fightingPredictorID);

            base.UpdateBody();
        }

    }
    
}
