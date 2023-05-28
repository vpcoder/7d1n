
namespace Engine.Data.Repositories
{

    public class CharQuestProxy : FileRepositoryProxyBase<QuestRepositoryObject>
    {

        public override string DirName => "Chars";
        public override string FileName => "Quest";

        public override QuestRepositoryObject CreateDefault()
        {
            return new Quest().CreateData();
        }

    }

}
