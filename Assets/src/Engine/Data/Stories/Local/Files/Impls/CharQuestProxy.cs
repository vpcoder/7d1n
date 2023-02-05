
namespace Engine.Data.Stories
{

    public class CharQuestProxy : FileStoryProxyBase<QuestStoryObject>
    {

        public override string DirName => "Chars";
        public override string FileName => "Quest";

        public override QuestStoryObject CreateDefault()
        {
            return new Quest().CreateData();
        }

    }

}
