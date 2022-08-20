using System;
using System.Collections.Generic;


namespace Engine.Data
{

    public class PlayerCharacter : IEnemy
    {
        public string SpritePath => throw new NotImplementedException();

        public int AP { get { return Game.Instance.Runtime.BattleContext.CurrentCharacterAP; } set { } }
        public EnemyGroup EnemyGroup { get; set; } = EnemyGroup.PlayerGroup;
        public long Exp { get { return 150; } set { } }
        public int Health { get { return Game.Instance.Character.State.Health; } set { Game.Instance.Character.State.Health = value; } }
        public int Protection { get { return Game.Instance.Character.State.Protection; } set { Game.Instance.Character.State.Protection = value; } }
        public List<IWeapon> Weapons { get { return null; } set { } }
        public List<IItem> Items { get { return null; } set { } }
        public List<long> WeaponsForGeneration { get { return null; } set { } }
        public int WeaponsMaxCountForGeneration { get { return 0; } set { } }
        public List<ResourcePair> ItemsForGeneration { get { return null; } set { } }
        public int ItemsMaxCountForGeneration { get { return 0; } set { } }
        public long ID { get { return Game.Instance.Character.Account.SpriteID; } set { } }

        public IIdentity Copy()
        {
            throw new NotSupportedException();
        }

    }

}
