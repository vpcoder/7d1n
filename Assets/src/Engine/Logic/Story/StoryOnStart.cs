namespace Engine.Story
{
    
    public abstract class StoryOnStart : StoryBase
    {
        public override bool IsActive => true;

        public override void Init()
        {
            activeFlag = true;
            base.Init();
            RunDialog();
        }
        
    }
    
}