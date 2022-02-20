using Engine.Data;

namespace Engine.Logic.Locations
{

    public class NpcReloadActionContext : NpcBaseActionContext
    {

        public IFirearmsWeapon FirearmsWeapon { get; set; }

        public IItem Ammo { get; set; }

    }

}
