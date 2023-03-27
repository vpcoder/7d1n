using Engine.Data;
using UnityEngine;

namespace Engine.Logic.Locations.Battle.Actions
{

    /// <summary>
    /// 
    /// Процессор, выполняющий действие завершение атаки
    /// ---
    /// Processor performing the attack completion action
    /// 
    /// </summary>
    public class ActionEndMeleeActionProcessor : BattleActionProcessor<BattleActionAttackContext>
    {

        #region Properties

        /// <summary>
        ///     Завершение атаки
        ///     ---
        ///     Ending the attack
        /// </summary>
        public override CharacterBattleAction Action => CharacterBattleAction.EndMeleeAttack;

        #endregion

        public override void DoProcessAction(BattleActionAttackContext context)
        {
            var character = ObjectFinder.Find<LocationCharacter>();
            if (character.Weapon != null)
            {
                switch (character.Weapon.Type)
                {
                    case GroupType.WeaponEdged:
                        DoEndEdgedWeaponAction(context, character);
                        break;
                }
            }
        }

        #region Attack Type Methods

        private void DoEndEdgedWeaponAction(BattleActionAttackContext context, LocationCharacter character)
        {
            var edged = context.Weapon as IEdgedWeapon;
            if (edged == null)
                return;

            switch (edged.WeaponType)
            {
                case WeaponType.Hands:
                case WeaponType.OneHanded:
                    DoOneHandedAction(edged, context, character);
                    break;
                case WeaponType.TwoHanded:
                    DoTwoHandedAction(edged, context, character);
                    break;
            }
        }

        /// <summary>
        ///     Атака одноручным оружием, цепляем первую ближайшую цель - в преоритете живой противник,
        ///     если по такому никак не попасть смотрим на неживые цели, объекты окружения
        ///     ---
        ///     Attack with a one-handed weapon, aim the first closest target - the living character takes priority,
        ///     if there is no way to hit it, look at undead targets, objects of the environment
        /// </summary>
        /// <param name="edgedWeapon">
        ///     Оружие, которым выполняется атака
        ///     ---
        ///     Weapon used in the attack
        /// </param>
        /// <param name="context">
        ///     Контекст атаки
        ///     ---
        ///     Context of the attack
        /// </param>
        /// <param name="character">
        ///     Персонаж, который выполняет атаку
        ///     ---
        ///     The character who performs the attack
        /// </param>
        private void DoOneHandedAction(IEdgedWeapon edgedWeapon, BattleActionAttackContext context, LocationCharacter character)
        {
            var colliders = GetCollidersInToSphere(edgedWeapon, context, character);
            IFragmentDamaged target = TryFindFirstTarget<FragmentCharacterDamagedBehaviour>(colliders, character); // Сначала целимся на живых NPC
            if (target == null)
                target = TryFindFirstTarget<IFragmentDamaged>(colliders, character); // NPC под руку не попались, смотрим на дамажные неживые цели
            
#if UNITY_EDITOR
            if (target != null && target.Damaged == null)
            {
                Debug.LogError("fragment " + ((MonoBehaviour)target).transform.name + " class " + target.GetType().FullName + " has empty Damaged link!");
            }            
#endif
            
            if(target != null && target.Damaged != character.Damaged) // Махали не в пустую! Во что-то попали...
                BattleCalculationService.DoEdgedAttack(character, target.Damaged);
        }

        /// <summary>
        ///     Атака двуручным оружием, цепляем все цели в радиусе атаки
        ///     ---
        ///     Attack with a two-handed weapon, snagging all targets in attack radius
        /// </summary>
        /// <param name="edgedWeapon">
        ///     Оружие, которым выполняется атака
        ///     ---
        ///     Weapon used in the attack
        /// </param>
        /// <param name="context">
        ///     Контекст атаки
        ///     ---
        ///     Context of the attack
        /// </param>
        /// <param name="character">
        ///     Персонаж, который выполняет атаку
        ///     ---
        ///     The character who performs the attack
        /// </param>
        private void DoTwoHandedAction(IEdgedWeapon edgedWeapon, BattleActionAttackContext context, LocationCharacter character)
        {
            var colliders = GetCollidersInToSphere(edgedWeapon, context, character);
            foreach (var collider in colliders)
            {
                if(collider.gameObject == character.gameObject)
                    continue;
                IFragmentDamaged target = collider.gameObject.GetComponent<IFragmentDamaged>();
                
#if UNITY_EDITOR
                if (target != null && target.Damaged == null)
                {
                    Debug.LogError("fragment " + ((MonoBehaviour)target).transform.name + " class " + target.GetType().FullName + " has empty Damaged link!");
                }            
#endif
                
                if (target != null && target.Damaged != character.Damaged)
                    BattleCalculationService.DoEdgedAttack(character, target.Damaged);
            }
        }

        private Collider[] GetCollidersInToSphere(IEdgedWeapon edgedWeapon, BattleActionAttackContext context, LocationCharacter character)
        {
            var meleeDistance = BattleCalculationService.GetMeleeDamageDistance(character, edgedWeapon);
            var halfMeleeDistance = meleeDistance * 0.5f;
            var attackSpherePos = (Vector3.Distance(context.WeaponPointPos, character.TargetAttackPos) <= halfMeleeDistance)
                ? character.TargetAttackPos
                : context.WeaponPointPos;
            var colliders = Physics.OverlapSphere(attackSpherePos, meleeDistance);
            return colliders;
        }
        
        private T TryFindFirstTarget<T>(Collider[] colliders, LocationCharacter character) where T : class, IFragmentDamaged
        {
            T target = null;
            foreach (var collider in colliders)
            {
                if(collider.gameObject == character.gameObject)
                    continue;
                target = collider.gameObject.GetComponent<T>();
                
                // Исключаем ситуацию атаки по себе
                // Excluding the situation of an attack on ourselves
                if (target?.Damaged == character.Damaged)
                    target = null;
                
                if (target != null)
                    break;
            }
            return target;
        }

        #endregion

        public override void DoRollbackAction(BattleActionAttackContext context)
        { }

    }

}
