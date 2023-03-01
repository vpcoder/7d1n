
namespace Engine.Data.Factories.Xml
{

    public interface INpcInfo : IIdentity
    {
        string BodyName { get; }
        string SpriteName { get; }
    }

    public class NpcInfo : INpcInfo
    {
        public string BodyName { get; set; }

        public string SpriteName { get; set; }

        public long ID { get; set; }

        public IIdentity Copy()
        {
            return new NpcInfo()
            {
                ID = ID,
                BodyName = BodyName,
                SpriteName = SpriteName,
            };
        }
    }

    /// <summary>
    /// Загрузчик фабрики врагов
    /// </summary>
    public class XmlFactoryLoaderNpc : XmlFactoryLoaderBase<INpcInfo>
    {

        public XmlFactoryLoaderNpc()
        {
            FileNames = new[] { "Data/npc_data" };
        }

        protected override INpcInfo ReadItem()
        {
            var npcInfo = new NpcInfo();
            npcInfo.ID            = Lng("ID");
            npcInfo.BodyName      = Str("Body");
            npcInfo.SpriteName    = Str("Sprite");
            return npcInfo;
        }

    }

}
