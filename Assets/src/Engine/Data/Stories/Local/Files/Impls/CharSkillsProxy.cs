
namespace Engine.Data.Stories
{

    public class CharSkillsProxy : FileStoryProxyBase<SkillsStoryObject>
    {

        public override string DirName => "Chars";
        public override string FileName => "Skills";

        public override SkillsStoryObject CreateDefault()
        {
            return new Skills().CreateData();
        }

    }

}
