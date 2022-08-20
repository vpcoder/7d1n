
namespace Engine.Data.Stories
{

    public class CharEquipmentProxy : FileStoryProxyBase<EquipmentStoryObject>
    {

        public override string DirName => "Chars";
        public override string FileName => "Equipment";

        public override EquipmentStoryObject CreateDefault()
        {
            return new EquipmentStoryObject();
        }

    }

}
