
namespace Engine.Data.Stories
{

    public class CharAccountProxy : FileStoryProxyBase<AccountStoryObject>
    {

        public override string DirName => "Chars";
        public override string FileName => "Account";

        public override AccountStoryObject CreateDefault()
        {
            return new Account().CreateData();
        }

    }

}
