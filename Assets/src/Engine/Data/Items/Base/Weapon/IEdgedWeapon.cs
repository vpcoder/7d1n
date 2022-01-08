
namespace Engine.Data
{

    /// <summary>
    /// 
    /// Холодное оружие, оружие ближнего боя, биты, палки, ножи и т.д.
    /// ---
    /// Cold weapons, melee weapons, bats, sticks, knives, etc.
    /// 
    /// </summary>
    public interface IEdgedWeapon : IWeapon
    {

        /// <summary>
        /// Можно метать во врага
        /// ---
        /// You can throw it at the enemy
        /// </summary>
        bool CanThrow { get; set; }

        /// <summary>
        /// Сколько стоит ОД для метания
        /// ---
        /// How much is the AP for throwing
        /// </summary>
        int ThrowAP { get; set; }

        /// <summary>
        /// Наносимый урон от метания
        /// ---
        /// Damage inflicted by throwing
        /// </summary>
        long ThrowDamage { get; set; }

        /// <summary>
        /// Дистанция метания в игровых метрах
        /// ---
        /// Throwing distance in game meters
        /// </summary>
        long ThrowDistance { get; set; }

        /// <summary>
        /// Как выглядит снаряд при метании
        /// ---
        /// What the projectile looks like when thrown
        /// </summary>
        string ThrowEffectType { get; set; }

        /// <summary>
        /// Звук метания
        /// ---
        /// Throwing sound
        /// </summary>
        string ThrowSoundType { get; set; }

        /// <summary>
        /// Звук попадания в тело
        /// ---
        /// The sound of hitting the body
        /// </summary>
        string ThrowInSoundType { get; set; }

        /// <summary>
        /// Звук промаха, попадания в стену или объект
        /// ---
        /// Sound of missing, hitting a wall or object
        /// </summary>
        string ThrowOutSoundType { get; set; }

    }

}
