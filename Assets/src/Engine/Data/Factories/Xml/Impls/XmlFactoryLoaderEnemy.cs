using System.Linq;

namespace Engine.Data.Factories.Xml
{

    /// <summary>
    /// Загрузчик фабрики врагов
    /// </summary>
    public class XmlFactoryLoaderEnemy : XmlFactoryLoaderBase<IEnemy>
    {

        public XmlFactoryLoaderEnemy()
        {
            FileNames = new[] { "Data/enemies_data" };
        }

        protected override IEnemy ReadItem()
        {
            NPC npc = new NPC();
            npc.ID         = Lng("ID");
            npc.Exp        = Lng("Exp");
            npc.EnemyGroup = Enm<EnemyGroup>("EnemyGroup");
            npc.SpritePath = Str("SpritePath");
            npc.AP         = Int("AP");
            npc.Protection = Int("Protection");
            npc.Health     = Int("Health");

            npc.ItemsMaxCountForGeneration   = Int("ItemsCount");
            npc.WeaponsMaxCountForGeneration = Int("WeaponsCount");

            var items = DblSplt("Items").Select(item => new Part() { ResourceID = long.Parse(item[0]), ResourceCount = long.Parse(item[1]), Difficulty = 0 }).ToList();
            npc.ItemsForGeneration = items;

            var weapons = Splt("Weapons").Select(id => long.Parse(id)).ToList();
            npc.WeaponsForGeneration = weapons;

            return npc;
        }

    }

}
