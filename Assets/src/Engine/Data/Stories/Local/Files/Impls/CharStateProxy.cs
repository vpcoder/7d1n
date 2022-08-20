
namespace Engine.Data.Stories
{

    public class CharStateProxy : FileStoryProxyBase<StateStoryObject>
    {

        public override string DirName => "Chars";
        public override string FileName => "State";

        public override StateStoryObject CreateDefault()
        {
            return new State().CreateData();
        }

    }

}
