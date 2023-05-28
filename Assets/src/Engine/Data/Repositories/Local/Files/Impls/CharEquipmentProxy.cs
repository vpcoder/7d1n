
namespace Engine.Data.Repositories
{

    public class CharEquipmentProxy : FileRepositoryProxyBase<EquipmentRepositoryObject>
    {

        public override string DirName => "Chars";
        public override string FileName => "Equipment";

        public override EquipmentRepositoryObject CreateDefault()
        {
            return new EquipmentRepositoryObject();
        }

    }

}
