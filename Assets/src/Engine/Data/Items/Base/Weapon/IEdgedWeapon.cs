
namespace Engine.Data
{

    /// <summary>
    /// Холодное оружие
    /// </summary>
    public interface IEdgedWeapon : IWeapon
    {

        /// <summary>
        /// Можно метать
        /// </summary>
        bool CanThrow { get; set; }

        /// <summary>
        /// Сколько стоит ОД для метания
        /// </summary>
        int ThrowAP { get; set; }

        /// <summary>
        /// Урон от метания
        /// </summary>
        long ThrowDamage { get; set; }

        /// <summary>
        /// Дистанция метания
        /// </summary>
        long ThrowDistance { get; set; }
        
        /// <summary>
        /// Как выглядит снаряд при метании
        /// </summary>
        string ThrowEffectType { get; set; }

        /// <summary>
        /// Звук метания
        /// </summary>
        string ThrowSoundType { get; set; }

        /// <summary>
        /// Звук попадания
        /// </summary>
        string ThrowInSoundType { get; set; }

        /// <summary>
        /// Звук промаха
        /// </summary>
        string ThrowOutSoundType { get; set; }

    }

}
