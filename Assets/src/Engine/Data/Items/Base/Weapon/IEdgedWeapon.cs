
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
        ///     Радиус нанесения урона
        ///     В указанной сфере от оружия будет рассчитываться достало ли оно по цели или нет
        ///     ---
        ///     The damage radius
        ///     In the specified area from the weapon will be calculated whether it has reached the target or not
        /// </summary>
        float DamageRadius { get; set; }

        /// <summary>
        ///     Можно метать во врага
        ///     ---
        ///     You can throw it at the character
        /// </summary>
        bool CanThrow { get; set; }

        /// <summary>
        ///     Сколько стоит ОД для метания
        ///     ---
        ///     How much is the AP for throwing
        /// </summary>
        int ThrowAP { get; set; }

        /// <summary>
        ///     Наносимый урон от метания
        ///     ---
        ///     Damage inflicted by throwing
        /// </summary>
        int ThrowDamage { get; set; }

        /// <summary>
        ///     Дистанция метания в игровых метрах
        ///     ---
        ///     Throwing distance in game meters
        /// </summary>
        float ThrowDistance { get; set; }

        /// <summary>
        ///     Дистанция прицеливания при метании ножа
        ///     ---
        ///     Aiming distance when throwing a edged
        /// </summary>
        float ThrowAimRadius { get; set; }

        /// <summary>
        ///     Как выглядит снаряд при метании
        ///     ---
        ///     What the projectile looks like when thrown
        /// </summary>
        string ThrowBulletObject { get; set; }

        /// <summary>
        ///     Звук метания
        ///     ---
        ///     Throwing sound
        /// </summary>
        string ThrowSound { get; set; }

        /// <summary>
        ///     Звук попадания в тело
        ///     ---
        ///     The sound of hitting the body
        /// </summary>
        string ThrowHitSound { get; set; }

        /// <summary>
        ///     Звук промаха, попадания в стену или объект
        ///     ---
        ///     Sound of missing, hitting a wall or object
        /// </summary>
        string ThrowMissSound { get; set; }

    }

}
