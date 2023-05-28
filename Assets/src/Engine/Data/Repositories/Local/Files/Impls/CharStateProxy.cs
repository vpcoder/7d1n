
namespace Engine.Data.Repositories
{

    public class CharStateProxy : FileRepositoryProxyBase<StateRepositoryObject>
    {

        public override string DirName => "Chars";
        public override string FileName => "State";

        public override StateRepositoryObject CreateDefault()
        {
            return new State().CreateData();
        }

    }

}
