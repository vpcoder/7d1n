using Engine.Data;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Контекст атаки.
    /// Содержит информацию для соверешния атаки.
    /// ---
    /// Attack context.
    /// Contains information for the attack context.
    /// 
    /// </summary>
    public class NpcAttackActionContext : NpcBaseActionContext
    {

        /// <summary>
        /// Оружие, которым происходит атака
        /// ---
        /// Weapon used in the attack
        /// </summary>
        public IWeapon Weapon { get; set; }

    }

}
