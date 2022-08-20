
namespace Engine.Data.Factories.Xml
{

    public interface INpcInfo : IIdentity
    {
        string BodyName { get; }
        string BehaviourName { get; }
        string SpriteName { get; }
    }

    public class NpcInfo : INpcInfo
    {
        public string BodyName { get; set; }

        public string BehaviourName { get; set; }

        public string SpriteName { get; set; }

        public long ID { get; set; }

        public IIdentity Copy()
        {
            return new NpcInfo()
            {
                ID = ID,
                BodyName = BodyName,
                BehaviourName = BehaviourName,
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
            npcInfo.BehaviourName = Str("Behaviour");
            npcInfo.SpriteName    = Str("Sprite");
            return npcInfo;
        }

    }

}
