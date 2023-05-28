
namespace Engine.Data.Repositories
{

    public class CharSkillsProxy : FileRepositoryProxyBase<SkillsRepositoryObject>
    {

        public override string DirName => "Chars";
        public override string FileName => "Skills";

        public override SkillsRepositoryObject CreateDefault()
        {
            return new Skills().CreateData();
        }

    }

}
