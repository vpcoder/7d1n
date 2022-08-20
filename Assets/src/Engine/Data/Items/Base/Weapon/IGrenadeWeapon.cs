
namespace Engine.Data
{

    /// <summary>
    /// 
    /// Граната, бомба, мина и т.д.
    /// ---
    /// Grenade, bomb, mine, etc.
    /// 
    /// </summary>
    public interface IGrenadeWeapon : IWeapon
    {

        /// <summary>
        /// Радиус взрыва
        /// ---
        /// Explosion radius
        /// </summary>
        float Radius { get; set; }

        /// <summary>
        /// Эффект метания гранаты
        /// ---
        /// Grenade throwing effect
        /// </summary>
        string GrenadeEffectType { get; set; }

        /// <summary>
        /// Эффект взрыва
        /// ---
        /// Explosion effect
        /// </summary>
        string ExplodeEffectType { get; set; }

        /// <summary>
        /// Звук метания гранаты
        /// ---
        /// The sound of a grenade being thrown
        /// </summary>
        string ThrowSoundType { get; set; }

        /// <summary>
        /// Звук взрыва
        /// ---
        /// The sound of an explosion
        /// </summary>
        string ExplodeSoundType { get; set; }

    }

}
