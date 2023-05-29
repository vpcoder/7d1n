using Engine.Data;
using Engine.Data.Factories;
using Engine.EGUI;
using Engine.Logic;
using Engine.Logic.Locations;
using Engine.Logic.Locations.Objects;
using UnityEngine;

namespace Engine
{

    /// <summary>
    /// 
    /// Сервис расчётов битвы
    /// ---
    /// Battle Calculation Service
    /// 
    /// </summary>
    public class BattleCalculationService
    {

#pragma warning disable IDE0060

        /// <summary>
        ///     Атака дальнего боя
        ///     ---
        ///     Ranged Attack
        /// </summary>
        /// <param name="source">
        ///     Кто атакует
        ///     ---
        ///     Who is attacking
        /// </param>
        public static void DoFirearmsAttack(IAttackObject source)
        {
            var firearms = (IFirearmsWeapon)source.Weapon;
            var bulletPrefab = BulletEffectFactory.Instance.Get(firearms.AmmoEffectType);
            var bullet = Object.Instantiate(bulletPrefab);
            var bulletItem = bullet.GetComponent<BulletItem>();

            var firearmsBehaviour = source.WeaponObject?.GetComponent<IFirearmsBehaviour>();
            var shotPos = firearmsBehaviour == null ? source.AttackCharacterObject.transform.position : firearmsBehaviour.ShotPosition;

            var targetPos = source.TargetAttackPos;
            targetPos.y = shotPos.y;

            bulletItem.Init(source, shotPos, targetPos, firearms);
        }

        /// <summary>
        ///     Метаем нож
        /// </summary>
        /// <param name="source">
        ///     Кто атакует
        ///     ---
        ///     Who is attacking
        /// </param>
        public static void DoEdgedThrowAttack(IAttackObject source, IEdgedWeapon edged, Vector3 weaponPos)
        {
            var throwPrefab = EdgedEffectFactory.Instance.Get(edged.ThrowBulletObject);
            var bullet = Object.Instantiate(throwPrefab);
            var edgedItem = bullet.GetComponent<ThrowItem>();

            var targetPos = source.TargetAttackPos;
            targetPos.y = weaponPos.y;

            edgedItem.Init(source, weaponPos, targetPos, edged);
        }

        /// <summary>
        ///     Метаем гранату
        ///     ---
        ///     
        /// </summary>
        /// <param name="source">
        ///     Кто атакует
        ///     ---
        ///     Who is attacking
        /// </param>
        public static void DoGrenadeAttack(IAttackObject source, IGrenadeWeapon grenade, Vector3 weaponPos)
        {
            var grenadePrefab = GrenadeEffectFactory.Instance.Get(grenade.GrenadeEffectType);
            var bullet = GameObject.Instantiate(grenadePrefab);
            var grenadeItem = bullet.GetComponent<GrenadeItem>();
            grenadeItem.Init(source, source.AttackCharacterObject.transform.position, source.TargetAttackPos, grenade);
        }

        /// <summary>
        ///     Атака ближнего боя
        /// </summary>
        /// <param name="source">
        ///     Кто атакует
        ///     ---
        ///     Who is attacking
        /// </param>
        /// <param name="target">
        ///     Кого атакует
        /// </param>
        public static void DoEdgedAttack(IAttackObject source, IDamagedObject target)
        {
            DoEdgedDamage(source, target, (IEdgedWeapon)source.Weapon);
        }

        /// <summary>
        ///     Нанесение цели target реактивного урона снарядом bulletItem
        ///     ---
        ///     Inflicting bulletItem projectile reactive damage on the target
        /// </summary>
        /// <param name="source">
        ///     Кто выпустил снаряд?
        ///     ---
        ///     Who fired the shell?
        /// </param>
        /// <param name="target">
        ///     Цель атаки, по которой наносят реактивный урон
        ///     ---
        ///     The target of the attack where reactive damage is inflicted
        /// </param>
        /// <param name="weapon">
        ///     Оружие дальнего действия, из которого был выпущен снаряд
        ///     ---
        ///     The long-range weapon from which the projectile was fired
        /// </param>
        /// <param name="bulletItem">
        ///     Снаряд, который наносит реактивный урон цели target
        ///     ---
        ///     A projectile that deals reactive damage to the target
        /// </param>
        public static void DoBulletDamage(IAttackObject source, IDamagedObject target, IFirearmsWeapon weapon, BulletItem bulletItem)
        {
            var damage = ReactiveDamageWithTargetDefence(target.Protection, weapon.Damage);
            DoDamage(source, target, damage);
        }

        public static void DoEdgedThrowDamage(IAttackObject source, IDamagedObject target, IEdgedWeapon weapon, ThrowItem throwItem)
        {
            var damage = ReactiveDamageWithTargetDefence(target.Protection, weapon.ThrowDamage);
            DoDamage(source, target, damage);
        }

        public static void DoGrenadeDamage(IAttackObject source, IDamagedObject target, IGrenadeWeapon weapon, GrenadeItem grenadeItem)
        {
            var damage = ReactiveDamageWithTargetDefence(target.Protection, weapon.Damage);
            DoDamage(source, target, damage);
        }

        private static void DoEdgedDamage(IAttackObject source, IDamagedObject target, IEdgedWeapon weapon)
        {
            var damage = ReactiveDamageWithTargetDefence(target.Protection, weapon.Damage);
            DoDamage(source, target, damage);
        }

        private static void DoDamage(IAttackObject source, IDamagedObject target, int weaponDamage)
        {
            var damage = ReactiveDamageWithTargetDefence(target.Protection, weaponDamage);
            target.Health -= damage;
            target.TakeDamage();

            AudioController.Instance.CreateTimedFragment(target.ToObject.transform.position, MixerType.Sounds, SoundFactory.DAMAGE);

            if (target.Health <= 0 && !target.ExpGeted)
            {
                target.ExpGeted = true;
                source.AddBattleExp(target.Exp);
            }

            var text = "-" + damage.ToString();
            var damageHintPrefab = EffectFactory.Instance.Get(EffectFactory.DAMAGE_HINT);
            UIHintMessageManager.Show(damageHintPrefab, target.ToObject.transform.position, text);
        }

        /// <summary>
        ///     Реактивный урон при атаке
        ///     Это урон с поправкой на вероятности (вероятность нанесения урона, вероятность срабатывания защиты и т.д.)
        ///     ---
        ///     Reactive Attack Damage
        ///     This is damage adjusted by probabilities (probability of taking damage, probability of defense triggering, etc.)
        /// </summary>
        /// <param name="targetProtection">
        ///     Защита цели (идеальная константа, без поправки на случайность)
        ///     ---
        ///     Target protection (ideal constant, no allowance for chance)
        /// </param>
        /// <param name="attackerDamage">
        ///     Атака атакующего (идеальная константа, без поправки на случайность)
        ///     ---
        ///     Attacker's attack (ideal constant, no allowance for chance)
        /// </param>
        /// <returns>
        ///     Рассчитанный реактивный урон с поправками на случайности
        ///     ---
        ///     Calculated Reactive Damage Adjusted for Accidents
        /// </returns>
        public static int ReactiveDamageWithTargetDefence(int targetProtection, float attackerDamage)
        {
            // Коэффициент прямого урона от 1f до 0f (обратное значение защиты)
            // Direct damage factor from 1f to 0f (inverse defense value)
            var defence = ReactiveDefence(targetProtection);
            
            // активный урон будет случайным от 50% до 100% урона
            // active damage will be random from 50% to 100% damage
            var reactiveDamage = ReactiveDamage(attackerDamage);
            
            // Вычисление фактического нанесённого урона
            // Calculation of actual inflicted damage
            return Mathf.RoundToInt(reactiveDamage * defence); 
        }

        public static float ReactiveDamage(float damage)
        {
            return Random.Range(GetMinDamage(damage), GetMaxDamage(damage));
        }

        public static float ReactiveDefence(int protection)
        {
            return Random.Range(GetMinDefence(protection), GetMaxDefence(protection));
        }

        public static float GetMinDefence(int protection)
        {
            return GetMaxDefence(protection) * 0.75f;
        }

        public static float GetMaxDefence(int protection)
        {
            return 1f - (GetProtectionPercent(protection) / 100f);
        }

        public static float GetMinDamage(float damage)
        {
            return GetMaxDamage(damage) * 0.5f;
        }

        public static float GetMaxDamage(float damage)
        {
            return damage;
        }

        public static float GetProtectionPercent(int protection)
        {
            // Отрицательная защита означает что объект не влияет на снаряды, он ничтожен, как лист бумаги на пути пули
            // Negative defense means that the object has no effect on the projectiles, it is as insignificant as a piece of paper in the path of a bullet
            if(protection < 0)
                // -100% вероятность защиты
                // -100% probability of protection
                return -100f;
            
            // Вычисляем процент защиты
            // Calculate the percentage of protection
            return protection / 1000f;
        }

        public static string GetProtectionPercentText(int protection)
        {
            return GetProtectionPercent(protection).ToString("0.00%");
        }

        public static float GetPenetrationPerent(float penetrationPercent, float targetProtectionPercent)
        {
            return Mathf.Max(penetrationPercent - targetProtectionPercent, 0f);
        }

        public static bool IsPenetration(float penetrationPercent)
        {
            return Random.Range(0f, 100f) <= penetrationPercent;
        }

        public static float GetMeleeDamageDistance(IAttackObject source, IEdgedWeapon edgedWeapon)
        {
            return edgedWeapon.DamageRadius;
        }

    }

}
