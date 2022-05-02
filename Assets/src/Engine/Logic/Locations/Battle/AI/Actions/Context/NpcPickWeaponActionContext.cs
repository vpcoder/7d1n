using Engine.Data;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Контекст переключения оружия в руках NPC
    /// ---
    /// Context of switching weapons in the hands of an NPC
    /// 
    /// </summary>
    public class NpcPickWeaponActionContext : NpcBaseActionContext
    {

        /// <summary>
        /// Оружие которое берёт в руки NPC
        /// ---
        /// Weapons that the NPC picks up
        /// </summary>
        public IWeapon Weapon { get; set; }

    }

}
