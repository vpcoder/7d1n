using Engine.Data.Factories;

namespace Engine.Story
{
    
    public abstract class StorySelectCatcherBase : SelectCatcherBase
    {
        
        protected override string HintID { get; } = EffectFactory.QUEST_HINT;

    }
    
}