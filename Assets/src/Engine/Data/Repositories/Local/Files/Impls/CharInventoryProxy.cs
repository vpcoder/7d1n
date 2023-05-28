
namespace Engine.Data.Repositories
{

    public class CharInventoryProxy : FileRepositoryProxyBase<InventoryRepositoryObject>
    {

        public override string DirName => "Chars";
        public override string FileName => "Inventory";

        public override InventoryRepositoryObject CreateDefault()
        {
            return new Inventory().CreateData();
        }

    }

}
