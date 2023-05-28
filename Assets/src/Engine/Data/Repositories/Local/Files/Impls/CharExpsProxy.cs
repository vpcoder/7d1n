
namespace Engine.Data.Repositories
{

    public class CharExpsProxy : FileRepositoryProxyBase<ExpDataRepositoryObject>
    {

        public override string DirName => "Chars";
        public override string FileName => "Exps";

        public override ExpDataRepositoryObject CreateDefault()
        {
            return new Exps().CreateData();
        }

    }

}
