using Engine.Data;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Контекст перезарядки огнестрельного оружия
    /// ---
    /// Context for reloading firearms
    /// 
    /// </summary>
    public class NpcReloadActionContext : NpcBaseActionContext
    {

        /// <summary>
        /// Оружие которое перезаряжают
        /// ---
        /// Weapons that reload
        /// </summary>
        public IFirearmsWeapon FirearmsWeapon { get; set; }

        /// <summary>
        /// Снаряды, которыми перезаряжают оружие (обойма, стрела и т.д.)
        /// Отсюда же берётся количество патронов которое будет в оружии
        /// ---
        /// The shells used to reload the weapon (clip, arrow, etc.)
        /// From this also comes the number of rounds that will be in the weapon
        /// </summary>
        public IItem Ammo { get; set; }

    }

}
