using Engine.Collections;
using System;

namespace Engine.DB
{

    public class PlayerFactory : DbCollection<Player>
    {

        private static readonly Lazy<PlayerFactory> instance = new Lazy<PlayerFactory>(() => new PlayerFactory());
        public static PlayerFactory Instance { get { return instance.Value; } }

    }

}
