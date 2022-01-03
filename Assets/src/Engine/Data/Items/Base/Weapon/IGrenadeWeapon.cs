
namespace Engine.Data
{

    /// <summary>
    /// Граната
    /// </summary>
    public interface IGrenadeWeapon : IWeapon
    {

        /// <summary>
        /// Радиус взрыва
        /// </summary>
        float Radius { get; set; }

        /// <summary>
        /// Эффект метания гранаты
        /// </summary>
        string GrenadeEffectType { get; set; }

        /// <summary>
        /// Эффект взрыва
        /// </summary>
        string ExplodeEffectType { get; set; }

        /// <summary>
        /// Звук метания гранаты
        /// </summary>
        string ThrowSoundType { get; set; }

        /// <summary>
        /// Звук взрыва
        /// </summary>
        string ExplodeSoundType { get; set; }

    }

}
