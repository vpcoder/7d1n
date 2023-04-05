using Engine.Data;
using UnityEngine;

namespace Engine.Logic.Locations.Animation
{

    public static class AnimatorPropertyExtension
    {

        private static readonly int TakeDamagePropertyID  = Animator.StringToHash("Damage");
        private static readonly int AttackPropertyID      = Animator.StringToHash("Attack");
        private static readonly int EquipWeaponPropertyID = Animator.StringToHash("Weapon");
        private static readonly int DeadPropertyID        = Animator.StringToHash("Dead");
        private static readonly int MoveSpeedPropertyID   = Animator.StringToHash("MoveSpeed");

        public static void SetCharacterDamageType(this Animator animator, TakeDamageType type)
        {
            animator.SetInteger(TakeDamagePropertyID, (int)type);
        }
        
        public static void SetCharacterDoAttackType(this Animator animator, AttackType type)
        {
            animator.SetInteger(AttackPropertyID, (int)type);
        }
        
        public static void SetCharacterEquipWeaponType(this Animator animator, WeaponType type)
        {
            animator.SetInteger(EquipWeaponPropertyID, (int)type);
        }
        
        public static void SetCharacterDeadType(this Animator animator, DeatType type)
        {
            animator.SetInteger(DeadPropertyID, (int)type);
        }
        
        public static void SetCharacterMoveSpeedType(this Animator animator, MoveSpeedType type)
        {
            animator.SetInteger(MoveSpeedPropertyID, (int)type);
        }
        
    }
    
}
