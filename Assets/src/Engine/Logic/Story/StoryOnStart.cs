namespace Engine.Story
{
    
    public abstract class StoryOnStart : StoryBase
    {

        public override void Init()
        {
            base.Init();
            RunDialog();
        }
        
    }
    
}