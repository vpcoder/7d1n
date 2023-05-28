
namespace Engine.Data.Repositories
{

    public class CharAccountProxy : FileRepositoryProxyBase<AccountRepositoryObject>
    {

        public override string DirName => "Chars";
        public override string FileName => "Account";

        public override AccountRepositoryObject CreateDefault()
        {
            return new Account().CreateData();
        }

    }

}
