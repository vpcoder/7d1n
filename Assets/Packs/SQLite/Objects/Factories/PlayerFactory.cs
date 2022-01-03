using Engine.Collections;

namespace Engine.DB
{

    public class PlayerFactory : DbCollection<Player>
    {

        private static readonly Lazy<PlayerFactory> instance = new Lazy<PlayerFactory>(() => new PlayerFactory());
        public static PlayerFactory Instance { get { return instance.Value; } }

    }

}
