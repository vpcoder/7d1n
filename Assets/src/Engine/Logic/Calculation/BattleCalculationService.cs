using Engine.Data;
using Engine.Data.Factories;
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
        ///     Передача урона от снаряда
        ///     ---
        ///     
        /// </summary>
        /// <param name="source">
        ///     Кто атакует
        ///     ---
        ///     Who is attacking
        /// </param>
        /// <param name="target">
        ///     Кого атакует
        ///     ---
        ///     
        /// </param>
        /// <param name="weapon">
        ///     Оружие дальнего действия
        ///     ---
        ///     
        /// </param>
        /// <param name="bulletItem">
        ///     Пуля, которая наносит урон
        ///     ---
        ///     
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

            AudioController.Instance.CreateTimedFragment(target.ToObject.transform.position, MixerType.Sounds, "damage");

            if (target.Health <= 0 && !target.ExpGeted)
            {
                target.ExpGeted = true;
                source.AddBattleExp(target.Exp);
            }
            ObjectFinder.Find<BattleManager>().ShowDamageHint("-" + damage.ToString(), target.ToObject.transform.position);
        }

        /// <summary>
        /// Реактивный урон при атаке
        /// </summary>
        /// <param name="targetProtection">Защита цели</param>
        /// <param name="attackerDamage">Атака атакующего</param>
        /// <returns>Случайный реактивный урон</returns>
        public static int ReactiveDamageWithTargetDefence(int targetProtection, float attackerDamage)
        {
            var defence        = ReactiveDefence(targetProtection); // Коэффициент прямого урона от 1f до 0f (обратное значение защиты)
            var reactiveDamage = ReactiveDamage(attackerDamage); // активный урон будет случайным от 50% до 100% урона
            return (int)(reactiveDamage * defence); // Наносимый урон
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
            if(protection < 0) // Отрицательная защита означает что объект не влияет на сраняды, он ничтожен, как лист бумаги на пути пули
                return -100f; // -100% вероятность защиты
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
