
namespace Engine.Data.Stories
{

    public class CharInventoryProxy : FileStoryProxyBase<InventoryStoryObject>
    {

        public override string DirName => "Chars";
        public override string FileName => "Inventory";

        public override InventoryStoryObject CreateDefault()
        {
            return new Inventory().CreateData();
        }

    }

}
