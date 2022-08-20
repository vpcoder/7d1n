
namespace Engine.Data.Stories
{

    public class CharExpsProxy : FileStoryProxyBase<ExpDataStoryObject>
    {

        public override string DirName => "Chars";
        public override string FileName => "Exps";

        public override ExpDataStoryObject CreateDefault()
        {
            return new Exps().CreateData();
        }

    }

}
