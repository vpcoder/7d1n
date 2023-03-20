using Engine.Logic.Locations;

namespace Engine
{
    
    public class NpcCalculationService
    {
        
        /// <summary>
        ///     Определяет - видит ли npc цель lookTarget
        ///     ---
        ///     Determines whether the npc sees the lookTarget
        /// </summary>
        /// <param name="npc">
        ///     НПС, который смотрит на цель lookTarget
        ///     ---
        ///     The NPC that looks at the lookTarget
        /// </param>
        /// <param name="lookTarget">
        ///     Цель, которую пытаются увидеть
        ///     ---
        ///     The goal they are trying to see
        /// </param>
        /// <returns>
        ///     true - если НПС видит цель
        ///     false - цель не видна
        ///     ---
        ///     true - if the NPC sees the target
        ///     false - the target is not visible
        /// </returns>
        public static bool CanSeeTarget(CharacterNpcBehaviour npc, CharacterNpcBehaviour lookTarget)
        {
            
            
            return false;
        }

        public static bool CanHearTarget()
        {
            
            return false;
        }

        public static bool CanSmellTarget()
        {
            
            return false;
        }
        
    }
    
}